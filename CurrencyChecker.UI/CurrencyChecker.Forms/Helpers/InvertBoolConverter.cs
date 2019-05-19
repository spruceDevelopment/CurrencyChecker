using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyChecker.Forms.Helpers
{
    public class InvertBoolConverter : Xamarin.Forms.IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //bool result;
            if (bool.TryParse(value.ToString(), out bool result))
                return !result;
            else
                return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
