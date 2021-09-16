using CatFactHerokuWebClientApp.Web.Domain;
using CatFactHerokuWebClientApp.Web.Domain.Mappers;
using CatFactHerokuWebClientApp.Web.External.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CatFactHerokuWebClientApp.Web.DependencyInjection
{
    public static class MapperCollectionExtensions
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.TryAddScoped<IMapper<FactResult, All>, FactResultMapper>();
            return services;
        }
    }
}
