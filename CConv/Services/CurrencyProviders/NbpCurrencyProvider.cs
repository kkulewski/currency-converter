using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CConv.Models;
using Newtonsoft.Json;

namespace CConv.Services.CurrencyProviders
{

    public class NbpCurrencyProvider : ICurrencyProvider
    {
        private readonly NbpTable _table;

        public NbpCurrencyProvider(NbpTable table)
        {
            _table = table;
            Name = string.Format("NBP - {0}", _table);
            Currencies = new List<ICurrency>();
        }

        public string Name { get; }

        public IList<ICurrency> Currencies { get; private set; }

        public async Task<bool> Fetch()
        {
            try
            {
                var uri = string.Format("http://api.nbp.pl/api/exchangerates/tables/{0}?format=json", _table);
                var json = await DownloadJson(uri);
                Currencies = DeserializeCurrencyList(json);

                return true;
            }
            catch
            {
                return false;
            }
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
