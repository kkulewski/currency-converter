using System.Collections.Generic;
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
    }
}
