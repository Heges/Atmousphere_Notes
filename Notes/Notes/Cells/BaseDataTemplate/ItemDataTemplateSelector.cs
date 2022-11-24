using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Notes.Models;

namespace Notes.Cells
{
    class ItemDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Normal { get; set; }
        public DataTemplate Special { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            //var nw = (NoteNews)item;
            //return nw.Title == "Some title 00" ? Special : Normal;

            return Normal;
        }
    }
}
