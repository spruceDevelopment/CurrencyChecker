using CurrencyChecker.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;
using System.Linq;

namespace CurrencyChecker.Core.Services
{
    public class LocalCurrencyService : ILocalCurrencyService
    {
        static bool _initialized;
        static SQLiteAsyncConnection _instance;
        private SQLiteAsyncConnection Connection => _instance ?? (_instance = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "currencies.db3")));

        public async Task AddRecordAsync(HistoryRatesDataObject record)
        {
            await CheckInit(Connection);
            record.DatabaseKey = record.GenerateDBKey();
            await Connection.InsertAsync(record);
        }

        public async Task<List<string?>> GetAllRecordsNamesAsync()
        {
            var connection = Connection;
                await CheckInit(connection);
                
                var result = await connection.QueryAsync<HistoryRatesDataObject>("select DatabaseKey from HistoryRatesDataObject");
                return result.Select(x => x.DatabaseKey).ToList();
        }

        public async Task<HistoryRatesDataObject> GetRecordAsync(string key)
        {
            var connection = Connection;
                await CheckInit(connection);
                return await connection.ExecuteScalarAsync<HistoryRatesDataObject>("select * from HistoryRatesDataObject where DatabaseKey = ?", key);
        }


        public async Task DeleteRecordAsync(string key)
        {
            var connection = Connection;
                await CheckInit(connection);
                await connection.ExecuteScalarAsync<HistoryRatesDataObject>("delete from HistoryRatesDataObject where DatabaseKey = ?", key);
        }

        private async Task CheckInit(SQLiteAsyncConnection connection)
        {
            if (!_initialized)
            {
                await connection.CreateTableAsync<HistoryRatesDataObject>();
                _initialized = true;
            }
        }
    }
}
