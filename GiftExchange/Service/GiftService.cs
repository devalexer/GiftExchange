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
                    rv.Add(new Gift(reader));
                }
                connection.Close();
            }
            return rv;
        }

        //Gets Individual Gift by Id number
        public Gift GetGift(int id)
        {
            Gift gift = null;
            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = $@"SELECT * FROM GiftTable WHERE Id={id}";
                var cmd = new SqlCommand(query, connection);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    gift = new Gift(reader);
                    //rv.Add(new Gift
                    //{
                    //    Contents = reader["Contents"].ToString(),
                    //    GiftHint = reader["GiftHint"].ToString(),
                    //    ColorWrappingPaper = reader["ColorWrappingPaper"].ToString(),
                    //    Height = reader["Height"] as double?,
                    //    Width = reader["Width"] as double?,
                    //    Depth = reader["Depth"] as double?,
                    //    Weight = reader["Weight"] as double?,
                    //    IsOpened = reader["IsOpened"] as bool?,
                    //});
                }
                connection.Close();
            }
            return gift;
        }


        //Creates New Gift And Adds It To Pile
        public static Gift CreateGift(Gift gift)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = @"INSERT INTO Gift 
                            ([Contents],[GiftHint],[ColorWrappingPaper],[Height],[Width],[Depth],[Weight])
                            OUTPUT INSERTED.Id                          
                            VALUES (@Contents, @GiftHint, @ColorWrappingPaper, @Height, @Width, @Depth, @Weight)";

                var cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@Contents", gift.Contents);
                cmd.Parameters.AddWithValue("@GiftHint", gift.GiftHint);
                cmd.Parameters.AddWithValue("@ColorWrappingPaper", gift.ColorWrappingPaper);
                cmd.Parameters.AddWithValue("@Height", gift.Height);
                cmd.Parameters.AddWithValue("@Width", gift.Width);
                cmd.Parameters.AddWithValue("@Depth", gift.Depth);
                cmd.Parameters.AddWithValue("@Weight", gift.Weight);
                cmd.Parameters.AddWithValue("@IsOpened", false);

                connection.Open();
                var newId = cmd.ExecuteScalar();
                gift.Id = (int)newId;
                connection.Close();
            }
            return gift;
        }


        //Updates Existing Gift In Catalog
        public static Gift UpdateGift(int id, Gift gift)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = @"UPDATE Gifts 
                            VALUES Contents = @Contents,
                            GiftHint = @GiftHint,
                            ColorWrappingPaper = @ColorWrappingPaper,
                            Height = @Height,
                            Width = @Width,
                            Weight = @Weight,
                            Depth = @Depth,
                            IsOpened = @IsOpened
                            WHERE @Id = Id";

                var cmd = new SqlCommand(query, connection);
                
                cmd.Parameters.AddWithValue("@Contents", gift.Contents);
                cmd.Parameters.AddWithValue("@GiftHint", gift.GiftHint);
                cmd.Parameters.AddWithValue("@ColorWrappingPaper", gift.ColorWrappingPaper);
                cmd.Parameters.AddWithValue("@Height", gift.Height);
                cmd.Parameters.AddWithValue("@Width", gift.Width);
                cmd.Parameters.AddWithValue("@Depth", gift.Depth);
                cmd.Parameters.AddWithValue("@Weight", gift.Weight);
                cmd.Parameters.AddWithValue("@IsOpened", gift.IsOpened);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            return gift;
        }


        //Deletes gift From Catalog
        public static Gift DeleteGift(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = $@"DELETE * FROM Gifts WHERE Id = {id}";
                var cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

            }
            Gift gift = null;
            return gift;
        }

        ////Opens Gift from Catalog
        //public static void OpenGift(int id)
        //{
        //    using (var connection = new SqlConnection(ConnectionString))
        //    {
        //        var query = @"UPDATE Gifts (IsOpened = @IsOpened) WHERE @Id = Id";
        //        var cmd = new SqlCommand(query, connection);

        //        cmd.Parameters.AddWithValue("@IsOpened", true);

        //        connection.Open();
        //        cmd.ExecuteNonQuery();
        //        connection.Close();
        //    }
        //    return gift;
        //}
    }
}