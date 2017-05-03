using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using OXamarin.Models;
using System.Threading.Tasks;

namespace OXamarin.Views
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();


        }

        /// <summary>
        /// コードビハインドにItemTappedイベント発火時のコードを書く必要がある
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            entry.Text = e.Item.ToString();
        }

    }
}
