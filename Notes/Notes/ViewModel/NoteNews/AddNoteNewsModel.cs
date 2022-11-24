using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using Notes.Models;
using Notes.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;

namespace Notes.ViewModel
{
    public class AddNoteNewsModel : BaseViewModel, IQueryAttributable, INotifyPropertyChanged
    {
        public ObservableRangeCollection<NoteNews> CollectionList { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public string ItemId { get; set; }

        private NoteNews noteNews;
        public NoteNews CurrentNoteNews
        {
            get => noteNews;
            set => base.SetProperty(ref noteNews, value);
        }

        public AddNoteNewsModel()
        {
            CollectionList = new ObservableRangeCollection<NoteNews>();
            RefreshCommand = new AsyncCommand(Refresh);
        }

        public async void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            ItemId = HttpUtility.UrlDecode(query["ItemId"]);
            var index = int.Parse(ItemId);
            var localNoteNews = await App.NotesDB.GetNoteNewsAsync(index);
            CurrentNoteNews = localNoteNews;
            CollectionList.Add(localNoteNews);
            OnPropertyChanged("CurrentNoteNews");
        }

        private async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            IsBusy = false;
        }
    }
}
