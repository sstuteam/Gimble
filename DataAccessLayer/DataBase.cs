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
        private readonly string _connectionString;

        public DataBase()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public bool CreateAccount(Account account)
        {
            
            string queryString =
                "INSERT INTO accounts (accountid, login, name, mail, password, createdtime) " +
                "VALUES (@accountid, @login, @name, @mail, @password, @createdtime); " +
                "INSERT INTO UsersRoles (RoleId, accountid) " +
                "VALUES(@RoleId, @accountid)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("accountid", Guid.NewGuid());
                command.Parameters.AddWithValue("login", account.Login);
                command.Parameters.AddWithValue("name", account.Login);
                command.Parameters.AddWithValue("mail", account.Email);
                command.Parameters.AddWithValue("password", account.Password);
                command.Parameters.AddWithValue("createdtime", DateTime.Now);
                command.Parameters.AddWithValue("RoleId", 1);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                    return false;
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

        public Dictionary<int, List<string>> GetRolesOfAccounts()
        {
            string queryString = "SELECT accountid, Roles.name AS RoleName " +
                                 "FROM AccountsRoles " +
                                 "INNER JOIN Roles ON AccountsRoles.roleid = Roles.roleid";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                connection.Open();
                var reader = command.ExecuteReader();
                var result = new Dictionary<int, List<string>>();

                while (reader.Read())
                {
                    List<string> roles;

                    if (result.TryGetValue((int)reader[0], out roles))
                    {
                        roles.Add((string)reader[1]);
                        result[(int)reader[0]].Add((string)reader[1]);
                    }
                    else
                    {
                        roles = new List<string>
                        {
                            (string) reader[1]
                        };
                        result.Add((int)reader[0], roles);
                    }

                }


                return result;
            }
        }

        public Account GetAccountByLogin(string username)
        {
            string queryString =
                "SELECT [dbo].accounts.accountid, login, password, mail, city, country, [dbo].accounts.name, [dbo].Roles.Name " +
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
                    return new Account()
                    {
                        Id = (Guid)reader[0],
                        Login = (string)reader[1],
                        Password = (string)reader[2],
                        Email = (string)reader[3],
                        City = (string)reader[4],
                        Country = (string)reader[5],
                        Name = (string)reader[6],
                        Role = (string)reader[7]
                    };
                }

                return null;
            }
        }

        public bool Update(Account account)
        {
            var queryString =
                 "UPDATE [dbo].[accounts] " +
                     ",[login] = @login " +
                     ",[name] =    @name" +
                     ",[country] = @country" +
                     ",[city] = @city" +
                     ",[mail] = @mail" +
                 "WHERE [dbo].accounts.accountid = @accountid";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("login", account.Login);
                command.Parameters.AddWithValue("password", account.Password);
                command.Parameters.AddWithValue("name", account.Name);
                command.Parameters.AddWithValue("country", account.Country);
                command.Parameters.AddWithValue("city", account.City);
                command.Parameters.AddWithValue("accountid", account.Id);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                    return false;
                }

                return true;

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
                    return false;
                }

                return true;

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

                    return false;
                }

                return true;

            }
        }

        public bool UpdateCityAndCountry(Guid id, string newCity, string newCountry)
        {
            var queryString =
                 "UPDATE [dbo].[accounts] " +
                     ",[country] = @country" +
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
                    return false;
                }

                return true;

            }
        }

        public bool DeleteAccount(Guid id)
        {
            string queryString =
            "DELETE FROM[dbo].[accounts] " +
              "WHERE accounts.accountid = @accountid;";

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
                    return false;
                }

                return true;
            }
        }

        public Post GetPost(Guid postId)
        {
            string queryString =
                "SELECT [dbo].posts.postid, postname, source, createdtime, accountid, rating, text, [dbo].accounts.name, tag " +
                "FROM [dbo].posts, [dbo].tagposts, [dbo].accounts " +
                "WHERE (postid = @postid) AND ([dbo].tagposts.postid = [dbo].posts.postid) " +
                "AND ([dbo].posts.accountid = [dbo].accounts.accountid)";

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
                        PostId = (Guid)reader[0],
                        NamePost = (string)reader[1],
             /*!!!!!*/  Source = (string)reader[2],
                        CreatedTime = (DateTime)reader[3],
                        AccountId = (Guid)reader[4],
                        Rating = (int)reader[5],
                        Text = (string)reader[6],
                        AuthorName = (string)reader[7],
                        Tags = ((string)reader[8]).ToString().Split(',')
                    };
                }

                return null;
            }
        }

        public bool CreatePost(Post post)
        {

            string queryString =
                "INSERT INTO [dbo].posts ([dbo].posts.postid, postname, source, createdtime, accountid, text) " +
                "VALUES (@postid, @postname, @source, @createdtime, @accountid, @createdtime, @text); " +
                "INSERT INTO tags (postid, tag) " +
                "VALUES(@postid, @tag)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("postid", Guid.NewGuid());
                command.Parameters.AddWithValue("postname",post.NamePost);
                command.Parameters.AddWithValue("source", post.Source);
                command.Parameters.AddWithValue("createdtime", DateTime.Now);
                command.Parameters.AddWithValue("accountid", post.AccountId);
                command.Parameters.AddWithValue("text", post.Text);
                command.Parameters.AddWithValue("tag", post.Tags.ToString());

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                    return false;
                }
            }

            return true;
        }
    }
}