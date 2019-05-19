using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyChecker.Core.Contracts
{
    public interface ISettingsProvider
    {
        string GetStringValue(string name);
        void SetValue(string name, object value);
    }
}
