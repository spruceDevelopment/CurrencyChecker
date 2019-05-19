using CurrencyChecker.Core.Models;
using System.Threading.Tasks;

namespace CurrencyChecker.Core.Services
{
    public interface ICurrencyService
    {
        Task<RatesDataObject?> GetCurrentRates(string baseCurrency);
        Task<RatesDataObject?> GetHistoryRates(string baseCurrency, string target);
    }
}