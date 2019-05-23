using CurrencyChecker.Core.Contracts;
using CurrencyChecker.Core.Helpers;
using CurrencyChecker.Core.Models;
using CurrencyChecker.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChecker.Core.ViewModels
{
    public class LocalDataRecordViewModel : BaseViewModel
    {
        public HistoryRatesDataObject? HistoryRatesDataObject;
        private readonly PickLocalDataViewModel _parentViewModel;

        private string _key;
        public string DisplayName => _key;
        public IAsyncCommand RemoveDataRecordCommand { get; }

        public LocalDataRecordViewModel(string key, PickLocalDataViewModel parentViewModel,IErrorHandler errorHandler)
        {
            _key = key;
            _parentViewModel = parentViewModel;
            RemoveDataRecordCommand = new AsyncCommand(RemoveDataRecord, errorHandler: errorHandler);
        }

        private async Task RemoveDataRecord()
        {
            await _parentViewModel.RemoveDataRecord(_key);
        }
    }
}
