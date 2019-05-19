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
        public T GetValue<T>(string key)
        {
            if (!Application.Current.Properties.ContainsKey(key))
            {
                if (key == "baseCurrency")
                    Application.Current.Properties[key] = "EUR";
                else if (key == "startDate")
                    Application.Current.Properties[key] = DateTime.Today.AddDays(-30);
                else if (key == "endDate")
                    Application.Current.Properties[key] = DateTime.Today;
                else
                    return default(T)!;
            }
            return (T)Application.Current.Properties[key];
        }


        public void SetValue(string name, object value)
        {
            Application.Current.Properties[name] = value;
        }
    }
}
