using System.Collections.Generic;
using CConv.Models;

namespace CConv.Services.CurrencyRateProviders
{
    public class FakeCurrencyRateProvider : ICurrencyRateProvider
    {
        public IList<ICurrency> Currencies { get; }

        public FakeCurrencyRateProvider()
        {
            Currencies = new List<ICurrency>
            {
                new Currency {ShortName = "EUR", Name = "Euro", Rate = 1.0M},
                new Currency {ShortName = "USD", Name = "United States Dollar", Rate = 1.25M},
                new Currency {ShortName = "GBP", Name = "British Pound", Rate = 0.85M},
                new Currency {ShortName = "AUD", Name = "Australian Dollar", Rate = 1.6M},
                new Currency {ShortName = "CAD", Name = "Canadian Dollar", Rate = 1.6M}
            };
        }
    }
}
