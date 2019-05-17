using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CurrencyChecker.Models
{
    public class FixerioRates
    {
        [JsonProperty("base")]
        public string Base { get; set; }
        [JsonProperty("rates")]
        public Dictionary<string, float> Rates { get; set; }
    }
}