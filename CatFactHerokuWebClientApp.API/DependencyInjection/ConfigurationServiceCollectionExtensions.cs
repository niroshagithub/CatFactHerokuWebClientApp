using CatFactHerokuWebClientApp.Web.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatFactHerokuWebClientApp.Web.DependencyInjection
{
    public static class ConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<ExternalServicesConfig>(config.GetSection("ExternalServices"));
       
            return services;
        }
    }
}
