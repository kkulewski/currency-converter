using System.Collections.Generic;
using CConv.Services.Conversion;
using CConv.Services.CurrencyProviders;
using CConv.ViewModels;
using CConv.Views;
using Xamarin.Forms;

namespace CConv
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ICurrencyConversionService conversionService = new CurrencyConversionService();
            IList<ICurrencyProvider> providers = new List<ICurrencyProvider>
            {
                new FakeCurrencyProvider(),
                new NbpCurrencyProvider(NbpTable.A),
                new NbpCurrencyProvider(NbpTable.B)
            };

            var mainPage = new TabbedPage();
            mainPage.Children.Add(new ConvertPage(new ConvertViewModel(conversionService, providers)));
            mainPage.Children.Add(new AboutPage(new AboutViewModel("kkulewski.pl", "github.com/kkulewski")));
            MainPage = mainPage;
        }
    }
}
