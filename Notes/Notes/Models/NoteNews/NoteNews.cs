using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models
{
    public class NoteNews
    {
        [AutoIncrement, PrimaryKey]
        public int NoteNewsId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateCreation { get; set; }
        public byte Active { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string HalfDecription { get; set; }
    }
}
