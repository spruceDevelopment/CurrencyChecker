
using CurrencyChecker.Core.Contracts;
using CurrencyChecker.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChecker.Core.Services
{
    public class ExternalCurrencyService : IExternalCurrencyService
    {
      
        static HttpClient _httpClient = new HttpClient();
        private readonly IErrorHandler _errorHandler;
        public ExternalCurrencyService(IErrorHandler errorHandler)
        {
            this._errorHandler = errorHandler;
        }
        public async Task<CurrentRatesDataObject?> GetCurrentRates(string baseCurrency)
        {
            var result = await _httpClient.GetAsync($"https://api.exchangeratesapi.io/latest?base={baseCurrency}");
            if (result?.Content == null)
                return null;
            return JsonConvert.DeserializeObject<CurrentRatesDataObject>(await result.Content.ReadAsStringAsync());
        }

        public async Task<HistoryRatesDataObject?> GetHistoryRates(string baseCurrency, string target, DateTime startDate, DateTime endDate)
        {
            string url = $"https://api.exchangeratesapi.io/history?start_at={startDate.ToString("yyyy-MM-dd")}&end_at={endDate.ToString("yyyy-MM-dd")}&base={baseCurrency}&symbols={target}";
            var result = await _httpClient.GetAsync(url);
            if (result?.Content == null)
                return null;
            var content = await result.Content.ReadAsStringAsync();


            var ratesToken = JObject.Parse(content).GetValue("rates").ToList();
            List<JProperty> properties = new List<JProperty>();
            ratesToken.ForEach(x => properties.Add((JProperty)x));
            Dictionary<string, float> keyValuePairs = new Dictionary<string, float>();
            properties.ForEach(x => keyValuePairs.Add(x.Name, (float)x.Value[target]));
            HistoryRatesDataObject historyRatesDataObject = new HistoryRatesDataObject { HistoryRates = keyValuePairs, Base = baseCurrency, end_at = endDate.ToString("yyyy-MM-dd"), start_at = startDate.ToString("yyyy-MM-dd"), Target = target };
            return historyRatesDataObject;
        }


    }
}
