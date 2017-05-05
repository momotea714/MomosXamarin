using Prism.Mvvm;
using System.Collections.Generic;
using OXamarin.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using System;
using System.Runtime;
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
    public class SecondPageViewModel : BindableBase, INavigationAware
    {
        public static readonly string LargeClassificationCD = "LargeClassificationCD";
private string _title;
public string Title
{
	get { return _title; }
	set { SetProperty(ref _title, value); }
}
        private bool _isRefresh;
        public bool IsRefresh
        {
            get { return _isRefresh; }
            set { SetProperty(ref _isRefresh, value); }
        }

//        private List<MiddleClassification> _items2;
//        public List<MiddleClassification> Items2
//{
//	get { return _items2; }
//	set { SetProperty(ref _items2, value); }
//}

        private List<MiddleClassification> _items;
        public List<MiddleClassification> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        #region コマンド
        public DelegateCommand ItemRefreshCommand { get; set; }
        public ICommand ItemSelectedCommand { get; set; }
        #endregion


        private string _largeClassificationCD;
        public string largeClassificationCD
        {
            get { return _largeClassificationCD; }
            set { SetProperty(ref _largeClassificationCD, value); }
        }

        //こういうフィールド追加するよ
        private readonly INavigationService _navigationService;

        //コンストラクタにINavigationSerivece型の引数を受け取るようにします
        public SecondPageViewModel(INavigationService navigationService)
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


            ItemSelectedCommand = new Command<MiddleClassification>(
                x =>
            {
                var navigationParameter = new NavigationParameters
                {
                    {FavoriteListPageViewModel.MiddleClassificationCD, x.id},
                };

                _navigationService.NavigateAsync("FavoriteListPage", navigationParameter);
            }
                    );

//            _items = new List<MiddleClassification>
//            { 
//                new MiddleClassification{Name="ばなな"},
//                new MiddleClassification{Name="どーなつ"},
//            };
        }

        private void GetLargeClassification()
        {
            Items = null;
            var kk = Download("https://favoritelist-mmtr.c9users.io/get_middleclassification");
            _items = (List<MiddleClassification>)kk;
        }

        public List<MiddleClassification> Download(string url)
        {
            var httpClient = new HttpClient();
            var strParams = "?largeclassificationcd=" + _largeClassificationCD;
            var jsonresult = httpClient.GetStringAsync(url + strParams).Result;
            
                var hoge = JsonConvert.DeserializeObject<List<MiddleClassification>>(@jsonresult.ToString());

            return hoge;
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        //    GetLargeClassification();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {


        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey(LargeClassificationCD))
            {
                _largeClassificationCD = parameters[LargeClassificationCD] as string;
                var kk = Download("https://favoritelist-mmtr.c9users.io/get_middleclassification");
                Items = (List<MiddleClassification>)kk;
         //                       Items = (List<MiddleClassification>)kk;
   //             _items2 = (List<MiddleClassification>)kk;
                _title = "タイトル代入";
           //                 _items = new List<MiddleClassification>
            //{ 
              //  new MiddleClassification{Name="ばなな"},
               // new MiddleClassification{Name="どーなつ"},
            //};
            }
        }
    }
}
