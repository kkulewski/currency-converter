using System.Collections.Generic;
using System.Linq;
using CConv.Models;
using CConv.Services;
using CConv.Services.CurrencyProviders;

namespace CConv.ViewModels
{
    public class ConvertViewModel : BaseViewModel
    {
        private readonly ICurrencyConversionService _conversionService;
        private ICurrencyProvider _currencyProvider;
        private ICurrency _sourceCurrency;
        private ICurrency _targetCurrency;
        private decimal _sourceValue;
        private decimal _targetValue;

        public ConvertViewModel()
        {
            _conversionService = new CurrencyConversionService();
            CurrencyProviderList = new List<ICurrencyProvider>
            {
                new FakeCurrencyProvider()
            };

            SelectedCurrencyProvider = CurrencyProviderList.FirstOrDefault();

            CurrencyList = SelectedCurrencyProvider.Currencies;

            SourceCurrency = CurrencyList.FirstOrDefault();
            TargetCurrency = CurrencyList.LastOrDefault();

            PropertyChanged += (sender, e) => Convert();
        }

        public IList<ICurrencyProvider> CurrencyProviderList { get; set; }

        public ICurrencyProvider SelectedCurrencyProvider
        {
            get => _currencyProvider;
            set => SetProperty(ref _currencyProvider, value);
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

        private void Convert()
        {
            TargetValue = _conversionService.Convert(SourceCurrency, TargetCurrency, SourceValue);
        }
    }
}
