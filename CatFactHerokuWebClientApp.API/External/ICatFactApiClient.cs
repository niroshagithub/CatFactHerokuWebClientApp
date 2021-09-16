using System.Threading;
using System.Threading.Tasks;
using CatFactHerokuWebClientApp.Web.External.Models;

namespace CatFactHerokuWebClientApp.Web.External
{
    public interface ICatFactApiClient
    {
        Task<FactsResultDto> GetFactsAsync(CancellationToken cancellationToken);
    }
}