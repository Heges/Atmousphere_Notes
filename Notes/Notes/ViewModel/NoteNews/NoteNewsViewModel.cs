using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Notes.Models;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using Notes.Views;
using Xamarin.Forms;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace Notes.ViewModel
{
    public class NoteNewsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<NoteNews> NoteNews { get; set; }
        public ObservableRangeCollection<Grouping<string, NoteNews>> NoteNewsGroups { get; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }
        public MvvmHelpers.Commands.Command LoadMoreCommand { get; }
        public MvvmHelpers.Commands.Command DelayLoadMoreCommand { get; }
        public MvvmHelpers.Commands.Command ClearCommand { get; }

        private const int ITEMAVAIBLE = 3;

        public NoteNewsViewModel()
        {
            Title = "SOME TITLE";

            NoteNews = new ObservableRangeCollection<NoteNews>();
            NoteNewsGroups = new ObservableRangeCollection<Grouping<string, NoteNews>>();

            LoadMore();

            RefreshCommand = new AsyncCommand(Refresh);
            LoadMoreCommand = new MvvmHelpers.Commands.Command(LoadMore);
            DelayLoadMoreCommand = new MvvmHelpers.Commands.Command(Delay);
            ClearCommand = new MvvmHelpers.Commands.Command(Clear);
            SelectedCommand = new AsyncCommand<object>(Selected);
        }

        private async void LoadMore()
        {
            var notesNewses = await App.NotesDB.GetNotesNewsesAsync();
            if (notesNewses == null || notesNewses.Count == 0)
            {
                await LoadDataFromBackend();
                notesNewses = await App.NotesDB.GetNotesNewsesAsync();
            }
            if (NoteNews.Count < notesNewses.Count)
            {
                for (int i = NoteNews.Count, count = ITEMAVAIBLE; i < notesNewses.Count; i++, count--)
                {
                    if (count > 0)
                    {
                        NoteNews.Add(notesNewses[i]);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            NoteNewsGroups.Clear();
            NoteNewsGroups.Add(new Grouping<string, NoteNews>("", NoteNews));
        }

        private async Task Refresh()
        {
            IsBusy = true;
            //await Task.Delay(2000);
            await LoadDataFromBackend();
            NoteNews.Clear();
            //Clear();
            LoadMore();
            IsBusy = false;
        }

        
        private async Task LoadDataFromBackend()
        {
            await App.NotesDB.DeleteAllNoteNewsAsync();
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://atmousphere.somee.com/api/Notes");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                HttpContent responseContent = response.Content;
                var json = await responseContent.ReadAsStringAsync();
                IEnumerable<NoteNews> result = JsonConvert.DeserializeObject<IEnumerable<NoteNews>>(json);

                foreach (var item in result)
                {
                    await App.NotesDB.SaveNoteNewsAsync(item);
                }
            }
        }

        private void Clear()
        {
            NoteNews.Clear();
            NoteNewsGroups.Clear();
        }


        private NoteNews noteNews;
        public NoteNews CurrentNoteNews { 
            get => noteNews;
            set => base.SetProperty(ref noteNews, value);
        }

        public async Task Selected(object obj)
        {
            if (obj is SelectedItemChangedEventArgs  ev)
            {
                if (ev.SelectedItem is NoteNews nw)
                {
                    CurrentNoteNews = nw;
                    await Shell.Current.GoToAsync($"{nameof(AddNoteNewsPage)}?ItemId={nw.NoteNewsId}");
                    //await Application.Current.MainPage.DisplayAlert("Selected", noteNews.Title, "ОТВИНТА");
                }
            }
            return;
        }

        private void Delay()
        {
            if (NoteNews.Count <= ITEMAVAIBLE)
                return;

            LoadMore();
        }
    }
}
