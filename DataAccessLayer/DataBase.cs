using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Entities;
using InterfacesLibrary;
using System.Text;

namespace DataAccessLayer
{
    public class DataBase : IDataAccessLayer
    {
        #region Fields
        private readonly string _connectionString;
        #endregion

        public DataBase()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public bool CreateAccount(Account account)
        {
            string queryString =
                "INSERT INTO accounts (accountid, login, name, mail, password, createdtime, country, city, photo, mimetype) " +
                "VALUES (@accountid, @login, @name, @mail, @password, @createdtime, @country, @city, @photo, @mimetype); " +
                "INSERT INTO UsersRoles (id, RoleId, accountid) " +
                "VALUES(@id, @RoleId, @accountid)";
            
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("accountid", account.Id);
                command.Parameters.AddWithValue("login", account.Login);
                command.Parameters.AddWithValue("name", account.Login);
                command.Parameters.AddWithValue("mail", account.Email);
                command.Parameters.AddWithValue("password", account.Password);
                command.Parameters.AddWithValue("createdtime", account.CreatedTime);
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
               
        public IEnumerable<Account> GetAllAccounts()
        {
            string queryString =
               "SELECT [dbo].accounts.accountid, login, mail, [dbo].accounts.name, [dbo].UsersRoles.RoleId " +
               "FROM [dbo].accounts, [dbo].UsersRoles " +
               "WHERE [dbo].UsersRoles.AccountId = [dbo].accounts.accountid;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                var list = new List<Account>();
                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                   list.Add(new Account()
                    {
                        Id = (Guid)reader[0],
                        Login = (string)reader[1],
                        Email = (string)reader[2],
                        Name = (string)reader[3]
                   });
                }
                return list;
            }
        }

        
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

        public Dictionary<Guid, string[]> GetRolesOfAccounts()
        {
            string queryString = "SELECT [dbo].[UsersRoles].AccountId, [dbo].[Roles].Name " +
                                 "FROM[dbo].[Roles], [dbo].[UsersRoles] " +
                                 "WHERE[dbo].[Roles].RoleId = [dbo].[UsersRoles].RoleId;";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                connection.Open();
                var reader = command.ExecuteReader();
                var result = new Dictionary<Guid, string[]>();

                while (reader.Read())
                {
                    result.Add((Guid)reader[0], ((string)reader[1]).Split(','));
                }
                return result;
            }
        }

        public Account GetAccountByLogin(string username, bool checkExisting)
        {
            string queryString =
                "SELECT [dbo].accounts.accountid, login, password, mail, city, country, [dbo].accounts.name, [dbo].Roles.Name, [dbo].accounts.photo, [dbo].accounts.mimetype " +
                "FROM [dbo].accounts, [dbo].UsersRoles, [dbo].Roles " +
                "WHERE (login = @login) AND ([dbo].accounts.accountid = [dbo].UsersRoles.AccountId) AND ([dbo].UsersRoles.RoleId = [dbo].Roles.RoleId);";

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
                            Avatar = (byte[])reader[8],
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

        public bool UpdateRole(Guid accountId, int roleCode)
        {
            var queryString =
                 "UPDATE [dbo].[UsersRoles] " +
                 "SET RoleId = @roleid " +
                 "WHERE [dbo].UsersRoles.AccountId = @AccountId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("roleid", roleCode);
                command.Parameters.AddWithValue("AccountId", accountId);

                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

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

        public bool DeleteAccount(string name)
        {
            string queryString =
                "DELETE FROM[dbo].comments "     +
                "WHERE name = @name; "           +
                "DELETE FROM[dbo].[accounts] "   +
                "WHERE accounts.[login] = @name;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("name", name);

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

        public Post GetPost(Guid postId)
        {
            var queryString =
                "SELECT posts.postid, posts.postname, posts.source, posts.createdtime, posts.accountid, posts.rating, posts.text, tag, posts.mimetype " +
                "FROM [dbo].posts, [dbo].tags, [dbo].accounts " +
                "WHERE (posts.postid = @postid) AND (tags.postid = posts.postid);";

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
                        Image = (byte[])reader[2],
                        CreatedTime = (DateTime)reader[3],
                        AccountId = (Guid)reader[4],
                        Rating = (int)reader[5],
                        Text = (string)reader[6],
                        AuthorName = NameById((Guid)reader[4]),
                        Tags = ((string)reader[7]).Split(','),
                        MimeType = (string)reader[8]
                    };
                }

                return null;
            }
        }

        public List<Comment> GetComents(Guid postId)
        {
            string queryString =
                "SELECT [dbo].comments.text, [dbo].comments.createdtime, comments.accountid, comments.name, comments.comid, comments.postid " +
                "FROM [dbo].comments " +
                "WHERE [dbo].comments.postid = @postid;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("postid", postId);

                connection.Open();

                var reader = command.ExecuteReader();
                var list = new List<Comment>();

                if (reader == null)
                    return null;

                while (reader.Read())
                {
                    list.Add(new Comment()
                    { 
                        Text = (string)reader[0],
                        CreatedTime = (DateTime)reader[1],
                        AccountId = (Guid)reader[2],
                        AuthorName = (string)reader[3],
                        ComId = (Guid)reader[4],
                        PostId = (Guid)reader[5]
                    });
                }

                return list;
            }
        }

        public bool CreateComment(Comment comment)
        {
            var queryString =
                "INSERT INTO [dbo].comments ([dbo].comments.comid, [dbo].comments.createdtime, [dbo].comments.accountid, [dbo].comments.postid, [dbo].comments.name, [dbo].comments.text) " +
                "VALUES (@comid, @createdtime, @accountid, @postid, @name, @text);";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("comid", comment.ComId);
                command.Parameters.AddWithValue("createdtime", comment.CreatedTime);
                command.Parameters.AddWithValue("accountid", comment.AccountId);
                command.Parameters.AddWithValue("postid", comment.PostId);
                command.Parameters.AddWithValue("name", comment.AuthorName);
                command.Parameters.AddWithValue("text", comment.Text);

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

                return true;
            }
        }
        
        public List<Post> GetAllPosts()
        {
            string queryString =
                "SELECT [dbo].posts.postid, posts.postname, posts.source, posts.createdtime, posts.accountid, posts.rating, posts.[text], [dbo].accounts.[login], tag, [dbo].posts.mimetype " +
                "FROM [dbo].posts, [dbo].tags, [dbo].accounts " +
                "WHERE (([dbo].tags.postid = [dbo].posts.postid) AND ([dbo].posts.accountid = [dbo].accounts.accountid))";

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
                        Tags = ((string)reader[8]).Split(','),
                        MimeType = (string)reader[9]
                    });
                    var exec = _list;
                }

                return _list;
            }
        }

        public bool CreatePost(Post post)
        {
            string queryString =
                "INSERT INTO [dbo].posts ([dbo].posts.postid, postname, source, createdtime, mimetype, accountid, text, rating) " +
                "VALUES (@postid, @postname, @source, @createdtime, @mimetype, @accountid, @text, @rating); " +
                "INSERT INTO tags ([dbo].tags.postid, tag) " +
                "VALUES(@postid, @tag)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("postid", post.PostId);
                command.Parameters.AddWithValue("postname", post.NamePost);
                command.Parameters.AddWithValue("source", post.Image);
                command.Parameters.AddWithValue("createdtime", post.CreatedTime);
                command.Parameters.AddWithValue("mimetype", post.MimeType);
                command.Parameters.AddWithValue("accountid", post.AccountId);
                command.Parameters.AddWithValue("text", post.Text);
                command.Parameters.AddWithValue("rating", post.Rating);
                command.Parameters.AddWithValue("tag", post.tmp);

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

        public Guid GetIdByName(string login)
        {
            string queryString =
               "SELECT [dbo].accounts.accountid " +
               "FROM [dbo].accounts " +
               "WHERE (login = @login);";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("login", login);

                connection.Open();
                var reader = command.ExecuteReader();

                if (reader == null)
                    return Guid.Empty;
                Guid guid = Guid.Empty;
                while (reader.Read())
                    guid = (Guid)reader[0];
                return guid;
            }
        }

        public string NameById(Guid accountId)
        {
            var queryString =
               "SELECT accounts.login " +
               "FROM [dbo].accounts " +
               "WHERE accounts.accountid = @accountid;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("accountid", accountId);

                connection.Open();
                var reader = command.ExecuteReader();

                if (reader == null)
                    return null;
                var str = "";
                while (reader.Read())
                    str = (string)reader[0];
                return str;
            }
        }

        #region LikeArea

        public bool SetLike(Guid postId, Guid accountId)
        {
            string queryString =
                "INSERT INTO [dbo].Likes ([dbo].Likes.likeid, [dbo].Likes.postid, [dbo].Likes.accountid) " +
                "VALUES (@likeid, @postid, @accountid); " +
                "UPDATE [dbo].[posts] " +
                      "SET [rating] = rating + 1 " +
                  "WHERE [dbo].posts.postid = @postid";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("likeid", Guid.NewGuid());
                command.Parameters.AddWithValue("postid", postId);
                command.Parameters.AddWithValue("accountid", accountId);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception) { return false; throw new Exception(); }
            }
            return true;
        }

        public IEnumerable<Guid> GetLikedByUser(string modelName)
        {
            var account = GetAccountByLogin(modelName, true);
            var accountId = account.Id;
            var queryString =
                "SELECT (postid) " +
                "FROM [dbo].Likes " +
                "WHERE accountid = @accountid;";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                List<Guid> _list;
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("accountid", accountId);
                connection.Open();

                var reader = command.ExecuteReader();
                _list = new List<Guid>();

                while (reader.Read())
                    _list.Add((Guid)reader[0]);

                return _list;
            }
        }

       
        public Dictionary<bool, int> GetLikes(Guid postId, Guid accountId)
        {
            var queryString =
                "SELECT [dbo].posts.rating, [dbo].Likes.likeid " +
                "FROM [dbo].posts, [dbo].Likes " +
                "WHERE ([dbo].posts.postid = @postid) AND " +
                "([dbo].Likes.accountid = @accountid) AND ([dbo].Likes.postid = @postid);";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("accountid", accountId);
                command.Parameters.AddWithValue("postid", postId);
                connection.Open();

                var reader = command.ExecuteReader();
                Dictionary<bool, int> toReturn;
                Guid guid = Guid.Empty;
                int rating = 0;

                while (reader.Read())
                {
                    rating = (int)reader[0];
                    guid = (Guid)reader[1];
                }

                if ((guid != Guid.Empty) && (rating >= 0))
                {
                    toReturn = new Dictionary<bool, int>();
                    toReturn.Add(true, rating);
                    return toReturn;
                }
                else
                {
                    toReturn = new Dictionary<bool, int>();
                    toReturn.Add(false, rating);
                    return toReturn;
                }
            }
        }
                
        public void DeleteComment(Guid comid)
        {
            var queryString = "DELETE FROM [dbo].[comments] " +
                              "WHERE comments.comid = @comid;";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("comid", comid);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
                return;
            }

            #endregion
        }

        public void CreateAdmin(Account account)
        {         
            string queryString =
                "INSERT INTO accounts (accountid, login, name, mail, password, createdtime, country, city, photo, mimetype) " +
                "VALUES (@accountid, @login, @name, @mail, @password, @createdtime, @country, @city, @photo, @mimetype); " +
                "INSERT INTO UsersRoles (id, RoleId, accountid) " +
                "VALUES(@id, @RoleId, @accountid)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("accountid", account.Id);
                command.Parameters.AddWithValue("login", account.Login);
                command.Parameters.AddWithValue("name", account.Login);
                command.Parameters.AddWithValue("mail", account.Email);
                command.Parameters.AddWithValue("password", account.Password);
                command.Parameters.AddWithValue("createdtime", account.CreatedTime);
                command.Parameters.AddWithValue("id", Guid.NewGuid());
                command.Parameters.AddWithValue("RoleId", 3);
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
        }

        public int RegisterRoles()
        {           
           var queryString =
                 "SET IDENTITY_INSERT[dbo].[Roles] ON "                           +
                 "INSERT INTO[dbo].[Roles] ([RoleId], [Name]) VALUES(1, 'User'); " +
                 "INSERT INTO[dbo].[Roles] ([RoleId], [Name]) VALUES(2, 'User,Moder'); " +
                 "INSERT INTO[dbo].[Roles] ([RoleId], [Name]) VALUES(3, 'User,Moder,Admin'); " +
                 "SET IDENTITY_INSERT[dbo].[Roles] OFF; ";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
