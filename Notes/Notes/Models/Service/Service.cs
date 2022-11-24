using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models
{
    public class Service
    {
        [PrimaryKey, AutoIncrement]
        public int ServiceId { get; set; }
        [ForeignKey(typeof(ServiceCategory))]
        public Nullable<int> ServiceCategoryId { get; set; }
        public string Name { get; set; }
        public TimeSpan? RequiredTime { get; set; }
        public Nullable<double> PriceFrom { get; set; }
        public Nullable<double> PriceTo { get; set; }
        public byte Active { get; set; }
        public string ImageUrl { get; set; }

        [JsonIgnore]
        [ManyToOne]
        public ServiceCategory category
        {
            get; set;
        }
    }
}
