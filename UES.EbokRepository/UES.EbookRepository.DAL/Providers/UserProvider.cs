using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UES.EbookRepository.DAL.Contract.Contracts;
using UES.EbookRepository.DAL.Contract.Entities;

namespace UES.EbookRepository.DAL.Providers
{
    public class UserProvider:IUserProvider
    {
        private static string _connectionString = @"server=localhost;user id=root;password=root;database=ebook;";
        private static string _tableName = "users";

        public IEnumerable<User> GetAll()
        {
            List<User> users = new List<User>();

            using (MySqlConnection sqlConnection = new MySqlConnection(_connectionString))
            {
                string query = String.Format("SELECT * FROM {0};", _tableName);
                sqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                {
                    using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            User user = null;

                            while (reader.Read())
                            {
                                user = MapTableEnityToObject(reader);
                                users.Add(user);
                            }
                        }
                    }
                }

            }

            return users;
        }

        public User GetById(int id)
        {
            User user = new User();

            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                string query = String.Format("SELECT * FROM {0} WHERE UserId = @Id;", _tableName);

                sqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("Id", id);
                    using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                user = MapTableEnityToObject(reader);
                            }
                        }
                    }
                }
            }

            return user;
        }

        public int Create(User user)
        {
            int newUserId = 0;
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                string query = String.Format("INSERT INTO {0}" +
                    "(Firstname,Lastname,Username,Password,Type,CategoryId)" +
                    "VALUES(@Firstname,@Lastname,@Username,@Password,@Type,@CategoryId); Select LAST_INSERT_ID();", _tableName);

                sqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("Firstname", user.Firstname);
                    sqlCommand.Parameters.AddWithValue("Lastname", user.Lastname);
                    sqlCommand.Parameters.AddWithValue("Username", user.Username);
                    sqlCommand.Parameters.AddWithValue("Password", user.Password);
                    sqlCommand.Parameters.AddWithValue("Type", user.Type);
                    sqlCommand.Parameters.AddWithValue("CategoryId", user.CategoryId);
                    newUserId = int.Parse(sqlCommand.ExecuteScalar().ToString());
                }
            }

            return newUserId;
        }

        public int Update(User user)
        {
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                string query = String.Format("UPDATE {0} SET " +
                    "Firstname=@Firstname,Lastname=@Lastname,Username=@Username,Password=@Password,Type=@Type,CategoryId=@CategoryId" +
                    " WHERE UserId = @Id", _tableName);

                sqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("Id", user.UserId);
                    sqlCommand.Parameters.AddWithValue("Firstname", user.Firstname);
                    sqlCommand.Parameters.AddWithValue("Lastname", user.Lastname);
                    sqlCommand.Parameters.AddWithValue("Username", user.Username);
                    sqlCommand.Parameters.AddWithValue("Password", user.Password);
                    sqlCommand.Parameters.AddWithValue("Type", user.Type);
                    sqlCommand.Parameters.AddWithValue("CategoryId", user.CategoryId);
                    int updatedRows = sqlCommand.ExecuteNonQuery();
                    if (updatedRows > 0)
                    {
                        Console.WriteLine("Category updated");
                    }
                    else
                    {
                        Console.WriteLine("Category not updated");
                    }
                }
            }

            return user.CategoryId;
        }

        public int Delete(int id)
        {
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                string query = String.Format("DELETE FROM {0} WHERE UserId = @Id", _tableName);

                sqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", id);

                    int deletedRows = sqlCommand.ExecuteNonQuery();
                    if (deletedRows > 0)
                    {
                        Console.WriteLine("User deleted");
                    }
                    else
                    {
                        Console.WriteLine("Not deleted");
                    }
                }
            }

            return id;
        }

        private User MapTableEnityToObject(IDataRecord record)
        {
            User result = new User();

            result.UserId = (int)record["UserId"];
            result.Firstname = (string)record["Firstname"];
            result.Lastname= (string)record["Lastname"];
            result.Username = (string)record["Username"];
            result.Password = (string)record["Password"];
            result.Type = (string)record["Type"];
            result.CategoryId = (int)record["CategoryId"];
            return result;
        }
    }
}
