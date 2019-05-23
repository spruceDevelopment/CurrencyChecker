using CurrencyChecker.Core.Contracts;
using CurrencyChecker.Core.Services;
using System;
using System.Threading.Tasks;
using Microcharts;
using System.Collections.ObjectModel;
using CurrencyChecker.Core.Helpers;
using CurrencyChecker.Core.Messages;
using System.Collections.Generic;
using System.Linq;
using SkiaSharp;
using CurrencyChecker.Core.Models;

namespace CurrencyChecker.Core.ViewModels
{
    public class RemoteRateViewModel : BaseViewModel
    {
        public string TargetKey { get; }
        private readonly float _value;
        private readonly IExternalCurrencyService _currencyService;
        private readonly ISettingsProvider _settingsProvider;
        private readonly ILocalCurrencyService _localCurrencyService;
        private HistoryRatesDataObject? _currentDataObject;

        public CurrencyDataGridViewModel CurrencyGridViewModel {get;}

        public string DisplayValue => _value.ToString("N4");
        public string BaseKey { get; }
        public IAsyncCommand SetAsBaseCurrencyCommand { get;}
        public IAsyncCommand SaveDataCommand { get;  }

        public RemoteRateViewModel(string baseKey, string targetKey, float value, IExternalCurrencyService currencyService, ISettingsProvider settingsProvider, ILocalCurrencyService localCurrencyService, IErrorHandler errorHandler)
        {
            TargetKey = targetKey;
            _value = value;
            _currencyService = currencyService;
            _settingsProvider = settingsProvider;
            _localCurrencyService = localCurrencyService;
            BaseKey = baseKey;
            Title = $"1 {baseKey} = x {targetKey}";
            SetAsBaseCurrencyCommand = new AsyncCommand(SetAsBaseCurrency, errorHandler: errorHandler);
            SaveDataCommand = new AsyncCommand(SaveData, errorHandler: errorHandler);
            CurrencyGridViewModel = new CurrencyDataGridViewModel();
        }


        public override async Task Init()
        {
            var result = await _currencyService.GetHistoryRates(BaseKey, TargetKey, DateTime.Today.AddDays(-30), DateTime.Today);
            if (result?.HistoryRates == null)
                return;
            _currentDataObject = result;

            CurrencyGridViewModel.PassNewData(result);
        }


        private async Task SetAsBaseCurrency()
        {
            _settingsProvider.SetValue("baseCurrency", TargetKey);
            MessengerInstance.Send(new BaseCurrencyChangedMessage());
            await Navigator.GoBackAsync();
        }


        private async Task SaveData()
        {
            if(_currentDataObject != null)
                await _localCurrencyService.AddRecordAsync(_currentDataObject);
        }
    }
}
