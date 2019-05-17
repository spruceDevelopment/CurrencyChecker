using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CurrencyChecker.Models;
using CurrencyChecker.Views;
using CurrencyChecker.ViewModels;
using GalaSoft.MvvmLight.Ioc;

namespace CurrencyChecker.Views
{
    public partial class CurrentPage : ContentPage
    {
        CurrentViewModel _viewModel;

        public CurrentPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = SimpleIoc.Default.GetInstance<CurrentViewModel>();
        }

        //async void AddItem_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Items.Count == 0)
                _viewModel.RefreshCommand.Execute(null);
        }
    }
}