using System.Collections.Generic;
using CConv.Models;

namespace CConv.Services.CurrencyProviders
{
    public class FakeCurrencyProvider : ICurrencyProvider
    {
        public FakeCurrencyProvider()
        {
            Name = "FakeProvider";

            Currencies = new List<ICurrency>
            {
                new Currency {Code = "EUR", Name = "Euro", Rate = 1.0M},
                new Currency {Code = "USD", Name = "United States Dollar", Rate = 1.25M},
                new Currency {Code = "GBP", Name = "British Pound", Rate = 0.85M},
                new Currency {Code = "AUD", Name = "Australian Dollar", Rate = 1.6M},
                new Currency {Code = "CAD", Name = "Canadian Dollar", Rate = 1.6M}
            };
        }


        public string Name { get; }

        public IList<ICurrency> Currencies { get; }
    }
}
