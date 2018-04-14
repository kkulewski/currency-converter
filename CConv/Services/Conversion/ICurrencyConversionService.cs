using CConv.Models;

namespace CConv.Services.Conversion
{
    public interface ICurrencyConversionService
    {
        decimal Convert(ICurrency sourceCurrency, ICurrency targetCurrency, decimal value);
    }
}
