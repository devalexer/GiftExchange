using GiftExchange.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace GiftExchange.Services
{
    public class GiftService
    {
        const string ConnectionString = @"Server=localhost\SQLEXPRESS;Database=GiftExchange;Trusted_Connection=True;";

        //Gets List Of All Gifts
        public List<Gift> GetAllGifts()
        {
            var rv = new List<Gift>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = "SELECT * FROM GiftTable";
                var cmd = new SqlCommand(query, connection);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    rv.Add(new Gift
                    {
                        Contents = (string)reader["Contents"],
                        GiftHint = (string)reader["GiftHint"],
                        ColorWrappingPaper = (string)reader["ColorWrappingPaper"],
                        Height = (double?)reader["Height"],
                        Width = (double?)reader["Width"],
                        Depth = (double?)reader["Depth"],
                        Weight = (double?)reader["Weight"],
                        IsOpened = (bool?)reader["IsOpened"],
                    });
                }
                connection.Close();
            }
            return rv;
        }

        ////Gets Individual Book by Id number
        //public IEnumerable<GiftService> GetGift(int id)
        //{
        //    var rv = new List<GiftService>();
        //    using (var connection = new SqlConnection(ConnectionString))
        //    {
        //        var query = $@"SELECT * FROM GiftTable WHERE Id={id}";
        //        var cmd = new SqlCommand(query, connection);
        //        connection.Open();
        //        var reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            rv.Add(new Gift
        //            {
        //                Id = (int)reader["Id"],
        //                Contents = reader["Contents"].ToString,
        //                GiftHint = reader["GiftHint"].ToString,
        //                ColorWrappingPaper = reader["ColorWrappingPaper"].ToString,
        //                Height = (double)reader["Height"],
        //                Width = (double)reader["Width"],
        //                Depth = (double)reader["Depth"],
        //                Weight = (double)reader["Weight"],
        //                IsOpened = (bool)reader["IsOpened"],
        //            });
        //        }
        //        connection.Close();
        //    }
        //    return rv;
        //}

        ////Creates New Book And Adds It To Catalog
        //public IHttpActionResult CreateBook([FromBody]GiftService book)
        //{
        //    using (var connection = new SqlConnection(ConnectionString))
        //    {
        //        var query = @"INSERT INTO[dbo].[Gift] ([Contents],[GiftHint],[ColorWrappingPaper],[Height],[Width],[Depth],[Weight])
        //                      OUTPUT INSERTED.Id                          
        //                      VALUES (@Contents, @GiftHint, @ColorWrappingPaper, @Height, @Width, @Depth, @Weight)";
        //        var cmd = new SqlCommand(query, connection);
        //        connection.Open();
        //        cmd.Parameters.AddWithValue("@Contents", gift.Contents);
        //        cmd.Parameters.AddWithValue("@Author", book.Author);
        //        cmd.Parameters.AddWithValue("@YearPublished", book.YearPublished);
        //        cmd.Parameters.AddWithValue("@Genre", book.Genre);
        //        var newId = cmd.ExecuteScalar();
        //        book.Id = (int)newId;
        //        connection.Close();
        //    }
        //    return Ok(book);
        //}

        ////Updates Existing Book In Catalog
        //public IHttpActionResult UpdateBook([FromUri]int id, [FromBody] GiftService book)
        //{
        //    using (var connection = new SqlConnection(ConnectionString))
        //    {
        //        var query = @"UPDATE [dbo].[Books] 
        //                    SET [Title] = @Title,[Author] = @Author,[YearPublished] = @YearPublished,[Genre] = @Genre                  
        //                    WHERE Id = @Id";
        //        var cmd = new SqlCommand(query, connection);
        //        connection.Open();
        //        cmd.Parameters.AddWithValue("@Id", book.Id);
        //        cmd.Parameters.AddWithValue("@Title", book.Title);
        //        cmd.Parameters.AddWithValue("@Author", book.Author);
        //        cmd.Parameters.AddWithValue("@YearPublished", book.YearPublished);
        //        cmd.Parameters.AddWithValue("@Genre", book.Genre);
        //        var reader = cmd.ExecuteNonQuery();
        //        connection.Close();
        //    }
        //    return Ok($"Book with Id {id} has been updated to {book.Title}");
        //}

        ////Deletes Book From Catalog
        //public IHttpActionResult DeleteBook(int id)
        //{
        //    using (var connection = new SqlConnection(ConnectionString))
        //    {
        //        var query = $@"DELETE FROM[dbo].[Books] WHERE Id = {id}";
        //        var cmd = new SqlCommand(query, connection);
        //        connection.Open();
        //        cmd.ExecuteNonQuery();
        //        connection.Close();

        //        return Ok($"Book with Id {id} has been deleted from the catalog");
        //    }
        //}
    }
}