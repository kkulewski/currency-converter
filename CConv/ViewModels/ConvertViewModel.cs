using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CConv.Models;
using CConv.Services;
using CConv.Services.CurrencyProviders;
using Xamarin.Forms;

namespace CConv.ViewModels
{
    public class ConvertViewModel : BaseViewModel
    {
        public readonly Action RaiseTargetValueChanged;
        private readonly ICurrencyConversionService _conversionService;
        private ICurrencyProvider _currencyProvider;
        private ICurrency _sourceCurrency;
        private ICurrency _targetCurrency;
        private decimal _sourceValue;

        public ConvertViewModel()
        {
            RaiseTargetValueChanged = () => OnPropertyChanged(nameof(Currencies));

            _conversionService = new CurrencyConversionService();
            CurrencyProviders = new List<ICurrencyProvider>
            {
                new FakeCurrencyProvider()
            };

            FetchCommand = new Command(() =>
            {
                SelectedCurrencyProvider.Fetch();
                OnPropertyChanged(nameof(TargetValue));
            });
        }

        public ICommand FetchCommand { get; }

        public IList<ICurrencyProvider> CurrencyProviders { get; set; }

        public ICurrencyProvider SelectedCurrencyProvider
        {
            get => _currencyProvider ?? CurrencyProviders.FirstOrDefault();
            set => SetProperty(ref _currencyProvider, value, RaiseTargetValueChanged);
        }

        public IList<ICurrency> Currencies => SelectedCurrencyProvider.Currencies;

        public ICurrency SourceCurrency
        {
            get => _sourceCurrency ?? Currencies.FirstOrDefault();
            set => SetProperty(ref _sourceCurrency, value, RaiseTargetValueChanged);
        }

        public ICurrency TargetCurrency
        {
            get => _targetCurrency ?? Currencies.Skip(1).FirstOrDefault();
            set => SetProperty(ref _targetCurrency, value, RaiseTargetValueChanged);
        }

        public decimal SourceValue
        {
            get => _sourceValue;
            set => SetProperty(ref _sourceValue, value, RaiseTargetValueChanged);
        }

        public decimal TargetValue => _conversionService.Convert(SourceCurrency, TargetCurrency, SourceValue);
    }
}
