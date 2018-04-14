using System.Collections.Generic;
using System.Linq;
using CConv.Services.Conversion;
using CConv.Services.CurrencyProviders;
using CConv.ViewModels;

namespace CConv
{
    /// <summary>
    /// Extremely naive service locator / container.
    /// </summary>
    public class Container
    {
        private static ICurrencyConversionService Conversion { get; }
        private static IList<ICurrencyProvider> Providers { get; }
        private static ConvertViewModel ConvertViewModel { get; }


        static Container()
        {
            Conversion = new CurrencyConversionService();
            Providers = new List<ICurrencyProvider>
            {
                new FakeCurrencyProvider(),
                new NbpCurrencyProvider(NbpTable.A),
                new NbpCurrencyProvider(NbpTable.B)
            };

            ConvertViewModel = new ConvertViewModel(Conversion, Providers);
        }

        public static T Resolve<T>() where T : class
        {
            var t = typeof(T);

            if (t.IsAssignableFrom(typeof(ConvertViewModel)))
                return ConvertViewModel as T;

            if (t.IsAssignableFrom(typeof(ICurrencyConversionService)))
                return Conversion as T;

            if (t.IsAssignableFrom(typeof(ICurrencyProvider)))
                return Providers.FirstOrDefault() as T;

            if (t.IsAssignableFrom(typeof(IList<ICurrencyProvider>)))
                return Providers as T;

            return null;
        }
    }
}
