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
        private readonly ICurrencyConversionService _conversionService;
        private ICurrencyProvider _currencyProvider;
        private ICurrency _sourceCurrency;
        private ICurrency _targetCurrency;
        private decimal _sourceValue;
        private decimal _targetValue;

        public ConvertViewModel()
        {
            _conversionService = new CurrencyConversionService();
            CurrencyProviders = new List<ICurrencyProvider>
            {
                new FakeCurrencyProvider()
            };

            SelectedCurrencyProvider = CurrencyProviders.FirstOrDefault();

            Currencies = SelectedCurrencyProvider.Currencies;

            SourceCurrency = Currencies.FirstOrDefault();
            TargetCurrency = Currencies.LastOrDefault();

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
            get => _currencyProvider;
            set => SetProperty(ref _currencyProvider, value);
        }

        public IList<ICurrency> Currencies { get; set; }

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

        public ICommand FetchCommand { get; }
    }
}
