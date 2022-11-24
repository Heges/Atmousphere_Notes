using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Notes.Models
{
    public class User
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        [MaxLength(16)]
        public string Email { get; set; }
        [MaxLength(16)]
        public string Password { get; set; }
    }
}
