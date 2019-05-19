
using CurrencyChecker.Core.Contracts;
using CurrencyChecker.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChecker.Core.Services
{
    public class ExternalCurrencyService : ICurrencyService
    {
      
        static HttpClient _httpClient = new HttpClient();
        private readonly IErrorHandler _errorHandler;
        public ExternalCurrencyService(IErrorHandler errorHandler)
        {
            this._errorHandler = errorHandler;
        }
        public async Task<RatesDataObject?> GetCurrentRates(string baseCurrency)
        {
            return await GetRates($"https://api.exchangeratesapi.io/latest?base={baseCurrency}");
        }

        public async Task<RatesDataObject?> GetHistoryRates(string baseCurrency, string target)
        {
            return await GetRates($"https://api.exchangeratesapi.io/history?start_at=2018-01-01&end_at=2018-09-01&base={baseCurrency}&symbols={target}");
        }

        private async Task<RatesDataObject?> GetRates(string url)
        {
            HttpResponseMessage? result = null;
            try
            {
                result = await _httpClient.GetAsync(url);
            }
            catch (Exception ex)
            {
                _errorHandler.HandleError(ex);
            }
            if (result?.Content == null)
                return null;
            return JsonConvert.DeserializeObject<RatesDataObject>(await result.Content.ReadAsStringAsync());
        }
    }
}
