using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CConv.Models;
using CConv.Services.Conversion;
using CConv.Services.CurrencyProviders;
using Xamarin.Forms;

namespace CConv.ViewModels
{
    public class ConvertViewModel : BaseViewModel
    {
        public ConvertViewModel(ICurrencyConversionService conversionService, IList<ICurrencyProvider> providers)
        {
            FetchCommand = new Command(async () => await FetchCurrencies());
            ConversionService = conversionService;
            CurrencyProviders = providers;
        }

        public ICurrencyConversionService ConversionService { get; set; }

        public IList<ICurrencyProvider> CurrencyProviders { get; set; }

        private ICurrencyProvider _currencyProvider;
        public ICurrencyProvider SelectedCurrencyProvider
        {
            get => _currencyProvider ?? CurrencyProviders.FirstOrDefault();
            set
            {
                SetProperty(ref _currencyProvider, value);
                CanConvert = _currencyProvider.Currencies.Any();
            }
        }

        public ICommand FetchCommand { get; }

        public IList<ICurrency> Currencies => SelectedCurrencyProvider.Currencies;

        private ICurrency _sourceCurrency;
        public ICurrency SourceCurrency
        {
            get => _sourceCurrency ?? Currencies.FirstOrDefault();
            set
            {
                SetProperty(ref _sourceCurrency, value);
                RaisePropertyChanged(nameof(TargetValue));
            }
        }

        private ICurrency _targetCurrency;
        public ICurrency TargetCurrency
        {
            get => _targetCurrency ?? Currencies.FirstOrDefault();
            set
            {
                SetProperty(ref _targetCurrency, value);
                RaisePropertyChanged(nameof(TargetValue));
            }
        }

        private decimal _sourceValue;
        public decimal SourceValue
        {
            get => _sourceValue;
            set
            {
                SetProperty(ref _sourceValue, value);
                RaisePropertyChanged(nameof(TargetValue));
            }
        }

        public decimal TargetValue =>
            CanConvert
            ? ConversionService.Convert(SourceCurrency, TargetCurrency, SourceValue)
            : 0;

        private bool _canConvert;
        public bool CanConvert
        {
            get => _canConvert;
            set
            {
                SetProperty(ref _canConvert, value);
                RaiseRefreshNeeded();
            } 
        }

        public string FetchedOn
        {
            get
            {
                var up = SelectedCurrencyProvider.UpdatedOn;
                return up > DateTime.MinValue ? up.ToString(CultureInfo.InvariantCulture) : "never";
            }
        }

        private async Task FetchCurrencies()
        {
            var fetchSucceeded = await SelectedCurrencyProvider.Fetch();
            if (fetchSucceeded)
            {
                CanConvert = _currencyProvider.Currencies.Any();
            }
        }

        private void RaiseRefreshNeeded()
        {
            RaisePropertyChanged(nameof(FetchedOn));
            RaisePropertyChanged(nameof(Currencies));
            RaisePropertyChanged(nameof(SourceCurrency));
            RaisePropertyChanged(nameof(TargetCurrency));
            RaisePropertyChanged(nameof(TargetValue));
        }

        public async Task LoadProviders()
        {
            foreach (var p in CurrencyProviders)
            {
                await p.Load();
            }
        }
    }
}
