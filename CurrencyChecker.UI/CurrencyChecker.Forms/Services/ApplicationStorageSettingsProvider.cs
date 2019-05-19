using CurrencyChecker.Core.Contracts;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CurrencyChecker.Forms.Services
{
    public class ApplicationStorageSettingsProvider : ISettingsProvider
    {
        public string GetStringValue(string key)
        {
            if (!Application.Current.Properties.ContainsKey(key))
            {
                if (key == "baseCurrency")
                    Application.Current.Properties[key] = "EUR";
                else
                    return "";
            }
            return Application.Current.Properties[key].ToString();
        }

        public void SetValue(string name, object value)
        {

            Application.Current.Properties[name] = value;
        }
    }
}
