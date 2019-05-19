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
    public partial class PickLocalDataPage : ContentPage
    {
        PickLocalDataViewModel _viewModel;

        public PickLocalDataPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = SimpleIoc.Default.GetInstance<PickLocalDataViewModel>();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void ItemsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            _viewModel.ItemTappedCommand.ExecuteAsync((LocalDataRecordViewModel)e.Item).FireAndForgetSafeAsync(SimpleIoc.Default.GetInstance<IErrorHandler>());
        }
    }
}