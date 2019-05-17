using System;

using CurrencyChecker.Models;

namespace CurrencyChecker.ViewModels
{
    public class RateViewModel : BaseViewModel
    {
        public string Key { get; }
        private double _value { get; }
        public string DisplayValue => _value.ToString("N4");

        public RateViewModel(string key, double value)
        {
            Key = key;
            _value = value;
        }
    }
}
