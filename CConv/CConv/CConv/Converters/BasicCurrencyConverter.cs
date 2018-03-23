using System;
using System.Globalization;
using Xamarin.Forms;

namespace CConv.Converters
{
    internal class BasicCurrencyConverter : IValueConverter
    {
        private readonly decimal _multiplier;

        public BasicCurrencyConverter(decimal multiplier)
        {
            _multiplier = multiplier;
        }

        public BasicCurrencyConverter()
        {
            _multiplier = 10M;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s)
                return ParseString(s) * _multiplier;

            if (value is decimal d)
                return d * _multiplier;

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s)
                return ParseString(s) / _multiplier;

            if (value is decimal d)
                return d / _multiplier;

            return 0;
        }

        private decimal ParseString(object value)
        {
            decimal.TryParse((string)value, out var result);
            return result;
        }
    }
}
