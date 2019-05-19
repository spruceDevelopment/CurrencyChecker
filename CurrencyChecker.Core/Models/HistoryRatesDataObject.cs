using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyChecker.Core.Models
{
    public class HistoryRatesDataObject
    {
        [PrimaryKey]
        public string? DatabaseKey { get; set; }
        [JsonProperty("base")]
        public string? Base { get; set; }
        [JsonProperty("rates"), Ignore]
        public Dictionary<string, float>? HistoryRates { get; set; }



        public string? Target;
        public string? end_at;
        public string? start_at;

        public string GenerateDBKey()
        {
            return Base + "_" + Target + "_" + end_at;
        }
    }
}