using CConv.Models;

namespace CConv.Services.Conversion
{
    public interface ICurrencyConversionService
    {
        decimal Convert(ICurrency source, ICurrency target, decimal value);
    }
}
