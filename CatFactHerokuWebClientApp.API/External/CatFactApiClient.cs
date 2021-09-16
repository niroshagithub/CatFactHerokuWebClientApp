using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CatFactHerokuWebClientApp.Web.Configuration;
using CatFactHerokuWebClientApp.Web.External.Models;
using System.Threading;

namespace CatFactHerokuWebClientApp.Web.External
{
    public class CatFactApiClient : ICatFactApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CatFactApiClient> _logger;

        public CatFactApiClient(HttpClient httpClient, IOptions<ExternalServicesConfig> options, ILogger<CatFactApiClient> logger)
        {
            var externalServicesConfig = options.Value;

            httpClient.BaseAddress = new Uri(externalServicesConfig.CatFactApiUrl);

            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<FactsResultDto> GetFactsAsync(CancellationToken cancellationToken)
        {
            const string path = "api/facts";
            try
            {
                var response = await _httpClient.GetAsync(path, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var content = await response.Content.ReadAsAsync<FactsResultDto>(cancellationToken);

                return content;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Failed to get Fact data from API");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get Fact data from API");
            }
            return null;
        }
    }
}
