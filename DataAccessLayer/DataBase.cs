using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Entities;
using InterfacesLibrary;

namespace DataAccessLayer
{
    public class DataBase : IDataAccessLayer
    {
        #region Fields
        private readonly string _connectionString;
        private readonly string _basixCity = "Не указан город";
        private readonly string _basixCountry = "Не указана страна";
        private readonly byte[] _basixAvatar = { 0, 0, 0, 25, 1, 0, 4 };
        private readonly string _basixMimeType = "image/jpeg";
        #endregion

        public DataBase()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public bool CreateAccount(Account account)
        {
            var accountId = Guid.NewGuid();

            string queryString =
                "INSERT INTO accounts (accountid, login, name, mail, password, createdtime, country, city, photo, mimetype) " +
                "VALUES (@accountid, @login, @name, @mail, @password, @createdtime, @country, @city, @photo, @mimetype); " +
                "INSERT INTO UsersRoles (id, RoleId, accountid) " +
                "VALUES(@id, @RoleId, @accountid)";
            if (account.Avatar == null)
                account.Avatar = _basixAvatar;
            if (account.City == null)
                account.City = _basixCity;
            if (account.Country == null)
                account.Country = _basixCountry;
            if (account.MimeType == null)
                account.MimeType = _basixMimeType;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("accountid", accountId);
                command.Parameters.AddWithValue("login", account.Login);
                command.Parameters.AddWithValue("name", account.Login);
                command.Parameters.AddWithValue("mail", account.Email);
                command.Parameters.AddWithValue("password", account.Password);
                command.Parameters.AddWithValue("createdtime", DateTime.Now);
                command.Parameters.AddWithValue("id", Guid.NewGuid());
                command.Parameters.AddWithValue("RoleId", 1);
                command.Parameters.AddWithValue("country", account.Country);
                command.Parameters.AddWithValue("city", account.City);
                command.Parameters.AddWithValue("photo", account.Avatar);
                command.Parameters.AddWithValue("mimetype", account.MimeType);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return true;
        }

        /// <summary>
        /// Получение экземпляра класса Account по Guid пользователя
        /// </summary>
        /// <param name="id">Guid пользователя</param>
        /// <returns>Account</returns>
        public Account GetAccountById(Guid id)
        {
            string queryString =
               "SELECT [dbo].accounts.accountid, login, password, mail, city, country, [dbo].accounts.name, [dbo].Roles.Name " +
                "FROM [dbo].accounts, [dbo].UsersRoles, [dbo].Roles " +
                "WHERE ([dbo].accounts = @accountid) AND ([dbo].Roles.RoleId = [dbo].UsersRoles.RoleId);";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("accountid", id);

                connection.Open();

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new Account()
                    {
                        Id = (Guid)reader[0],
                        Login = (string)reader[1],
                        Email = (string)reader[2],
                        City = (string)reader[3],
                        Country = (string)reader[4]
                    };
                }
                return null;
            }
        }

        /// <summary>
        /// Получение Перечисления всех аккаунтов.
        /// </summary>
        /// <returns>Перечисление аккаунтов</returns>
        public IEnumerable<Account> GetAllAccounts()
        {
            string queryString =
                "SELECT accountid, login, mail " +
                "FROM [dbo].accounts";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return new Account()
                    {
                        Id = (Guid)reader[0],
                        Login = (string)reader[1],
                        Email = (string)reader[2]
                    };
                }
            }
        }

        /// <summary>
        /// Получение списка всех ролей
        /// </summary>
        /// <returns>Массив ролей</returns>
        public string[] GetAllRoles()
        {
            string queryString = "SELECT roleid, name " +
                                 "FROM [dbo].Roles";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                connection.Open();

                var reader = command.ExecuteReader();

                var result = new List<string>();

                while (reader.Read())
                {
                    result.Add((string)reader[1]);
                }

                return result.ToArray();
            }
        }

        /// <summary>
        /// Функция для получения всех ролей аккаунтов.
        /// </summary>
        /// <returns></returns>
        public Dictionary<Guid, List<string>> GetRolesOfAccounts()
        {
            string queryString = "SELECT accountid, Roles.name " +
                                 "FROM  Roles, UsersRoles " +
                                 "INNER JOIN Roles ON UsersRoles.RoleId = Roles.RoleId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                connection.Open();
                var reader = command.ExecuteReader();

                var result = new Dictionary<Guid, List<string>>();

                while (reader.Read())
                {
                    List<string> roles;

                    if (result.TryGetValue((Guid)reader[0], out roles))
                    {
                        roles.Add((string)reader[1]);
                        result[(Guid)reader[0]].Add((string)reader[1]);
                    }
                    else
                    {
                        roles = new List<string>
                        {
                            (string) reader[1]
                        };
                        result.Add((Guid)reader[0], roles);
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// Функция для получения данных аккаунта по логину
        /// </summary>
        /// <param name="username">Параметр - login пользователя</param>
        /// <param name="checkExisting">Флаг, если установить true, то результатом вызова будет аккаунт
        ///  с минимальным число полей, например, для проверки на существование.
        /// Значение флага false эквивлентно возврату всех полей аккаунта. </param>
        /// <returns>Аккаунт пользователя</returns>
        public Account GetAccountByLogin(string username, bool checkExisting)
        {
            string queryString =
                "SELECT [dbo].accounts.accountid, login, password, mail, city, country, [dbo].accounts.name, [dbo].Roles.Name, [dbo].accounts.photo, [dbo].accounts.mimetype " +
                "FROM [dbo].accounts, [dbo].UsersRoles, [dbo].Roles " +
                "WHERE (login = @login) AND ([dbo].Roles.RoleId = [dbo].UsersRoles.RoleId);";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("login", username);

                connection.Open();
                var reader = command.ExecuteReader();

                if (reader == null)
                {
                    return null;
                }

                while (reader.Read())
                {
                    if (!checkExisting)
                    {
                        return new Account()
                        {
                            Id = (Guid)reader[0],
                            Login = (string)reader[1],
                            Password = (string)reader[2],
                            Email = (string)reader[3],
                            City = (string)reader[4],
                            Country = (string)reader[5],
                            Name = (string)reader[6],
                            Role = ((string)reader[7]).Split(','),
                            Avatar = (byte [])reader[8],
                            MimeType = (string)reader[9]
                        };
                    }
                    else
                    {
                        return new Account()
                        {
                            Id = (Guid)reader[0],
                            Password = (string)reader[2],
                            Role = ((string)reader[7]).Split(',')
                        };
                    }
                    
                }

                return null;
            }
        }
        
        /// <summary>
        /// Функция для смены пароля по имеющемуся id.
        /// Вызов подразумевается от аккаунта авторизованного пользователя.
        /// </summary>
        /// <param name="id">Guid пользователя</param>
        /// <param name="password">Новый пароль</param>
        /// <returns>Успешность операции</returns>
        public bool UpdatePassword(Guid id, string password)
        {
            var queryString =
                 "UPDATE [dbo].[accounts] " +                     
                 "SET password = @password " +
                 "WHERE [dbo].accounts.accountid = @accountid";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("password", password);
                command.Parameters.AddWithValue("accountid", id);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }

                return true;

            }
        }

        /// <summary>
        /// Функция для смены почтового ящика по имеющемуся id.
        /// Вызов подразумевается от аккаунта авторизованного пользователя.
        /// </summary>
        /// <param name="id">Guid пользователя</param>
        /// <param name="newMail">Новый EMail</param>
        /// <returns>Успешность операции</returns>
        public bool UpdateMail(Guid id, string newMail)
        {
            var queryString =
                 "UPDATE [dbo].[accounts] " +
                 "SET mail = @mail " +
                 "WHERE [dbo].accounts.accountid = @accountid";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("mail", newMail);
                command.Parameters.AddWithValue("accountid", id);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }

                return true;

            }
        }

        /// <summary>
        /// Функция для смены города и страны.
        /// Вызов подразумевается от аккаунта авторизованного пользователя.
        /// </summary>
        /// <param name="id">Guid пользователя</param>
        /// <param name="newCity">Новый город</param>
        /// /// <param name="newCountry">Новыая страна</param>
        /// <returns>Успешность операции</returns>
        public bool UpdateCityAndCountry(Guid id, string newCity, string newCountry)
        {
            var queryString =
                 "UPDATE [dbo].[accounts] " +
                     "SET [country] = @country" +
                     ",[city] = @city" +
                 "WHERE [dbo].accounts.accountid = @accountid";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("country", newCountry);
                command.Parameters.AddWithValue("city", newCity);
                command.Parameters.AddWithValue("accountid", id);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }

                return true;

            }
        }

        /// <summary>
        /// Функция смены аватара на странице.
        /// </summary>
        /// <param name="accountId">Guid пользователя</param>
        /// <param name="Image">Массив байтов от фатографии</param>
        /// <param name="mimeType">mimeType для операции преобразования массива байтов в фотографию</param>
        /// <returns></returns>
        public bool UpdateAvatar(Guid accountId, byte[] Image, string mimeType)
        {
            var queryString =
                 "UPDATE [dbo].[accounts] " +
                     "SET [photo] = @photo " +
                     ",[mimetype] = @mimetype " +
                 "WHERE [dbo].accounts.accountid = @accountid";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("photo", Image);
                command.Parameters.AddWithValue("mimetype", mimeType);
                command.Parameters.AddWithValue("accountid", accountId);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }

                return true;

            }
        }

        /// <summary>
        /// Удаление аккаунта из базы данных и его постов 
        /// </summary>
        /// <param name="id">Guid пользователя</param>
        /// <returns>Успешность операции</returns>
        public bool DeleteAccount(Guid id)
        {
            string queryString =
            "DELETE FROM[dbo].[accounts] " +
            "WHERE accounts.accountid = @accountid; " +
            "DELETE FROM [dbo].[posts], [dbo].[tags] " +
            "WHERE posts.accountid = @accountid; ";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                
                command.Parameters.AddWithValue("accountid", id);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }

                return true;
            }
        }

        /// <summary>
        /// Получение поста по Guid поста
        /// </summary>
        /// <param name="postId">Guid поста</param>
        /// <returns>Экземпляр класса Post</returns>
        public Post GetPost(Guid postId)
        {
            string queryString =
                "SELECT [dbo].posts.postid, posts.postname, posts.source, posts.createdtime, posts.accountid, posts.rating, posts.text, [dbo].accounts.login, tag, [dbo].posts.mimetype " +
                "FROM [dbo].posts, [dbo].tags, [dbo].accounts " +
                "WHERE ([dbo].posts.postid = @postid) AND ([dbo].tags.postid = [dbo].posts.postid) " +
                "AND ([dbo].accounts.accountid = [dbo].posts.accountid)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("postid", postId);

                connection.Open();
                var reader = command.ExecuteReader();

                if (reader == null)
                {
                    return null;
                }

                while (reader.Read())
                {
                    return new Post()
                    {
                        PostId = postId,
                        NamePost = (string)reader[1],
                        Image = (byte [])reader[2],
                        CreatedTime = (DateTime)reader[3],
                        AccountId = (Guid)reader[4],
                        Rating = (int)reader[5],
                        Text = (string)reader[6],
                        AuthorName = (string)reader[7],
                        Tags = ((string)reader[8]).ToString().Split(','),
                        MimeType = (string)reader[9]
                    };
                }

                return null;
            }
        }

        /// <summary>
        /// Получение всех постов.
        /// </summary>
        /// <returns>Список постов</returns>
        public List<Post> GetAllPosts()
        {
            string queryString =
                "SELECT [dbo].posts.postid, posts.postname, posts.source, posts.createdtime, posts.accountid, posts.rating, posts.text, [dbo].accounts.login, tag, [dbo].posts.mimetype " +
                "FROM [dbo].posts, [dbo].tags, [dbo].accounts " +
                "WHERE ([dbo].tags.postid = [dbo].posts.postid) " +
                "AND ([dbo].posts.accountid = [dbo].accounts.accountid)";

            List<Post> _list;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                
                connection.Open();
                var reader = command.ExecuteReader();

                if (reader == null)
                {
                    return null;
                }
               
                _list = new List<Post>();

                while (reader.Read())
                {                   
                    _list.Add(new Post()
                    {
                        PostId = (Guid)reader[0],
                        NamePost = (string)reader[1],
                        Image = (byte[])reader[2],
                        CreatedTime = (DateTime)reader[3],
                        AccountId = (Guid)reader[4],
                        Rating = (int)reader[5],
                        Text = (string)reader[6],
                        AuthorName = (string)reader[7],
                        Tags = ((string)reader[8]).ToString().Split(','),
                        MimeType = (string)reader[9],
                        Avatar = GetAccountByLogin((string)reader[7], false).Avatar,
                        MimeTypeAvatar = GetAccountByLogin((string)reader[7], false).MimeType
                    });
                }

                return _list;
            }
        }

        /// <summary>
        /// Функция добавления поста.
        /// </summary>
        /// <param name="post">Экзмемляр класса Post(Доменной модели)</param>
        /// <returns>Успешность операции</returns>
        public bool CreatePost(Post post)
        {
            var postid = Guid.NewGuid();

            post.Rating = 0;

            string queryString =
                "INSERT INTO [dbo].posts ([dbo].posts.postid, postname, source, createdtime, mimetype, accountid, text, rating) " +
                "VALUES (@postid, @postname, @source, @createdtime, @mimetype, @accountid, @text, @rating); " +
                "INSERT INTO tags ([dbo].tags.postid, tag) " +
                "VALUES(@postid, @tag)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("postid", postid);
                command.Parameters.AddWithValue("postname",post.NamePost);
                command.Parameters.AddWithValue("source", post.Image);
                command.Parameters.AddWithValue("createdtime", DateTime.Now);
                command.Parameters.AddWithValue("mimetype", post.MimeType);
                command.Parameters.AddWithValue("accountid", post.AccountId);
                command.Parameters.AddWithValue("text", post.Text);
                command.Parameters.AddWithValue("rating", post.Rating);
                command.Parameters.AddWithValue("tag", post.Tags.ToString());

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    var stack = e.StackTrace;                    
                    throw new Exception();                    
                }
            }

            return true;
        }
    }
}
