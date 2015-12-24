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
                "INSERT INTO Accounts (Login, Email, Hash) " +
                "VALUES (@login, @email, @hash)";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("login", account.Login);
                command.Parameters.AddWithValue("email", account.Email);
                command.Parameters.AddWithValue("hash", account.Password);
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

        public Account GetAccountById(int id)
        {
            string queryString =
                "SELECT AccountId, Login, Email " +
                "FROM dbo.Accounts " +
                "WHERE AccountId = @id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Account()
                    {
                        Id = (int)reader[0],
                        Login = (string)reader[1],
                        Email = (string)reader[2]
                    };
                }
                return null;
            }
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            string queryString =
                "SELECT AccountId, Login, Email " +
                "FROM dbo.Accounts";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return new Account()
                    {
                        Id = (int)reader[0],
                        Login = (string)reader[1],
                        Email = (string)reader[2]
                    };
                }
            }
        }


        public string[] GetAllRoles()
        {
            string queryString = "SELECT RoleId, Name " +
                                 "FROM dbo.Roles";

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
            string queryString = "SELECT AccountId, Roles.Name AS RoleName " +
                                 "FROM AccountsRoles " +
                                 "INNER JOIN Roles ON AccountsRoles.RoleId = Roles.RoleId";

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
    "SELECT Id, login, Email, Hash " +
    "FROM dbo.Users " +
    "WHERE Login = @login";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("login", username);

                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return new Account()
                    {
                        Id = (int)reader[0],
                        Login = (string)reader[1],
                        Email = (string)reader[2],
                        Password = (string)reader[3]
                    };
                }

                return null;
            }
        }

        public bool Update(Account obj)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAccount(int id)
        {
            throw new NotImplementedException();
        }
    }
}
