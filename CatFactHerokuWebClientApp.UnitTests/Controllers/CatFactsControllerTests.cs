using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CatFactHerokuWebClientApp.Web.Controllers;
using CatFactHerokuWebClientApp.Web.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CatFactHerokuWebClientApp.UnitTests.Controllers
{
    public class CatFactsControllerTests
    {
        private readonly Mock<ICatFactService> _mockCatFactService;
        private readonly Mock<ILogger<CatFactsController>> _mockLogger;
        private readonly Fixture _fixture;
        private readonly CancellationToken _cancellationToken;
        public CatFactsControllerTests()
        {
            _mockCatFactService = new Mock<ICatFactService>();
            _mockLogger = new Mock<ILogger<CatFactsController>>();
            _fixture = new Fixture();
            _cancellationToken = new CancellationToken();
        }

        [Fact]
        public async Task GetUserUpvotesForFactsAsync_Returns_FileContentResult()
        {
            //Arrange
            var expectedResult = _fixture.Create<byte[]>();
            _mockCatFactService.Setup(client => client.GetUserUpvotesForFactsAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(expectedResult));

            //Act
            var sut = new CatFactsController(_mockCatFactService.Object,
                _mockLogger.Object);
           
            var result = await sut.GetUserUpvotesForFactsAsync(_cancellationToken);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<FileContentResult>();
            var fileContentResult = (result as FileContentResult);
            fileContentResult?.FileContents.Should().BeEquivalentTo(expectedResult);
            fileContentResult?.FileDownloadName.Should().Be("UserFactsResult.csv");
            fileContentResult?.ContentType.Should().Be("text/csv");
        }
        
        [Fact]
        public async Task GetUserUpvotesForFactsAsync_Returns_NotFoundResultIfDataIsNotAvailable()
        {
            //Arrange
            byte[] expectedResult = null;
            _mockCatFactService.Setup(client => client.GetUserUpvotesForFactsAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(expectedResult));

            //Act
            var sut = new CatFactsController(_mockCatFactService.Object,
                _mockLogger.Object);
           
            var result = await sut.GetUserUpvotesForFactsAsync(_cancellationToken);

            //Assert
            result.Should().NotBeNull();

            result.Should().BeOfType<NotFoundResult>();
            var noContentResult = (result as NotFoundResult);

            //noContentResult.StatusCode.Should().Be(HttpResponseStaStatus404NotFound);

        }
        [Fact]
        public async Task GetUserUpvotesForFactsAsync_Returns_NoContentResultIfDataIsEmpty()
        {
            //Arrange
            byte[] expectedResult = new byte[]{};
            _mockCatFactService.Setup(client => client.GetUserUpvotesForFactsAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(expectedResult));

            //Act
            var sut = new CatFactsController(_mockCatFactService.Object,
                _mockLogger.Object);
           
            var result = await sut.GetUserUpvotesForFactsAsync(_cancellationToken);

            //Assert
            result.Should().NotBeNull();

            result.Should().BeOfType<NoContentResult>();
        }
    }
}
