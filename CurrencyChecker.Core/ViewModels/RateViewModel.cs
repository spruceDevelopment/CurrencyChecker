using System;


namespace CurrencyChecker.Core.ViewModels
{
    public class RateViewModel : BaseViewModel
    {
        public string TargetName { get; }
        private readonly double _value;
        public string DisplayValue => _value.ToString("N4");
        public string BaseName { get; }

        public RateViewModel(string baseName, string targetName, double value )
        {
            TargetName = targetName;
            _value = value;
            BaseName = baseName;
        }
    }
}
