using System.Collections.Generic;
using CConv.Services.Conversion;
using CConv.Services.CurrencyProviders;
using CConv.ViewModels;
using Xamarin.Forms;

namespace CConv
{
    public partial class App : Application
    {
        public App()
        {
            ConfigureContainer();
            InitializeComponent();
            MainPage = new Views.MainPage();
        }

        protected override void OnStart()
        {
            var providers = Container.Resolve<IList<ICurrencyProvider>>();
            foreach (var p in providers)
            {
                p.Load();
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private static void ConfigureContainer()
        {
            Container.Register(new CurrencyConversionService());
            Container.Register(new List<ICurrencyProvider>
            {
                new FakeCurrencyProvider(),
                new NbpCurrencyProvider(NbpTable.A),
                new NbpCurrencyProvider(NbpTable.B)
            });

            Container.Register(new ConvertViewModel(
                Container.Resolve<ICurrencyConversionService>(),
                Container.Resolve<IList<ICurrencyProvider>>()
            ));
        }
    }
}
