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
    public class ServiceViewModel : BaseViewModel
    {
        public ObservableRangeCollection<ServiceCategory> ServiceList { get; set; }
        public ObservableRangeCollection<Grouping<string, Service>> ServiceGroup { get; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }
        public MvvmHelpers.Commands.Command LoadMoreCommand { get; }
        public MvvmHelpers.Commands.Command DelayLoadMoreCommand { get; }
        public MvvmHelpers.Commands.Command ClearCommand { get; }

        private const int ITEMAVAIBLE = 3;

        public ServiceViewModel()
        {
            Title = "Каталог услуг";

            ServiceList = new ObservableRangeCollection<ServiceCategory>();
            ServiceGroup = new ObservableRangeCollection<Grouping<string, Service>>();

            LoadMore();

            RefreshCommand = new AsyncCommand(Refresh);
            LoadMoreCommand = new MvvmHelpers.Commands.Command(LoadMore);
            DelayLoadMoreCommand = new MvvmHelpers.Commands.Command(Delay);
            ClearCommand = new MvvmHelpers.Commands.Command(Clear);
            SelectedCommand = new AsyncCommand<object>(Selected);
        }

        private async void LoadMore()
        {
            var categories = await App.NotesDB.GetServiceCategoriesAsync();
            if (categories == null || categories.Count == 0)
            {
                await LoadDataFromBackend();
                var services = await App.NotesDB.GetServicesAsync();
                categories = await App.NotesDB.GetServiceCategoriesAsync();
            }
            if (ServiceList.Count < categories.Count)
            {
                for (int i = ServiceList.Count, count = ITEMAVAIBLE; i < categories.Count; i++, count--)
                {
                    if (count > 0)
                    {
                        ServiceList.Add(categories[i]);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            ServiceGroup.Clear();
            foreach (var item in ServiceList)
            {
                ServiceGroup.Add(new Grouping<string, Service>(item.Name, item.Services));
            }
        }

        private async Task Refresh()
        {
            IsBusy = true;
            //await Task.Delay(2000);
            await LoadDataFromBackend();
            ServiceList.Clear();
            //Clear();
            LoadMore();
            IsBusy = false;
        }


        private async Task LoadDataFromBackend()
        {
            await App.NotesDB.DeleteAllCategoryServices();
            await App.NotesDB.DeleteAllServices();
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://atmousphere.somee.com/api/Services");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                HttpContent responseContent = response.Content;
                var json = await responseContent.ReadAsStringAsync();
                IEnumerable<ServiceCategory> result = JsonConvert.DeserializeObject<IEnumerable<ServiceCategory>>(json);

                foreach (var item in result)
                {
                   await App.NotesDB.SaveServiceCategoryAsync(item);
                }
            }
        }

        private void Clear()
        {
            ServiceList.Clear();
            ServiceGroup.Clear();
        }


        private Service service;
        public Service CurrentService
        {
            get => service;
            set => base.SetProperty(ref service, value);
        }

        public async Task Selected(object obj)
        {
            if (obj is SelectedItemChangedEventArgs ev)
            {
                if (ev.SelectedItem is Service nw)
                {
                    CurrentService = nw;
                    //await Shell.Current.GoToAsync($"{nameof(AddNoteNewsPage)}?ItemId={nw.ServiceCategoryId}");
                    await Application.Current.MainPage.DisplayAlert("Selected", nw.Name, "ОТВИНТА");
                }
            }
            return;
        }

        private void Delay()
        {
            if (ServiceList.Count <= ITEMAVAIBLE)
                return;

            LoadMore();
        }
    }
}
