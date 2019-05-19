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

namespace CurrencyChecker.Core.ViewModels
{
    public class RateViewModel : BaseViewModel
    {
        public string TargetKey { get; }
        private readonly float _value;
        private readonly ICurrencyService _currencyService;
        private readonly ISettingsProvider _settingsProvider;

        
        public string DisplayValue => _value.ToString("N4");
        public string BaseKey { get; }
        Chart? _chart;
        public Chart? Chart { get => _chart; set { SetProperty(ref _chart, value); } }
        public IAsyncCommand SetAsBaseCurrencyCommand { get; internal set; }

        public RateViewModel(string baseKey, string targetKey, float value, ICurrencyService currencyService, ISettingsProvider settingsProvider)
        {
            TargetKey = targetKey;
            _value = value;
            _currencyService = currencyService;
            _settingsProvider = settingsProvider;
            BaseKey = baseKey;
            Title = $"1 {baseKey} = x {targetKey}";
            SetAsBaseCurrencyCommand = new AsyncCommand(SetAsBaseCurrency);
        }

        public override async Task Init()
        {
            var draftEntries = new List<Entry>();
            var result = await _currencyService.GetHistoryRates(BaseKey, TargetKey, _settingsProvider.GetValue<DateTime>("startDate"), _settingsProvider.GetValue<DateTime>("endDate"));
            if (result?.HistoryRates == null)
                return;
            var sorted = result.HistoryRates.OrderBy(x => x.Key);
            foreach (var item in sorted)
            {
                var entry = new Entry((float)item.Value.Value);
                entry.Label = DateTime.Parse(item.Key).ToString("dd-MM-yyyy");
                entry.ValueLabel = item.Value.Value.ToString("N4");
                entry.Color = SKColor.Parse("#7777FF");
                draftEntries.Add(entry);
            }
            var max = draftEntries.Max(x => x.Value) + 0.0001f;
            var min = draftEntries.Min(x => x.Value) - 0.0001f;
            Chart = new LineChart()
            {
                Entries = draftEntries,
                LineMode = LineMode.Spline,
                LineSize = 5,
                PointMode = PointMode.Circle,
                PointSize = 12,
                MaxValue = max,
                MinValue = min,
            };
        }


        private async Task SetAsBaseCurrency()
        {
            _settingsProvider.SetValue("baseCurrency", TargetKey);
            MessengerInstance.Send(new BaseCurrencyChangedMessage());
            await Navigator.GoBackAsync();
        }
    }
}
