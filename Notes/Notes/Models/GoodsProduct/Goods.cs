using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models
{
    public class Goods
    {
        [PrimaryKey, AutoIncrement]
        public int GoodsId { get; set; }
        [ForeignKey(typeof(GoodsCategory))]
        public Nullable<int> GoodsCategoryId { get; set; }
        public string Name { get; set; }
        public Nullable<double> Price { get; set; }
        public byte Active { get; set; }
        public string ImageUrl { get; set; }

        [JsonIgnore]
        [ManyToOne]
        public GoodsCategory category
        {
            get; set;
        }
    }
}
