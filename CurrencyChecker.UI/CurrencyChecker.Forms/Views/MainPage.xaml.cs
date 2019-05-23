using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GalaSoft.MvvmLight.Ioc;
using CurrencyChecker.Core.ViewModels;
using CurrencyChecker.Core.Contracts;
using CurrencyChecker.Core.Helpers;

namespace CurrencyChecker.Forms.Views
{
    public partial class MainPage : ContentPage
    {
        MainViewModel _viewModel;

        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void ItemsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            _viewModel.ItemTappedCommand.ExecuteAsync((RemoteRateViewModel)e.Item).FireAndForgetSafeAsync(SimpleIoc.Default.GetInstance<IErrorHandler>());
        }
    }
}