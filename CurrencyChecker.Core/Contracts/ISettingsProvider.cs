using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyChecker.Core.Contracts
{
    public interface ISettingsProvider
    {
        T GetValue<T>(string name);
        void SetValue(string name, object value);
    }
}
