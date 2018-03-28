using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CConv.Models;
using CConv.Services;
using CConv.Services.CurrencyProviders;
using Xamarin.Forms;

namespace CConv.ViewModels
{
    public class ConvertViewModel : BaseViewModel
    {
        public ConvertViewModel()
        {
            ConversionService = new CurrencyConversionService();

            CurrencyProviders = new List<ICurrencyProvider>
            {
                new FakeCurrencyProvider(),
                new NbpCurrencyProvider(NbpTable.A),
                new NbpCurrencyProvider(NbpTable.B)
            };

            FetchCommand = new Command(async () => await FetchCurrencies());
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
                RaisePropertyChanged(nameof(Currencies));
                CurrenciesFetched = false;
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

        public decimal TargetValue => CurrenciesFetched ? Convert : 0;

        private bool _currenciesFetched;
        public bool CurrenciesFetched
        {
            get => _currenciesFetched;
            set => SetProperty(ref _currenciesFetched, value);
        }

        private async Task FetchCurrencies()
        {
            var fetchSucceeded = await SelectedCurrencyProvider.Fetch();
            if (fetchSucceeded)
            {
                RaisePropertyChanged(nameof(Currencies));
                RaisePropertyChanged(nameof(SourceCurrency));
                RaisePropertyChanged(nameof(TargetCurrency));
                CurrenciesFetched = true;
            }
        }

        private decimal Convert => ConversionService.Convert(SourceCurrency, TargetCurrency, SourceValue);
    }
}
