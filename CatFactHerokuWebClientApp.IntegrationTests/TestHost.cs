using System;
using CatFactHerokuWebClientApp.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CatFactHerokuWebClientApp.IntegrationTests
{ 
    public class TestHost<TStartup> : WebApplicationFactory<TStartup> where TStartup: class
        {
            protected override void ConfigureWebHost(IWebHostBuilder builder)
            {
                builder.ConfigureServices(services =>
                {
                    // Build the service provider.
                    var sp = services.BuildServiceProvider();

                    // Create a scope to obtain a reference to the database contexts
                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                        
                    var logger = scopedServices.GetRequiredService<ILogger<TestHost<TStartup>>>();
                    
                });
            }
    }
}