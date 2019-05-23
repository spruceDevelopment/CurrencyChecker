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
        public string? SerializedHistoryRates { get; set; }



        public string? Target{get; set;}
        public string? end_at { get; set; }
        public string? start_at { get; set; }

        public string GenerateDBKey()
        {
            return Base + "_" + Target + "_" + end_at;
        }

        public void SerializeDictionary()
        {
            SerializedHistoryRates = JsonConvert.SerializeObject(HistoryRates);
        }
        public void DeserializeDictionary()
        {
            HistoryRates = JsonConvert.DeserializeObject<Dictionary<string,float>?>(SerializedHistoryRates);
        }
    }
}