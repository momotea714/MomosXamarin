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
using Prism.Navigation;
using Xamarin.Forms;

namespace OXamarin.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private bool _isRefresh;
        public bool IsRefresh
        {
            get { return _isRefresh; }
            set { SetProperty(ref _isRefresh, value); }
        }

        private List<LargeClassification> _items;
        public List<LargeClassification> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        #region コマンド
        public DelegateCommand ItemRefreshCommand { get; set; }
        public ICommand ItemSelectedCommand { get; set; }
        #endregion

        //こういうフィールド追加するよ
        private readonly INavigationService _navigationService;

        //コンストラクタにINavigationSerivece型の引数を受け取るようにします
        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            //コマンドの設定をしますよ
            //Reflesh時に発火
            ItemRefreshCommand = new DelegateCommand(
                () =>
                {
                    //Insert();
                    GetLargeClassification();

                    IsRefresh = false;
                });
            //Insert();

            GetLargeClassification();

            ItemSelectedCommand = new Command<LargeClassification>(
                x =>
            {
                var navigationParameter = new NavigationParameters
                {
                    {SecondPageViewModel.LargeClassificationCD, x.id.ToString()},
                };
                _navigationService.NavigateAsync("SecondPage", navigationParameter);
            });

            //URLに対応するModelの変換型
            //URLAndType = new Dictionary<string, Type>
            //{
            //    {"https://favoritelist-mmtr.c9users.io/get_favoritelist", typeof(Favorite)},
            //};

            //foreach (var item in URLAndType)
            //{
            //    var jsonResult = GetJsonResult(item.Key);
            //    var hoge = Desirialize(jsonResult);
            //    var fuga = hoge.Select( x => (Favorite)x);

            //}
        }

        //private T Test<T>(Type T)
        //{
        //    return T;
        //}

//        private void Insert()
//        {

//            var content = new FormUrlEncodedContent(new Dictionary<string, string>
//{
//                {"MiddleClassificationCD","1"},
//                {"title",DateTime.Now.ToString()},
//                {"content","testやで〜"}
//});
//            var httpClient = new HttpClient();
//            var response = httpClient.PostAsync("https://favoritelist-mmtr.c9users.io/insert_favoritelist", content).Result;
//        }

        private void GetLargeClassification()
        {
            Items = null;
            var kk = Download<LargeClassification>("https://favoritelist-mmtr.c9users.io/get_largeclassification");
            _items = (List<LargeClassification>)kk;
        }

        public IEnumerable<T> Download<T>(string url)
        {
            var httpClient = new HttpClient();
            var jsonresult = httpClient.GetStringAsync(url).Result;
            var hoge = JsonConvert.DeserializeObject<List<T>>(@jsonresult);

            return hoge;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        //        public IEnumerable<LargeClassification> Download1()
        //        {
        //            var httpClient = new HttpClient();
        //            var jsonresult = httpClient.GetStringAsync("https://favoritelist-mmtr.c9users.io/get_favoritelist").Result;
        //            var hoge = JsonConvert.DeserializeObject<List<LargeClassification>>(@jsonresult);

        //            return hoge;
        //        }

        //        public string GetJsonResult(string url)
        //        {
        //            var httpClient = new HttpClient();
        //            return httpClient.GetStringAsync(url).Result;
        //        }

        //        public IEnumerable<object> Desirialize(string jsonResult)
        //         { 
        //            return  JsonConvert.DeserializeObject<List<object>>(@jsonResult);
        //}
    }
}