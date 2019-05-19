using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CurrencyChecker.Core.Models
{
    public class RatesDataObject
    {
        public RatesDataObject(Dictionary<string, double> rates)
        {
            Rates = rates;
        }

        [JsonProperty("base")]
        public string? Base { get; set; }
        public Dictionary<string, double> Rates { get; }
    }
}