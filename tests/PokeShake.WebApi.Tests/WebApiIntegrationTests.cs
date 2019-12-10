using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace PokeShake.WebApi.Tests
{
    /// <summary>
    /// The Web API integration tests class
    /// </summary>
    /// <seealso cref="Xunit.IClassFixture{Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory{PokeShake.WebApi.Startup}}" />
    [Collection("Web Api Integration Tests")]
    public class WebApiIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        /// <summary>
        /// The prefix
        /// </summary>
        public const string Prefix = "Web API - ";

        /// <summary>
        /// The factory
        /// </summary>
        private readonly WebApplicationFactory<PokeShake.WebApi.Startup> factory;

        /// <summary>
        /// The client
        /// </summary>
        private readonly HttpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebApiIntegrationTests"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public WebApiIntegrationTests(WebApplicationFactory<PokeShake.WebApi.Startup> factory)
        {
            this.factory = factory;
            client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://localhost:9999")
            });

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        /// <summary>
        /// Determines whether this instance [can get swagger] the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        [Fact(DisplayName = Prefix + "Can get swagger")]
        public async Task CanGetSwaggerAsync()
        {
            // Arrange
            var url = "/swagger/v1/swagger.json";

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Determines whether this instance [can get pokemon].
        /// </summary>
        [Fact(DisplayName = Prefix + "Can get pokemon")]
        public async Task CanGetPokemonAsync()
        {
            // Arrange
            var url = "/pokemon/charizard";

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();

            // Read the response into a jobject
            var jsonResponse = JObject.Parse(await response.Content.ReadAsStringAsync());

            // Check response values
            jsonResponse["name"].Value<string>().Should().Be("charizard");
            jsonResponse["description"].Value<string>().Should().Be("Charizard flies 'round the sky in search of powerful opponents. 't breathes fire of such most wondrous heat yond 't melts aught. However,  't nev'r turns its fiery breath on any opponent weaker than itself.");
        }

        /// <summary>
        /// Determines whether this instance [can get pokemon not found].
        /// </summary>
        [Fact(DisplayName = Prefix + "Can get pokemon not found error")]
        public async Task CanGetPokemonNotFoundAsync()
        {
            // Arrange
            var url = "/pokemon/stefano";

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound, "Response code should be not found");

            // Read the response into a jobject
            var jsonResponse = JObject.Parse(await response.Content.ReadAsStringAsync());

            // Check response values
            jsonResponse["error_code"].Value<string>().Should().Be("pokemon_not_found");
        }
    }
}
