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
                        Contents = reader["Contents"].ToString(),
                        GiftHint = reader["GiftHint"].ToString(),
                        ColorWrappingPaper = reader["ColorWrappingPaper"].ToString(),
                        Height = reader["Height"] as double?,
                        Width =  reader["Width"] as double?,
                        Depth =  reader["Depth"] as double?,
                        Weight =  reader["Weight"] as double?,
                        IsOpened = reader["IsOpened"] as bool?,
                    });
                }
                connection.Close();
            }
            return rv;
        }

        //Gets Individual Gift by Id number
        public List<Gift> GetAllGifts(int id)
        {
            var rv = new List<Gift>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = $@"SELECT * FROM GiftTable WHERE Id={id}";
                var cmd = new SqlCommand(query, connection);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    rv.Add(new Gift
                    {
                        Contents = reader["Contents"].ToString(),
                        GiftHint = reader["GiftHint"].ToString(),
                        ColorWrappingPaper = reader["ColorWrappingPaper"].ToString(),
                        Height = reader["Height"] as double?,
                        Width = reader["Width"] as double?,
                        Depth = reader["Depth"] as double?,
                        Weight = reader["Weight"] as double?,
                        IsOpened = reader["IsOpened"] as bool?,
                    });
                }
                connection.Close();
            }
            return rv;
        }


        //Creates New Gift And Adds It To Pile
        public void CreateGift(Gift gift)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = @"INSERT INTO Gift 
                            ([Contents],[GiftHint],[ColorWrappingPaper],[Height],[Width],[Depth],[Weight])
                            OUTPUT INSERTED.Id                          
                            VALUES (@Contents, @GiftHint, @ColorWrappingPaper, @Height, @Width, @Depth, @Weight)";

                var cmd = new SqlCommand(query, connection);
                connection.Open();

                cmd.Parameters.AddWithValue("@Contents", gift.Contents);
                cmd.Parameters.AddWithValue("@GiftHint", gift.GiftHint);
                cmd.Parameters.AddWithValue("@ColorWrappingPaper", gift.ColorWrappingPaper);
                cmd.Parameters.AddWithValue("@Height", gift.Height);
                cmd.Parameters.AddWithValue("@Width", gift.Width);
                cmd.Parameters.AddWithValue("@Depth", gift.Depth);
                cmd.Parameters.AddWithValue("@Weight", gift.Weight);
                cmd.Parameters.AddWithValue("@IsOpened", false);

                var newId = cmd.ExecuteScalar();
                gift.Id = (int)newId;
                connection.Close();
            }
            return Ok(gift);
        }


        //Updates Existing gift In Catalog
        public void Updategift(int id, Gift gift)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = @"UPDATE [dbo].[Gifts] 
                            SET [Title] = @Title,
                            [Author] = @Author,
                            [YearPublished] = @YearPublished,
                            [Genre] = @Genre                  
                            WHERE Id = @Id";

                var cmd = new SqlCommand(query, connection);
                connection.Open();

                cmd.Parameters.AddWithValue("@Contents", gift.Contents);
                cmd.Parameters.AddWithValue("@GiftHint", gift.GiftHint);
                cmd.Parameters.AddWithValue("@ColorWrappingPaper", gift.ColorWrappingPaper);
                cmd.Parameters.AddWithValue("@Height", gift.Height);
                cmd.Parameters.AddWithValue("@Width", gift.Width);
                cmd.Parameters.AddWithValue("@Depth", gift.Depth);
                cmd.Parameters.AddWithValue("@Weight", gift.Weight);
                cmd.Parameters.AddWithValue("@IsOpened", gift.IsOpened);

                var reader = cmd.ExecuteNonQuery();
                connection.Close();
            }
            return Ok($"Gift with Id {id} has been updated to {gift.Contents}");
        }


        //Deletes gift From Catalog
        public IHttpActionResult DeleteGift(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = $@"DELETE FROM Gifts WHERE Id = {id}";
                var cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                return Ok($"gift with Id {id} has been deleted from the catalog");
            }
        }
    }
}