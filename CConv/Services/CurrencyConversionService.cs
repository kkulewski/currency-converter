using System;
using CConv.Models;

namespace CConv.Services
{
    public class CurrencyConverterService : ICurrencyConverterService
    {
        public decimal Convert(ICurrency sourceCurrency, ICurrency targetCurrency, decimal value)
        {
            throw new NotImplementedException();
        }
    }
}
