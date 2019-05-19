using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using System.Linq;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System.Collections.Generic;
using CurrencyChecker.Core.Services;
using CurrencyChecker.Core.Contracts;
using CurrencyChecker.Core.Helpers;
using CurrencyChecker.Core.Models;
using CurrencyChecker.Core.Messages;

namespace CurrencyChecker.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ICurrencyService _currencyService;
        private readonly ISettingsProvider _settingsProvider;
        private readonly IErrorHandler _errorHandler;
        private string _searchText = "";

        private readonly List<RateViewModel> _sourceItems = new List<RateViewModel>();
        public ObservableCollection<RateViewModel> Items { get; set; }
        public IAsyncCommand RefreshCommand { get; }
        public IAsyncCommand<RateViewModel> ItemTappedCommand { get; }
        public string SearchText { get => _searchText; set { SetProperty(ref _searchText, value, onChanged: FilterAndDisplay); } }
        public string TopLabelText => $"1 {_settingsProvider.GetValue<string>("baseCurrency")} = ";
        public MainViewModel(ICurrencyService currencyService, ISettingsProvider settingsProvider, IErrorHandler errorHandler)
        {
            Title = "Currency Checker";
            Items = new ObservableCollection<RateViewModel>();
            RefreshCommand = new AsyncCommand(LoadCurrencies, errorHandler: errorHandler);
            ItemTappedCommand = new AsyncCommand<RateViewModel>(ItemTapped);
            this._currencyService = currencyService;
            this._settingsProvider = settingsProvider;
            this._errorHandler = errorHandler;
            MessengerInstance.Register<BaseCurrencyChangedMessage>(this, OnBaseCurrencyChangedMessage);
        }

        private async Task ItemTapped(RateViewModel vm)
        {
            await Navigator.PushAsync("details", vm);
        }

        private void OnBaseCurrencyChangedMessage(BaseCurrencyChangedMessage obj)
        {
            LoadCurrencies().FireAndForgetSafeAsync(_errorHandler);
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
                RaisePropertyChanged(nameof(TopLabelText));
                _sourceItems.Clear();
                Items.Clear();
                CurrentRatesDataObject? result = await _currencyService.GetCurrentRates(_settingsProvider.GetValue<string>("baseCurrency"));
                if (result?.Rates != null)
                    foreach (var item in result.Rates)
                        _sourceItems.Add(new RateViewModel(result.Base ?? "", item.Key, item.Value, _currencyService, _settingsProvider));
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
            foreach (var item in _sourceItems.Where(x=>x.TargetKey.ToLower().Contains(SearchText)))
                Items.Add(item);
        }
    }
}