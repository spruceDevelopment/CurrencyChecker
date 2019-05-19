using CurrencyChecker.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChecker.Core.Contracts
{
    public interface INavigator
    {
        Task PushAsync(string pageKey, BaseViewModel? viewModel = null);
        Task GoBackAsync();
    }
}
