
using CurrencyChecker.Core.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurrencyChecker.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        private readonly RateViewModel _viewModel;

        public DetailsPage(RateViewModel vm)
        {
            InitializeComponent();
            BindingContext = _viewModel = vm;
        }
    }
}