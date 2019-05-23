using CurrencyChecker.Core.Models;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChecker.Core.ViewModels
{
    public class CurrencyDataGridViewModel : BaseViewModel
    {

        Chart? _chart;
        public Chart? Chart { get => _chart; set { SetProperty(ref _chart, value); } }

        public void PassNewData(HistoryRatesDataObject data)
        {
            var sorted = data.HistoryRates.OrderBy(x => x.Key);
            var draftEntries = new List<Entry>();
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
                LabelTextSize = 8
            };
        }
    }
}
