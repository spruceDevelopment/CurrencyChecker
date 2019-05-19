using CurrencyChecker.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChecker.Core.Services
{
    public interface ILocalCurrencyService
    {
        Task<List<string?>> GetAllRecordsNamesAsync();
        Task<HistoryRatesDataObject> GetRecordAsync(string key);
        Task AddRecordAsync(HistoryRatesDataObject record);
        Task DeleteRecordAsync(string key);
    }
}
