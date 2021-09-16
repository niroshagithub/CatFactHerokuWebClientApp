using System.Threading;
using System.Threading.Tasks;
using CatFactHerokuWebClientApp.Web.External;
using CatFactHerokuWebClientApp.Web.External.Models;
using CatFactHerokuWebClientApp.Web.Services;

namespace CatFactHerokuWebClientApp.IntegrationTests
{
    public class MockCatFactService: ICatFactService
    {
        public Task<byte[]> GetUserUpvotesForFactsAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
    
    public class MockCatFactApiClient: ICatFactApiClient
    {
        public Task<FactsResultDto> GetFactsAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}