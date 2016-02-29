using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //TODO: добавить создание роли
            string queryString =
                "INSERT INTO accounts (accountid, login, name, mail, password, createdtime) " +
                "VALUES (@accountid, @login, @name, @mail, @password, @createdtime)";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("accountid", Guid.NewGuid());
                command.Parameters.AddWithValue("login", account.Login);
                command.Parameters.AddWithValue("name", account.Login);
                command.Parameters.AddWithValue("mail", account.Email);
                command.Parameters.AddWithValue("password", account.Password);
                command.Parameters.AddWithValue("createdtime", DateTime.Now);

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
                "SELECT accountid, login, mail, city, country " +
                "FROM [dbo].accounts " +
                "WHERE accountid = @accountid";

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
                "SELECT accountid, login, password, mail, city, country, name " +
                "FROM [dbo].accounts " +
                "WHERE login = @login";

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
                        Name = (string)reader[6]
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
                     ",[password] = @password" +
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
                     ",[mail] = @mail" +
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
    }
}