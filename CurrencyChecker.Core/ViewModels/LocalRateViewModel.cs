using CurrencyChecker.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChecker.Core.ViewModels
{
    public class LocalRateViewModel : BaseViewModel
    {
        public CurrencyDataGridViewModel CurrencyGridViewModel { get; }
        HistoryRatesDataObject _dataObj;
        public LocalRateViewModel(HistoryRatesDataObject dataObj)
        {
            CurrencyGridViewModel = new CurrencyDataGridViewModel();
            _dataObj = dataObj;
            Title = $"1 {_dataObj.Base} = x {_dataObj.Target}   - dane lokalne"; 
        }

        public override Task Init()
        {
            CurrencyGridViewModel.PassNewData(_dataObj);
            return Task.CompletedTask;
        }
    }
}
