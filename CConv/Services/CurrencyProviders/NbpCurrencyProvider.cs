using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CConv.Models;
using CConv.Services.Cache;
using CConv.Services.Storage;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CConv.Services.CurrencyProviders
{

    public class NbpCurrencyProvider : ICurrencyProvider
    {
        private readonly ICache<IList<ICurrency>> _cache;
        private readonly NbpTable _table;
        private readonly string _currenciesFileName;
        private readonly string _updatedOnFileName;

        public NbpCurrencyProvider(NbpTable table)
        {
            _table = table;
            _cache = new SimpleCache<IList<ICurrency>>(TimeSpan.FromSeconds(10));

            Name = string.Format("NBP - {0}", _table);
            Currencies = new List<ICurrency>();
            _cache.Expire();
 
            _currenciesFileName = nameof(NbpCurrencyProvider) + _table + "currencies";
            _updatedOnFileName = nameof(NbpCurrencyProvider) + _table + "updatedOn";
        }

        public string Name { get; }

        public IList<ICurrency> Currencies
        {
            get => _cache.Get();
            private set => _cache.Set(value);
        }

        public DateTime UpdatedOn
        {
            get => _cache.UpdatedOn;
            set => _cache.UpdatedOn = value;
        }

        public async Task<bool> Fetch()
        {
            try
            {
                if (!_cache.Expired)
                {
                    return true;
                }

                var uri = string.Format("http://api.nbp.pl/api/exchangerates/tables/{0}?format=json", _table);
                var json = await DownloadJson(uri);
                Currencies = DeserializeCurrencyList(json);
                await Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Load()
        {
            var writer = DependencyService.Get<IFileWriter>();
            var currenciesJson = await writer.Read(_currenciesFileName);
            var updatedOnJson = await writer.Read(_updatedOnFileName);

            var currenciesDeserialized = JsonConvert.DeserializeObject<List<Currency>>(currenciesJson);
            if (currenciesDeserialized == null)
            {
                return false;
            }

            var updatedOnDeserialized = JsonConvert.DeserializeObject<DateTime>(updatedOnJson);
            if (updatedOnDeserialized == DateTime.MinValue)
            {
                return false;
            }

            Currencies = currenciesDeserialized.ToList<ICurrency>();
            UpdatedOn = updatedOnDeserialized;

            return Currencies.Count > 0;
        }

        private async Task Save()
        {
            var writer = DependencyService.Get<IFileWriter>();
            var currenciesJson = JsonConvert.SerializeObject(Currencies);
            var updatedOnJson = JsonConvert.SerializeObject(UpdatedOn);
            await writer.Write(_currenciesFileName, currenciesJson);
            await writer.Write(_updatedOnFileName, updatedOnJson);
        }

        private static async Task<string> DownloadJson(string uri)
        {
            string json;
            using (var client = new WebClient())
            {
                json = await client.DownloadStringTaskAsync(uri);
            }

            return json;
        }

        private static IList<ICurrency> DeserializeCurrencyList(string json)
        {
            var deserialized = JsonConvert.DeserializeObject<ExchangeTable[]>(json);
            var rates = deserialized[0].Rates;

            var currencies = rates.Select(x => new Currency
            {
                Code = x.Code,
                Name = x.Currency,
                Rate = decimal.Parse(x.Mid)
            });

            return currencies.ToList<ICurrency>();
        }

        #region JSON
        
        public class ExchangeTable
        {
            public string Table { get; set; }
            public string No { get; set; }
            public string EffectiveDate { get; set; }
            public Rate[] Rates { get; set; }
        }

        public class Rate
        {
            public string Currency { get; set; }
            public string Code { get; set; }
            public string Mid { get; set; }
        }

        #endregion
    }

    public enum NbpTable
    {
        A,
        B
    }
}
