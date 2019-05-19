using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChecker.Core.Contracts
{
    public interface INavigator
    {
        Task Push(object page);
    }
}
