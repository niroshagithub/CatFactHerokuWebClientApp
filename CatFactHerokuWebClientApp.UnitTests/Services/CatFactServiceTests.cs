using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CatFactHerokuWebClientApp.Web.Domain;
using CatFactHerokuWebClientApp.Web.External;
using CatFactHerokuWebClientApp.Web.External.Models;
using CatFactHerokuWebClientApp.Web.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using FluentAssertions;
using System.Threading;
using CatFactHerokuWebClientApp.Web.Services.Files;

namespace CatFactHerokuWebClientApp.UnitTests.Services
{
    public class CatFactServiceTests
    {
        private readonly Mock<ICatFactApiClient> _mockCatFactApiClient;
        private readonly Mock<ILogger<CatFactService>> _mockLogger;
        private readonly Mock<IMapper<FactResult, All>> _mockMapperFactResult;
        private readonly Mock<ICsvFileBuilder> _mockCsvFileBuilder;
        private readonly CancellationToken _cancellationToken;
        public CatFactServiceTests()
        {
            _mockCatFactApiClient = new Mock<ICatFactApiClient>();
            _mockLogger = new Mock<ILogger<CatFactService>>();
            _mockMapperFactResult = new Mock<IMapper<FactResult, All>>();
            _mockCsvFileBuilder = new Mock<ICsvFileBuilder>();
            _cancellationToken = new CancellationToken();
        }

        [Fact]
        public async Task GetUserUpvotesForFactsAsync_Returns_NullIfDataUnavailable()
        {
            FactsResultDto expectedResult = null;
            _mockCatFactApiClient.Setup(client => client.GetFactsAsync(It.IsAny<CancellationToken>()))
                                 .Returns(Task.FromResult(expectedResult));

            //Act
            var sut = new CatFactService(_mockCatFactApiClient.Object,
                _mockMapperFactResult.Object,
                _mockCsvFileBuilder.Object,
                _mockLogger.Object);
            var result = await sut.GetUserUpvotesForFactsAsync(_cancellationToken);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetUserUpvotesForFactsAsync_Returns_JsonResult()
        {
            //Arrange
            const string jsonFilePath = @".\Inputs\catfacts_sample.json";
            var json = await File.ReadAllTextAsync(jsonFilePath, _cancellationToken);
            var catFactsResult = FactsResultDto.FromJson(json);
            _mockCatFactApiClient.Setup(client => client.GetFactsAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(catFactsResult));

            var factModel = catFactsResult.All.Select(x => new FactResult
            {
                Id = x.Id,
                Text = x.Text,
                Type = x.Type,
                User = new UserData
                {
                    Id = x.User.Id,
                    FirstName = x.User.Name.First,
                    LastName = x.User.Name.Last,

                },
                Upvotes = x.Upvotes,
                UserUpvoted = x.UserUpvoted
            });

            _mockMapperFactResult.Setup(map => map.ToEntity(It.IsAny<IEnumerable<All>>())).Returns(factModel);

            //Calculate the expected value
            var userUpvotesResult = GetUserUpvotes(factModel);
            var expectedResult = ConvertIEnumerableToByteArray(userUpvotesResult);

            _mockCsvFileBuilder.Setup(file => file.BuildUserUpvotesFile(It.IsAny<IEnumerable<UserUpvotes>>())).Returns(expectedResult);

            //Act
            var sut = new CatFactService(_mockCatFactApiClient.Object,
                _mockMapperFactResult.Object,
                _mockCsvFileBuilder.Object,
                _mockLogger.Object);
            var result = await sut.GetUserUpvotesForFactsAsync(_cancellationToken);

            //Assert
            result.Should().NotBeEmpty();

            result.Should().BeEquivalentTo(expectedResult);
        }

        private static IEnumerable<UserUpvotes> GetUserUpvotes(IEnumerable<FactResult> factResults)
        {
            return factResults.GroupBy(x => x.User.FullName)
                       .Select(itemGroup => new UserUpvotes
                       {
                           UserName = itemGroup.Key,
                           Upvotes = itemGroup.Sum(x => x.Upvotes)
                       }).OrderByDescending(y => y.Upvotes);

        }

        private static byte[] ConvertIEnumerableToByteArray(IEnumerable<UserUpvotes> list)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                foreach (var item in list)
                {
                    streamWriter.WriteLine(item);
                }
            }
            return memoryStream.ToArray();
        }
    }
}
