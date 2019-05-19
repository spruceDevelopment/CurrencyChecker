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
    public class RateViewModel : BaseViewModel
    {
        public string TargetKey { get; }
        private readonly float _value;
        private readonly IExternalCurrencyService _currencyService;
        private readonly ISettingsProvider _settingsProvider;
        private readonly ILocalCurrencyService _localCurrencyService;
        private HistoryRatesDataObject? _currentDataObject;

        public string DisplayValue => _value.ToString("N4");
        public string BaseKey { get; }
        Chart? _chart;
        public Chart? Chart { get => _chart; set { SetProperty(ref _chart, value); } }
        public IAsyncCommand SetAsBaseCurrencyCommand { get;}
        public IAsyncCommand SaveDataCommand { get;  }

        public RateViewModel(string baseKey, string targetKey, float value, IExternalCurrencyService currencyService, ISettingsProvider settingsProvider, ILocalCurrencyService localCurrencyService)
        {
            TargetKey = targetKey;
            _value = value;
            _currencyService = currencyService;
            _settingsProvider = settingsProvider;
            _localCurrencyService = localCurrencyService;
            BaseKey = baseKey;
            Title = $"1 {baseKey} = x {targetKey}";
            SetAsBaseCurrencyCommand = new AsyncCommand(SetAsBaseCurrency);
            SaveDataCommand = new AsyncCommand(SaveData);
        }


        public override async Task Init()
        {
            var draftEntries = new List<Entry>();
            var result = await _currencyService.GetHistoryRates(BaseKey, TargetKey, DateTime.Today.AddDays(-30), DateTime.Today);
            if (result?.HistoryRates == null)
                return;
            _currentDataObject = result;
            var sorted = result.HistoryRates.OrderBy(x => x.Key);
            foreach (var item in sorted)
            {
                var entry = new Entry(item.Value);
                entry.Label = DateTime.Parse(item.Key).ToString("dd-MM-yyyy");
                entry.ValueLabel = item.Value.ToString("N4");
                entry.Color = SKColor.Parse("#7777FF");
                draftEntries.Add(entry);
            }
            Chart = new LineChart()
            {
                Entries = draftEntries,
                LineMode = LineMode.Spline,
                LineSize = 5,
                PointMode = PointMode.Circle,
                PointSize = 12,
                MaxValue = draftEntries.Max(x => x.Value) + 0.0001f,
                MinValue = draftEntries.Min(x => x.Value) - 0.0001f,
            };
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
