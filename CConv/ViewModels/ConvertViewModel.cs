using System.Collections.Generic;
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
