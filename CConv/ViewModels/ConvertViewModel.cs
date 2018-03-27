using System.Collections.Generic;
using System.Linq;
using CConv.Models;

namespace CConv.ViewModels
{
    public class ConvertViewModel : BaseViewModel
    {
        private ICurrency _sourceCurrency;
        private ICurrency _targetCurrency;
        private decimal _sourceValue;
        private decimal _targetValue;

        public ConvertViewModel()
        {
            CurrencyList = new List<ICurrency>
            {
                new Currency {Name = "Euro", ShortName = "EUR", Rate = 1.0M},
                new Currency {Name = "United States dollar", ShortName = "USD", Rate = 1.2M}
            };

            SourceCurrency = CurrencyList.FirstOrDefault();
            TargetCurrency = CurrencyList.LastOrDefault();
        }

        public IList<ICurrency> CurrencyList { get; set; }

        public ICurrency SourceCurrency
        {
            get => _sourceCurrency;
            set => SetProperty(ref _sourceCurrency, value);
        }

        public ICurrency TargetCurrency
        {
            get => _targetCurrency;
            set => SetProperty(ref _targetCurrency, value);
        }

        public decimal SourceValue
        {
            get => _sourceValue;
            set => SetProperty(ref _sourceValue, value);
        }

        public decimal TargetValue
        {
            get => _targetValue;
            set => SetProperty(ref _targetValue, value);
        }
    }
}
