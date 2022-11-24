using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models
{
    public class GoodsCategory
    {
        [PrimaryKey, AutoIncrement]
        public int GoodsCategoryId { get; set; }
        public string Name { get; set; }
        public byte Active { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Goods> Goods { get; set; }
    }
}
