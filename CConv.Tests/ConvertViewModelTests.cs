using System;
using System.Collections.Generic;
using System.Globalization;
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
            var currencies = new List<ICurrency> { new Currency() };
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

        [Fact]
        public void FetchCommand_FetchesCurrentlySelectedProvider()
        {
            // Arrange
            var conversionService = new Mock<ICurrencyConversionService>().Object;
            var providerMock1 = new Mock<ICurrencyProvider>();
            providerMock1.Setup(x => x.Currencies).Returns(new List<ICurrency> { new Currency() });
            var providers = new List<ICurrencyProvider> { providerMock1.Object };
            var vm = new ConvertViewModel(conversionService, providers);

            // Act
            vm.FetchCommand.Execute(null);

            // Assert
            providerMock1.Verify(x => x.Fetch(), Times.Exactly(1));
        }

        [Fact]
        public void FetchOn_GivenNotFetchedProvider_ReturnsNeverString()
        {
            // Arrange
            var conversionService = new Mock<ICurrencyConversionService>().Object;
            var providerMock1 = new Mock<ICurrencyProvider>();
            var providers = new List<ICurrencyProvider> { providerMock1.Object };
            var vm = new ConvertViewModel(conversionService, providers);

            // Act
            var fetchDate = vm.FetchedOn;

            // Assert
            Assert.Equal("never", fetchDate);
        }

        [Fact]
        public void FetchOn_GivenFetchedProvider_ReturnsExpectedDateString()
        {
            // Arrange
            var conversionService = new Mock<ICurrencyConversionService>().Object;
            var providerMock1 = new Mock<ICurrencyProvider>();
            var date = new DateTime(2010, 1, 1);
            providerMock1.Setup(x => x.UpdatedOn).Returns(date);
            var providers = new List<ICurrencyProvider> { providerMock1.Object };
            var vm = new ConvertViewModel(conversionService, providers);

            // Act
            var fetchDate = vm.FetchedOn;

            // Assert
            Assert.Equal(date.ToString(CultureInfo.InvariantCulture), fetchDate);
        }

        [Fact]
        public void SetSourceValue_RaisesPropertyChangedEvent()
        {
            // Arrange
            var conversionService = new Mock<ICurrencyConversionService>().Object;
            var providers = new List<ICurrencyProvider> { new Mock<ICurrencyProvider>().Object };
            var vm = new ConvertViewModel(conversionService, providers);

            var eventRaised = false;
            vm.PropertyChanged += (sender, args) => eventRaised = true;

            // Act
            vm.SourceValue = 1.0m;

            // Assert
            Assert.True(eventRaised);

        }
    }
}
