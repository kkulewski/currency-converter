using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CConv.Models;
using Newtonsoft.Json;

namespace CConv.Services.CurrencyProviders
{
    public enum NbpTable
    {
        A,
        B
    }

    public class NbpCurrencyProvider : ICurrencyProvider
    {
        private readonly NbpTable _table;

        public NbpCurrencyProvider(NbpTable table)
        {
            _table = table;
            Name = string.Format("NBP - {0}", _table);
        }

        public string Name { get; }

        public IList<ICurrency> Currencies { get; private set; }

        public async Task<bool> Fetch()
        {
            try
            {
                var uri = string.Format("http://api.nbp.pl/api/exchangerates/tables/{0}?format=json", _table);
                var client = new WebClient();
                var json = await client.DownloadStringTaskAsync(uri);

                var deserialized = JsonConvert.DeserializeObject<ExchangeTable[]>(json);
                var rates = deserialized[0].Rates;

                Currencies = rates.Select(x => new Currency
                {
                    Code = x.Code,
                    Name = x.Currency,
                    Rate = decimal.Parse(x.Mid)
                }).ToList<ICurrency>();

                return true;
            }
            catch
            {
                return false;
            }
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
}
