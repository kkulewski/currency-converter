using CConv.Models;

namespace CConv.Services
{
    public interface ICurrencyConversionService
    {
        decimal Convert(ICurrency sourceCurrency, ICurrency targetCurrency, decimal value);
    }
}
