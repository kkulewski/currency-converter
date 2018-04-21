using CConv.Models;

namespace CConv.Services.Conversion
{
    public class CurrencyConversionService : ICurrencyConversionService
    {
        public decimal Convert(ICurrency source, ICurrency target, decimal value)
        {
            var rate = target.Rate / source.Rate;
            return decimal.Round(value * rate, 2);
        }
    }
}
