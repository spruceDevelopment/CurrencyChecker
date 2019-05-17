using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.Linq;
using CurrencyChecker.Models;
using CurrencyChecker.Views;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using CurrencyChecker.Services;
using System.Collections.Generic;

namespace CurrencyChecker.ViewModels
{
    public class CurrentViewModel : BaseViewModel
    {
        private readonly ICurrencyService _currencyService;
        private string _searchText = "";

        private readonly List<RateViewModel> _sourceItems = new List<RateViewModel>();
        public ObservableCollection<RateViewModel> Items { get; set; }
        public Command RefreshCommand { get; }
        //public RelayCommand<FixerioRates> ItemSelectedCommand { get; }
        public string SearchText { get => _searchText; set { SetProperty(ref _searchText, value, onChanged:FilterAndDisplay); } }
        public CurrentViewModel(ICurrencyService currencyService)
        {
            Title = "Browse";
            Items = new ObservableCollection<RateViewModel>();
            RefreshCommand = new Command(async () => await LoadCurrencies());
            //ItemSelectedCommand = new RelayCommand<FixerioRates>(ItemSelected);
            this._currencyService = currencyService;
        }

        public override async Task Init()
        {
            await LoadCurrencies();
        }

        private async Task LoadCurrencies()
        {
            try
            {
                IsBusy = true;
                _sourceItems.Clear();
                Items.Clear();
                var result = await _currencyService.GetCurrentRates();
                if (result?.Rates != null)
                    foreach (var item in result.Rates)
                        _sourceItems.Add(new RateViewModel(item.Key, item.Value));
                FilterAndDisplay();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void FilterAndDisplay()
        {
            Items.Clear();
            foreach (var item in _sourceItems.Where(x=>x.Key.ToLower().Contains(SearchText)))
                Items.Add(item);
        }
    }
}