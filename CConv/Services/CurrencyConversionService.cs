using CConv.Models;

namespace CConv.Services
{
    public class CurrencyConversionService : ICurrencyConversionService
    {
        public decimal Convert(ICurrency sourceCurrency, ICurrency targetCurrency, decimal value)
        {
            var rate = targetCurrency.Rate / sourceCurrency.Rate;
            return decimal.Round(value * rate, 2);
        }
    }
}
