using System.Threading;
using System.Threading.Tasks;

namespace CatFactHerokuWebClientApp.Web.Services
{
    public interface ICatFactService
    {
        Task<byte[]> GetUserUpvotesForFactsAsync(CancellationToken cancellationToken);
    }
}