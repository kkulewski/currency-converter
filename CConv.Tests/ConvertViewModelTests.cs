using System.Collections.Generic;
using System.Linq;
using CConv.Models;
using CConv.Services.Conversion;
using CConv.Services.CurrencyProviders;
using CConv.ViewModels;
using Moq;
using Xunit;

namespace CConv.Tests
{
    public class ConvertViewModelTests
    {
        [Fact]
        public void NewInstance_GivenThreeProviders_HasFirstAsSelectedProvider()
        {
            // Arrange
            var conversionService = new Mock<ICurrencyConversionService>().Object;
            var provider1 = new Mock<ICurrencyProvider>().Object;
            var provider2 = new Mock<ICurrencyProvider>().Object;
            var provider3 = new Mock<ICurrencyProvider>().Object;
            var providers = new List<ICurrencyProvider>
            {
                provider1,
                provider2,
                provider3
            };

            // Act
            ConvertViewModel vm = new ConvertViewModel(conversionService, providers);

            // Assert
            Assert.Same(provider1, vm.SelectedCurrencyProvider);
        }

        [Fact]
        public void CanConvert_GivenProviderWithoutCurrencies_ReturnsFalse()
        {
            // Arrange
            var conversionService = new Mock<ICurrencyConversionService>().Object;
            var providerMock = new Mock<ICurrencyProvider>();
            providerMock.Setup(x => x.Currencies).Returns(new List<ICurrency>());
            var providers = new List<ICurrencyProvider> {providerMock.Object};
            var vm = new ConvertViewModel(conversionService, providers);

            // Act
            vm.SelectedCurrencyProvider = vm.CurrencyProviders.FirstOrDefault();
            var canConvert = vm.CanConvert;

            // Assert
            Assert.False(canConvert);
        }

        [Fact]
        public void CanConvert_GivenProviderWithCurrencies_ReturnsTrue()
        {
            // Arrange
            var conversionService = new Mock<ICurrencyConversionService>().Object;

            var currency = new Currency { Code = "USD" };
            var currencies = new List<ICurrency> { currency };
            var providerMock = new Mock<ICurrencyProvider>();
            providerMock.Setup(x => x.Currencies).Returns(currencies);

            var providers = new List<ICurrencyProvider> { providerMock.Object };
            var vm = new ConvertViewModel(conversionService, providers);

            // Act
            vm.SelectedCurrencyProvider = vm.CurrencyProviders.FirstOrDefault();
            var canConvert = vm.CanConvert;

            // Assert
            Assert.True(canConvert);
        }

        [Fact]
        public void Load_GivenValidCurrencyProviders_CallsLoadOnceOnEachProvider()
        {
            // Arrange
            var conversionService = new Mock<ICurrencyConversionService>().Object;
            var providerMock1 = new Mock<ICurrencyProvider>();
            providerMock1.Setup(x => x.Load()).ReturnsAsync(true);
            var providerMock2 = new Mock<ICurrencyProvider>();
            providerMock2.Setup(x => x.Load()).ReturnsAsync(true);
            var providers = new List<ICurrencyProvider> { providerMock1.Object, providerMock2.Object };
            var vm = new ConvertViewModel(conversionService, providers);

            // Act
            vm.LoadProviders().Wait();

            // Assert
            providerMock1.Verify(x => x.Load(), Times.Exactly(1));
            providerMock2.Verify(x => x.Load(), Times.Exactly(1));
        }
    }
}
