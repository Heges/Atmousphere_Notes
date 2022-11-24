using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Notes.Models;
using System.Threading.Tasks;
using Notes.Views;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Notes.ViewModel
{
    public class GoodsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<GoodsCategory> GoodsList { get; set; }
        public ObservableRangeCollection<Grouping<string, Goods>> GoodsGroup { get; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }
        public MvvmHelpers.Commands.Command LoadMoreCommand { get; }
        public MvvmHelpers.Commands.Command DelayLoadMoreCommand { get; }
        public MvvmHelpers.Commands.Command ClearCommand { get; }

        private const int ITEMAVAIBLE = 3;

        public GoodsViewModel()
        {
            Title = "Каталог товаров";

            GoodsList = new ObservableRangeCollection<GoodsCategory>();
            GoodsGroup = new ObservableRangeCollection<Grouping<string, Goods>>();

            LoadMore();

            RefreshCommand = new AsyncCommand(Refresh);
            LoadMoreCommand = new MvvmHelpers.Commands.Command(LoadMore);
            DelayLoadMoreCommand = new MvvmHelpers.Commands.Command(Delay);
            ClearCommand = new MvvmHelpers.Commands.Command(Clear);
            SelectedCommand = new AsyncCommand<object>(Selected);
        }

        private async void LoadMore()
        {
            var categories = await App.NotesDB.GetGoodsCategoriesAsync();
            if (categories == null || categories.Count == 0)
            {
                await LoadDataFromBackend();
                categories = await App.NotesDB.GetGoodsCategoriesAsync();
            }
            if (GoodsList.Count < categories.Count)
            {
                for (int i = GoodsList.Count, count = ITEMAVAIBLE; i < categories.Count; i++, count--)
                {
                    if (count > 0)
                    {
                        GoodsList.Add(categories[i]);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            GoodsGroup.Clear();
            foreach (var item in GoodsList)
            {
                GoodsGroup.Add(new Grouping<string, Goods>(item.Name, item.Goods));
            }
        }

        private async Task Refresh()
        {
            IsBusy = true;
            //await Task.Delay(2000);
            await LoadDataFromBackend();
            GoodsList.Clear();
            //Clear();
            LoadMore();
            IsBusy = false;
        }


        private async Task LoadDataFromBackend()
        {
            await App.NotesDB.DeleteAllGoodsCategories();
            await App.NotesDB.DeleteAllGoods();
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://atmousphere.somee.com/api/Goods");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                HttpContent responseContent = response.Content;
                var json = await responseContent.ReadAsStringAsync();
                IEnumerable<GoodsCategory> result = JsonConvert.DeserializeObject<IEnumerable<GoodsCategory>>(json);

                foreach (var item in result)
                {
                    await App.NotesDB.SaveGoodsCategoryWithChildrenAsync(item);
                }
            }
        }

        private void Clear()
        {
            GoodsList.Clear();
            GoodsGroup.Clear();
        }


        private Goods goods;
        public Goods CurrentGoods
        {
            get => goods;
            set => base.SetProperty(ref goods, value);
        }

        public async Task Selected(object obj)
        {
            if (obj is SelectedItemChangedEventArgs ev)
            {
                if (ev.SelectedItem is Goods nw)
                {
                    CurrentGoods = nw;
                    //await Shell.Current.GoToAsync($"{nameof(AddNoteNewsPage)}?ItemId={nw.ServiceCategoryId}");
                    await Application.Current.MainPage.DisplayAlert("Selected", nw.Name, "ОТВИНТА");
                }
            }
            return;
        }

        private void Delay()
        {
            if (GoodsList.Count <= ITEMAVAIBLE)
                return;

            LoadMore();
        }
    }
}
