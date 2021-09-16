using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CatFactHerokuWebClientApp.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CatFactHerokuWebClientApp.Web.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CatFactsController : ControllerBase
    {
        private readonly ICatFactService _catFactService;
        private readonly ILogger<CatFactsController> _logger;
        public CatFactsController(ICatFactService catFactService, ILogger<CatFactsController> logger)
        {
            _catFactService = catFactService;
            _logger = logger;
        }
       
        [HttpGet("GetUserUpvotes")]
        public async Task<IActionResult> GetUserUpvotesForFactsAsync(CancellationToken cancellationToken)
        {    
            var result = await _catFactService.GetUserUpvotesForFactsAsync(cancellationToken);
            if (result == null)
            {
                return NotFound();
            }
            if (!result.Any())
            {
                return NoContent();
            }
            return File(result, "text/csv", "UserFactsResult.csv");
        }
    }
}
