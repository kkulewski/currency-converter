﻿using System.Collections.Generic;
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
        private readonly ICurrencyConversionService _conversionService;
        private ICurrencyProvider _currencyProvider;
        private ICurrency _sourceCurrency;
        private ICurrency _targetCurrency;
        private decimal _sourceValue;
        private decimal _targetValue;
        private IList<ICurrency> _currencies;

        public ConvertViewModel()
        {
            _conversionService = new CurrencyConversionService();
            CurrencyProviders = new List<ICurrencyProvider>
            {
                new FakeCurrencyProvider()
            };

            SelectedCurrencyProvider = CurrencyProviders.FirstOrDefault();

            PropertyChanged += (sender, e) => Convert();

            FetchCommand = new Command(() =>
            {
                SelectedCurrencyProvider.Fetch();
                OnPropertyChanged(nameof(SourceValue));
            });
        }

        public IList<ICurrencyProvider> CurrencyProviders { get; set; }

        public ICurrencyProvider SelectedCurrencyProvider
        {
            get => _currencyProvider ?? CurrencyProviders.FirstOrDefault();
            set
            {
                _currencyProvider = value;
                Currencies = _currencyProvider.Currencies;
            }
        }

        public IList<ICurrency> Currencies
        {
            get => _currencies;
            set => SetProperty(ref _currencies, value);
        }

        public ICurrency SourceCurrency
        {
            get => _sourceCurrency ?? Currencies.FirstOrDefault();
            set => SetProperty(ref _sourceCurrency, value);
        }

        public ICurrency TargetCurrency
        {
            get => _targetCurrency ?? Currencies.Skip(1).FirstOrDefault();
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

        public ICommand FetchCommand { get; }
    }
}
