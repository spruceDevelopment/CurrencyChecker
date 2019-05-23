
using CurrencyChecker.Core.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurrencyChecker.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocalDetailsPage : ContentPage
    {
        private readonly LocalRateViewModel _viewModel;

        public LocalDetailsPage(LocalRateViewModel vm)
        {
            InitializeComponent();
            BindingContext = _viewModel = vm;
        }
    }
}