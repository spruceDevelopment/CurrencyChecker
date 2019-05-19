using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CurrencyChecker.Core.Models
{
    public class CurrentRatesDataObject
    {
        public CurrentRatesDataObject(Dictionary<string, float> rates)
        {
            Rates = rates;
        }

        [JsonProperty("base")]
        public string? Base { get; set; }
        public Dictionary<string, float> Rates { get; }
    }
}