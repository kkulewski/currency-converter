using System;
using CConv.ViewModels;
using Xunit;

namespace CConv.Tests
{
    public class AboutViewModelTests
    {
        [Fact]
        public void NewInstance_HasExpectedHomeUrl()
        {
            // Arrange
            const string url = "test.com";
            var vm = new AboutViewModel(url, string.Empty);

            // Act & Assert
            Assert.Equal(url, vm.HomeUrl);
        }

        [Fact]
        public void NewInstance_HasExpectedGithubUrl()
        {
            // Arrange
            const string url = "github.com/test";
            var vm = new AboutViewModel(string.Empty, url);

            // Act & Assert
            Assert.Equal(url, vm.GitHubUrl);
        }
    }
}
