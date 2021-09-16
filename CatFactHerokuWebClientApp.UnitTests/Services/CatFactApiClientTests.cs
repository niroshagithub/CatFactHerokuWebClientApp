using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using CatFactHerokuWebClientApp.Web.Configuration;
using CatFactHerokuWebClientApp.Web.External;
using CatFactHerokuWebClientApp.Web.External.Models;
using CatFactHerokuWebClientApp.Web.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace CatFactHerokuWebClientApp.UnitTests.Services
{
    public class CatFactApiClientTests
    {
        private readonly HttpClient _mockHttpClient;
        private readonly Mock<ILogger<CatFactApiClient>> _mockLogger;
        //private readonly Mock<IOptions<ExternalServicesConfig>> _mockOptions;
        private readonly IOptions<ExternalServicesConfig> _mockOptions;
        private readonly CancellationToken _cancellationToken;
        

        public CatFactApiClientTests()
        {
            _mockHttpClient = new HttpClient();
            _mockLogger = new Mock<ILogger<CatFactApiClient>>();
            _cancellationToken = new CancellationToken();
            _mockOptions = Options.Create(new ExternalServicesConfig
            {
                CatFactApiUrl = "https://localhost:1000"
            });
        }

        [Fact]
        public async Task GetFactsAsync_Returns_NullIfDataUnavailable()
        {
            //Act
            var sut = new CatFactApiClient(_mockHttpClient, 
                _mockOptions,
                _mockLogger.Object);
            
            var result = await sut.GetFactsAsync(_cancellationToken);

            //Assert
            result.Should().BeNull();
        }
        
        [Fact]
        public async Task GetFactsAsync_Returns_NullIfRequestNotSucceeds()
        {
            //Act
            var sut = new CatFactApiClient(_mockHttpClient, 
                _mockOptions,
                _mockLogger.Object);
            
            var result = await sut.GetFactsAsync(_cancellationToken);

            //Assert
            result.Should().BeNull();
        }

    }
}