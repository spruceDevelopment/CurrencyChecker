using CurrencyChecker.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChecker.Services
{
    public class FixerioService : ICurrencyService
    {
        static HttpClient _httpClient = new HttpClient();
        public async Task<FixerioRates> GetCurrentRates()
        {
            HttpResponseMessage result = null;
            try
            {
                result = await _httpClient.GetAsync($"http://data.fixer.io/api/latest?access_key=2d6052c3cb370f417142faee7a6d826d");
            }
            catch
            {
                //TODO:obsługa błedów
            }
            if (result?.Content == null)
                return null;
            return JsonConvert.DeserializeObject<FixerioRates>(await result.Content.ReadAsStringAsync());
        }
    }
}
