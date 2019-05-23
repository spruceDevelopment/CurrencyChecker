
using CurrencyChecker.Core.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurrencyChecker.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RemoteDetailsPage : ContentPage
    {
        private readonly RemoteRateViewModel _viewModel;

        public RemoteDetailsPage(RemoteRateViewModel vm)
        {
            InitializeComponent();
            BindingContext = _viewModel = vm;
        }
    }
}