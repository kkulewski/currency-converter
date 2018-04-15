using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CConv.Models;

namespace CConv.Services.CurrencyProviders
{
    public class FakeCurrencyProvider : ICurrencyProvider
    {
        private readonly Random _random;

        public FakeCurrencyProvider()
        {
            _random = new Random();

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

        public DateTime UpdatedOn => DateTime.Now;

        public async Task<bool> Fetch()
        {
            foreach (var currency in Currencies)
            {
                var factor = _random.Next(-5, 5) * 0.01M;
                currency.Rate += currency.Rate * factor;
                currency.Rate = decimal.Round(currency.Rate, 4);
            }

            return await Task.Run(() => true);
        }

        public async Task<bool> Load()
        {
            await Fetch();
            return await Task.Run(() => true);
        }
    }
}
