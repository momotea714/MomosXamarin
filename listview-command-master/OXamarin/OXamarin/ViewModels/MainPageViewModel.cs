using Prism.Mvvm;
using System.Collections.Generic;
using OXamarin.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using System.Windows.Input;
using Prism.Commands;

namespace OXamarin.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private bool _isRefresh;
        public bool IsRefresh
        {
            get { return _isRefresh; }
            set { SetProperty(ref _isRefresh, value); }
        }

        private List<string> _items;
        public List<string> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }
        //private List<Favorite> tmp;

        #region コマンド
        public DelegateCommand ItemRefreshCommand { get; set; }
        #endregion

        public MainPageViewModel()
        {
            //コマンドの設定をしますよ
            //Reflesh時に発火
            ItemRefreshCommand = new DelegateCommand(
                () =>
                {
                    Insert();
                    GetDownload();
                
                    IsRefresh = false;
                });
            //var a = LoadData();
            //var favoroteList = getData(new WebData());


            //var ll = GetTodoItemsAsync();
            Insert();
            
            GetDownload();
        }


        private void Insert()
        {

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
{
                {"categoryCD","1"},
                {"title",DateTime.Now.ToString()},
                {"content","testやで〜"}
});
            var httpClient = new HttpClient();
            var response = httpClient.PostAsync("https://favoritelist-mmtr.c9users.io/insert_favoritelist", content).Result;
        }

        private void GetDownload()
        {
            Items = null;
            var kk = Download();
            _items = kk.Select(x => x.title).ToList();
        }

        public IEnumerable<Favorite> Download()
        {
            var httpClient = new HttpClient();
            var jsonresult = httpClient.GetStringAsync("https://favoritelist-mmtr.c9users.io/get_favoritelist").Result;
            var hoge = JsonConvert.DeserializeObject<List<Favorite>>(@jsonresult);

            return hoge;
        }
    }


}