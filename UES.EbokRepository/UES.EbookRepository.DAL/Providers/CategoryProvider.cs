using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using UES.EbookRepository.DAL.Contract.Contracts;
using UES.EbookRepository.DAL.Contract.Entities;

namespace UES.EbookRepository.DAL.Providers
{
   public class CategoryProvider:ICategoryProvider
    {
        private static string _connectionString = @"server=localhost;user id=root;password=root;database=ebook;";
        private static string _tableName = "categories";

        public IEnumerable<Category> GetAll()
        {
            List<Category> categories = new List<Category>();

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
                            Category category = null;

                            while (reader.Read())
                            {
                                category = MapTableEnityToObject(reader);
                                categories.Add(category);
                            }
                        }
                    }
                }

            }

            return categories;
        }

        public Category GetById(int id)
        {
            Category category = new Category();

            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                string query = String.Format("SELECT * FROM {0} WHERE CategoryId = @Id;", _tableName);

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
                                category = MapTableEnityToObject(reader);
                            }
                        }
                    }
                }
            }

            return category;
        }

        public int Create(Category category)
        {
            int newCategoryId = 0;
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                string query = String.Format("INSERT INTO {0}" +
                    "(Name)" +
                    "VALUES(@Name); Select LAST_INSERT_ID();", _tableName);

                sqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("Name", category.Name);

                    newCategoryId = int.Parse(sqlCommand.ExecuteScalar().ToString());
                }
            }

            return newCategoryId;
        }

        public int Update(Category categoory)
        {
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                string query = String.Format("UPDATE {0} SET " +
                    "Name = @Name" +
                    " WHERE CategoryId = @Id", _tableName);

                sqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("Id", categoory.CategoryId);
                    sqlCommand.Parameters.AddWithValue("Name", categoory.Name);

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

            return categoory.CategoryId;
        }

        public int Delete(int id)
        {
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                string query = String.Format("DELETE FROM {0} WHERE CategoryId = @Id", _tableName);

                sqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("Id", id);

                    int deletedRows = sqlCommand.ExecuteNonQuery();
                    if (deletedRows > 0)
                    {
                        Console.WriteLine("Category deleted");
                    }
                    else
                    {
                        Console.WriteLine("Not deleted");
                    }
                }
            }

            return id;
        }

        private Category MapTableEnityToObject(IDataRecord record)
        {
            Category result = new Category();

            result.CategoryId = (int)record["CategoryId"];
            result.Name = (string)record["Name"];

            return result;
        }
    }
}
