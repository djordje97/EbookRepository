using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UES.EbookRepository.DAL.Contract.Contracts;
using UES.EbookRepository.DAL.Contract.Entities;

namespace UES.EbookRepository.DAL.Providers
{
   public class LanguageProvider:ILanguageProvider
    {
        private static string _connectionString = @"server=localhost;user id=root;password=root;database=ebook;";
        private static string _tableName = "languages";

        public IEnumerable<Language> GetAll()
        {
            List<Language> languages = new List<Language>();

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
                            Language language = null;

                            while (reader.Read())
                            {
                                language = MapTableEnityToObject(reader);
                                languages.Add(language);
                            }
                        }
                    }
                }

            }

            return languages;
        }

        public Language GetById(int id)
        {
            Language language = new Language();

            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                string query = String.Format("SELECT * FROM {0} WHERE LanguageId = @Id;", _tableName);

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
                                language = MapTableEnityToObject(reader);
                            }
                        }
                    }
                }
            }

            return language;
        }

        public int Create(Language language)
        {
            int newLanguageId = 0;
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                string query = String.Format("INSERT INTO {0}" +
                    "(Name)" +
                    "VALUES(@Name); Select LAST_INSERT_ID();", _tableName);

                sqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("Name", language.Name);

                    newLanguageId = int.Parse(sqlCommand.ExecuteScalar().ToString());
                }
            }

            return newLanguageId;
        }

        public int Update(Language language)
        {
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                string query = String.Format("UPDATE {0} SET " +
                    "Name = @Name" +
                    " WHERE LanguageId = @Id", _tableName);

                sqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("Id", language.LanguageId);
                    sqlCommand.Parameters.AddWithValue("Name", language.Name);

                    int updatedRows = sqlCommand.ExecuteNonQuery();
                    if (updatedRows > 0)
                    {
                        Console.WriteLine("Language updated");
                    }
                    else
                    {
                        Console.WriteLine("Language not updated");
                    }
                }
            }

            return language.LanguageId;
        }

        public int Delete(int id)
        {
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                string query = String.Format("DELETE FROM {0} WHERE LanguageId = @Id", _tableName);

                sqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", id);

                    int deletedRows = sqlCommand.ExecuteNonQuery();
                    if (deletedRows > 0)
                    {
                        Console.WriteLine("Language deleted");
                    }
                    else
                    {
                        Console.WriteLine("Not deleted");
                    }
                }
            }

            return id;
        }

        private Language MapTableEnityToObject(IDataRecord record)
        {
            Language result = new Language();

            result.LanguageId = (int)record["LanguageId"];
            result.Name = (string)record["Name"];

            return result;
        }
    }
}

