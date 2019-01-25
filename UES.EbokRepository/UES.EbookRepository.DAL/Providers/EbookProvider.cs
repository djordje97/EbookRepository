using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UES.EbookRepository.DAL.Contract.Contracts;
using UES.EbookRepository.DAL.Contract.Entities;

namespace UES.EbookRepository.DAL.Providers
{
    public class EbookProvider:IEbookProvider
    {
        private static string _connectionString = @"server=localhost;user id=root;password=root;database=ebook;";
        private static string _tableName = "ebooks";

        public IEnumerable<Ebook> GetAll()
        {
            List<Ebook> ebooks = new List<Ebook>();

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
                            Ebook ebook = null;

                            while (reader.Read())
                            {
                                ebook = MapTableEnityToObject(reader);
                                ebooks.Add(ebook);
                            }
                        }
                    }
                }

            }

            return ebooks;
        }

        public Ebook GetById(int id)
        {
            Ebook ebook = new Ebook();

            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                string query = String.Format("SELECT * FROM {0} WHERE EbookId = @Id;", _tableName);

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
                                ebook = MapTableEnityToObject(reader);
                            }
                        }
                    }
                }
            }

            return ebook;
        }

        public int Create(Ebook ebook)
        {
            int newEbookId = 0;
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                string query = String.Format("INSERT INTO {0}" +
                    "(Title,Author,Keywords,PublicationYear,Filename,MIME,UserId,CategoryId,LanguageId)" +
                    "VALUES(@Title,@Author,@Keywords,@PublicationYear,@Filename,@MIME,@UserId,@CategoryId,@LanguageId); Select LAST_INSERT_ID();", _tableName);

                sqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("Title", ebook.Title);
                    sqlCommand.Parameters.AddWithValue("Author", ebook.Author);
                    sqlCommand.Parameters.AddWithValue("Keywords", ebook.Keywords);
                    sqlCommand.Parameters.AddWithValue("PublicationYear", ebook.PublicationYear);
                    sqlCommand.Parameters.AddWithValue("Filename", ebook.Filename);
                    sqlCommand.Parameters.AddWithValue("MIME", ebook.MIME);
                    sqlCommand.Parameters.AddWithValue("UserId", ebook.UserId);
                    sqlCommand.Parameters.AddWithValue("CategoryId", ebook.CategoryId);
                    sqlCommand.Parameters.AddWithValue("LanguageId", ebook.LanguageId);
                    newEbookId = int.Parse(sqlCommand.ExecuteScalar().ToString());
                }
            }

            return newEbookId;
        }

        public int Update(Ebook ebook)
        {
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                string query = String.Format("UPDATE {0} SET " +
                    "Title = @Title, Author=@Author ,Keywords=@Keywords ,PublicationYear=@PublicationYear,Filename=@Filename,MIME=@MIME,UserId=@UserId,LanguageId=@LanguageId,CategoryId=@CategoryId" +
                    " WHERE EbookId = @Id", _tableName);

                sqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("Id", ebook.EbookId);
                    sqlCommand.Parameters.AddWithValue("Title", ebook.Title);
                    sqlCommand.Parameters.AddWithValue("Author", ebook.Author);
                    sqlCommand.Parameters.AddWithValue("Keywords", ebook.Keywords);
                    sqlCommand.Parameters.AddWithValue("PublicationYear", ebook.PublicationYear);
                    sqlCommand.Parameters.AddWithValue("Filename", ebook.Filename);
                    sqlCommand.Parameters.AddWithValue("MIME", ebook.MIME);
                    sqlCommand.Parameters.AddWithValue("UserId", ebook.UserId);
                    sqlCommand.Parameters.AddWithValue("CategoryId", ebook.CategoryId);
                    sqlCommand.Parameters.AddWithValue("LanguageId", ebook.LanguageId);
                    int updatedRows = sqlCommand.ExecuteNonQuery();
                    if (updatedRows > 0)
                    {
                        Console.WriteLine("Ebook updated");
                    }
                    else
                    {
                        Console.WriteLine("Ebook not updated");
                    }
                }
            }

            return ebook.CategoryId;
        }

        public int Delete(int id)
        {
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                string query = String.Format("DELETE FROM {0} WHERE EbookId = @Id", _tableName);

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

        private Ebook MapTableEnityToObject(IDataRecord record)
        {
            Ebook result = new Ebook();

            result.EbookId = (int)record["EbookId"];
            result.Title = (string)record["Title"];
            result.Author = (string)record["Author"];
            result.Keywords = (string)record["Keywords"];
            result.PublicationYear = (int)record["PublicationYear"];
            result.Filename = (string)record["Filename"];
            result.MIME = (string)record["MIME"];
            result.UserId = (int)record["UserId"];
            result.CategoryId = (int)record["CategoryId"];
            result.LanguageId = (int)record["LanguageId"];
            return result;
        }
    }
}
