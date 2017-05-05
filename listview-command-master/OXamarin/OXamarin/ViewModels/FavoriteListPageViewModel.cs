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
    public class FavoriteListPageViewModel : BindableBase,INavigatedAware
    {
        public static readonly string MiddleClassificationCD = "MiddleClassificationCD";

        private bool _isRefresh;
        public bool IsRefresh
        {
            get { return _isRefresh; }
            set { SetProperty(ref _isRefresh, value); }
        }

        private List<Favorite> _items;
        public List<Favorite> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        #region コマンド
        public DelegateCommand ItemRefreshCommand { get; set; }
        public ICommand ItemSelectedCommand { get; set; }
        #endregion


        private string _middleClassificationCD;
        public string middleClassificationCD
        {
            get { return _middleClassificationCD; }
            set { SetProperty(ref _middleClassificationCD, value); }
        }

        //こういうフィールド追加するよ
        //private readonly INavigationService _navigationService;

        //コンストラクタにINavigationSerivece型の引数を受け取るようにします
        public FavoriteListPageViewModel(INavigationService navigationService)
        {
            //_navigationService = navigationService;
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

            //ItemSelectedCommand = new Command<MiddleClassification>(
            //    x =>
            //{
            //    var navigationParameter = new NavigationParameters
            //    {
            //        {SecondPageViewModel.LargeClassificationCD, x.id},
            //    };

            //    _navigationService.NavigateAsync("FavoriteListPage", navigationParameter);
            //});
        }

        private void GetLargeClassification()
        {
            Items = null;
            var kk = Download<Favorite>("https://favoritelist-mmtr.c9users.io/get_favoritelist");
            _items = (List<Favorite>)kk;
        }

        public IEnumerable<T> Download<T>(string url)
        {
            var httpClient = new HttpClient();
            var strParams = "?middleclassificationcd=" + _middleClassificationCD;
            var jsonresult = httpClient.GetStringAsync(url + strParams).Result;
            var hoge = JsonConvert.DeserializeObject<List<T>>(@jsonresult);

            return hoge;
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {


        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey(MiddleClassificationCD))
            {
                _middleClassificationCD = parameters[MiddleClassificationCD] as string;
                System.Diagnostics.Debug.WriteLine(_middleClassificationCD);
                var kk = Download<Favorite>("https://favoritelist-mmtr.c9users.io/get_favoritelist");
                Items = (List<Favorite>)kk;
            }
        }
    }
}
