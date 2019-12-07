using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PokeShake.Services.Common;
using PokeShake.Services.PokeApi.Contracts;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PokeShake.Services.PokeApi.Tests
{
    /// <summary>
    /// The PokeApiService integration tests class
    /// </summary>
    [Collection("PokeApiService Integration Tests")]
    public class PokeApiIntegrationTests
    {
        /// <summary>
        /// The test prefix
        /// </summary>
        public const string TestPrefix = "PokeApiService - ";

        /// <summary>
        /// Determines whether this instance [can get pokemon] with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        [Theory(DisplayName = TestPrefix + "Can fetch pokemon details")]
        [InlineData("charizard")]
        [InlineData("rattata")]
        [InlineData("unown")]
        [InlineData("girafarig")]
        [InlineData("samurott")]
        [InlineData("klinklang")]
        [InlineData("hawlucha")]
        [InlineData("magearna")]
        public async Task CanGetPokemon(string name)
        {
            // Arrange

            // Declare a new services collection
            var services = new ServiceCollection();

            // Add logging
            services.AddLogging();

            // Add http client 
            services.AddHttpClient<IPokeApiService, HttpPokeApiService>();

            // Configure options for http service
            services.Configure<HttpPokeApiServiceOptions>(options => 
            {
                options.BaseUrl = "https://pokeapi.co";
                options.SpeciesEndpoint = "/api/v2/pokemon-species/";
            });

            // Build the service provider
            var serviceProvider = services.BuildServiceProvider();

            // Retrieve PokeApiService instance from the service provider
            var pokeApiService = serviceProvider.GetRequiredService<IPokeApiService>();

            // Act
            var pokemon = await pokeApiService.GetPokemonAsync(name);

            // Assert
            pokemon.Should().NotBeNull("Result pokemon should have a value");
            pokemon.Name.Should().Be(name, "Provided name and result name should be the same");
            pokemon.FormDescriptions.Any().Should().BeTrue("There should be at least one description");
        }

        /// <summary>
        /// Determines whether this instance [can get pokemon not found error].
        /// </summary>
        [Fact(DisplayName = TestPrefix + "Can get pokemon not found error")]
        public async Task CanGetPokemonNotFoundError()
        {
            // Arrange

            // Declare a new services collection
            var services = new ServiceCollection();

            // Add logging
            services.AddLogging();

            // Add http client 
            services.AddHttpClient<IPokeApiService, HttpPokeApiService>();

            // Configure options for http service
            services.Configure<HttpPokeApiServiceOptions>(options =>
            {
                options.BaseUrl = "https://pokeapi.co";
                options.SpeciesEndpoint = "/api/v2/pokemon-species/";
            });

            // Build the service provider
            var serviceProvider = services.BuildServiceProvider();

            // Retrieve PokeApiService instance from the service provider
            var pokeApiService = serviceProvider.GetRequiredService<IPokeApiService>();

            // Act
            var exception = await Record.ExceptionAsync(async () => await pokeApiService.GetPokemonAsync("stefano"));

            // Assert
            exception.Should().NotBeNull("An exception should be thrown");
            exception.Should().BeOfType(typeof(HttpServiceException));
            ((HttpServiceException)exception).StatusCode.Should().Be(HttpStatusCode.NotFound, "The server should return not found");
        }
    }
}
