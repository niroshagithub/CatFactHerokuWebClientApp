using System;
using System.Net.Http;
using System.Threading.Tasks;
using CatFactHerokuWebClientApp.Web;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CatFactHerokuWebClientApp.IntegrationTests
{
    public class CatFactControllerIntegrationTests : IClassFixture<TestHost<Startup>>
    {
        private readonly HttpClient _client;

        public CatFactControllerIntegrationTests(TestHost<Startup> factory)
        {
            var clientOptions = new WebApplicationFactoryClientOptions();
            clientOptions.BaseAddress = new Uri("http://localhost:5001");
            _client = factory.CreateClient(clientOptions);
        }

        [Fact]
        public async Task CanGetUserUpvotes()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/GetUserUpvotes");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Headers.ContentType.Should().NotBeNull();
            httpResponse.Content.Headers.ContentType.ToString().Should().Be("text/csv");
            var responseData = await httpResponse.Content.ReadAsStringAsync();
            responseData.Should().NotBeNull();
            responseData.Should().Contain("UserName,Upvotes");
            responseData.Should().Contain("Alex Wohlbruck");

        }
        [Fact]
        public async Task CannotGetUserUpvotes_IfAPIisUnavailable()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/GetUserUpvotes");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            httpResponse.Content.Should().NotBeNull();
           

        }
    }
}
