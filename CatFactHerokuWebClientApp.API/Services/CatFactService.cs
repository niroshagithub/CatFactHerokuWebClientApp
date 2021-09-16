using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CatFactHerokuApp.Infrastructure.Files;
using CatFactHerokuWebClientApp.Web.Domain;
using CatFactHerokuWebClientApp.Web.External;
using CatFactHerokuWebClientApp.Web.External.Models;
using CatFactHerokuWebClientApp.Web.Services.Files;
using Microsoft.Extensions.Logging;

namespace CatFactHerokuWebClientApp.Web.Services
{
    public class CatFactService : ICatFactService
    {
        private readonly ICatFactApiClient _catFactApiClient;
        private readonly ILogger<CatFactService> _logger;
        private readonly IMapper<FactResult, All> _mapperFactResult;
        private readonly ICsvFileBuilder _csvFileBuilder;
        public CatFactService(ICatFactApiClient catFactApiClient, 
                              IMapper<FactResult, All> mapperFactResult,
                              ICsvFileBuilder csvFileBuilder,
                              ILogger<CatFactService> logger)
        {
            _catFactApiClient = catFactApiClient;            
            _mapperFactResult = mapperFactResult;
            _csvFileBuilder = csvFileBuilder;
            _logger = logger;
        }
       
        public async Task<byte[]> GetUserUpvotesForFactsAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting user upvotes for cat facts service call");
            var facts = await _catFactApiClient.GetFactsAsync(cancellationToken);

            if (facts == null)
                return null;
            
            var factResultList = _mapperFactResult.ToEntity(facts.All);

            var result = (from fact in factResultList
                         group fact by new
                         {
                             fact.User.FullName
                         } into g
                        select new UserUpvotes { UserName = g.Key.FullName,
                                                 Upvotes = g.Sum(x=>x.Upvotes) 
                        }).OrderByDescending(x=>x.Upvotes);
            
            _logger.LogInformation($"Received user upvotes for cat facts result: {result}");
            return _csvFileBuilder.BuildUserUpvotesFile(result);
        }
    }
}
