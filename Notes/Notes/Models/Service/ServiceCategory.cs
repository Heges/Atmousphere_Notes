using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models
{
    public class ServiceCategory
    {
        [PrimaryKey, AutoIncrement]
        public int ServiceCategoryId { get; set; }
        public string Name { get; set; }
        public byte Active { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Service> Services { get; set; }
    }
}
