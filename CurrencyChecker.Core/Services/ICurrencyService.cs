using CurrencyChecker.Core.Models;
using System;
using System.Threading.Tasks;

namespace CurrencyChecker.Core.Services
{
    public interface ICurrencyService
    {
        Task<CurrentRatesDataObject?> GetCurrentRates(string baseCurrency);
        Task<HistoryRatesDataObject?> GetHistoryRates(string baseCurrency, string target, DateTime startDate, DateTime endDate);
    }
}