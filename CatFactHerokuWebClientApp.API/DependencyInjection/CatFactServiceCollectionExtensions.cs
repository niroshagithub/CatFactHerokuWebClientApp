using CatFactHerokuApp.Infrastructure.Files;
using CatFactHerokuWebClientApp.Web.External;
using CatFactHerokuWebClientApp.Web.Services;
using CatFactHerokuWebClientApp.Web.Services.Files;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CatFactHerokuWebClientApp.Web.DependencyInjection
{
    public static class CatFactServiceCollectionExtensions
    {
        public static IServiceCollection AddCatFactServices(this IServiceCollection services)
        {
            services.AddHttpClient<ICatFactApiClient, CatFactApiClient>();
            services.TryAddScoped<ICatFactService, CatFactService>();
            services.TryAddScoped<ICsvFileBuilder, CsvFileBuilder>();

            return services;
        }
    }
}
