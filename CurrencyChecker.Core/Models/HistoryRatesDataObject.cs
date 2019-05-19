using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CurrencyChecker.Core.Models
{
    public class HistoryRatesDataObject
    {
        public HistoryRatesDataObject(Dictionary<string, KeyValuePair<string, float>> rates)
        {
            HistoryRates = rates;
        }

        [JsonProperty("base")]
        public string? Base { get; set; }
        public Dictionary<string, KeyValuePair<string, float>> HistoryRates { get; }
    }
}