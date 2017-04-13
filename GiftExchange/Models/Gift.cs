using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GiftExchange.Models
{
    public class Gift
    {
        public int Id { get; set; }
        public string Contents { get; set; }
        public string GiftHint { get; set; }
        public string ColorWrappingPaper { get; set; }
        public double? Height { get; set; }
        public double? Width { get; set; }
        public double? Depth { get; set; }
        public double? Weight { get; set; }
        public bool? IsOpened { get; set; }

        public Gift() { }

        public Gift(SqlDataReader reader)
        {
            this.Id = (int)reader["Id"];
            this.Contents = reader["Contents"].ToString();
            this.GiftHint = reader["GiftHint"].ToString();
            this.ColorWrappingPaper = reader["ColorWrappingPaper"].ToString();
            this.Height = double.Parse(reader["Height"].ToString()); 
            this.Width = double.Parse(reader["Width"].ToString());
            this.Depth = double.Parse(reader["Depth"].ToString()); 
            this.Weight = double.Parse(reader["Weight"].ToString());
            this.IsOpened = reader["IsOpened"] as bool?;
        }

        //Constructor that holds the data coming from the forms
        public Gift(FormCollection collection, int id = 0)
        {
            if (id > 0)
            {
                this.Id = id;
            }
            else
            {
                int _id;
                int.TryParse(collection["Id"], out _id);
                if (_id > 0)
                {
                    this.Id = _id;
                }
            }
            //this.Id = int.Parse(collection["Id"]);
            this.Contents = collection["Contents"];
            this.GiftHint = collection["GiftHint"];
            this.ColorWrappingPaper = collection["ColorWrappingPaper"];
            this.Height = double.Parse(collection["Height"]);
            this.Width = double.Parse(collection["Width"]);
            this.Depth = double.Parse(collection["Depth"]);
            this.Weight = double.Parse(collection["Weight"]);
            this.IsOpened = bool.Parse(collection["IsOpened"]);
        }


    }
}