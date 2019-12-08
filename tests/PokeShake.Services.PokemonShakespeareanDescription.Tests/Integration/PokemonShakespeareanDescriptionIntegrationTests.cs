using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PokeShake.Services.FunTranslations;
using PokeShake.Services.FunTranslations.Contracts;
using PokeShake.Services.PokeApi;
using PokeShake.Services.PokeApi.Contracts;
using PokeShake.Services.PokemonShakespeareanDescription.Contracts;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PokeShake.Services.PokemonShakespeareanDescription.Tests.Integration
{
    [Collection("PokemonShakespeareanDescription Integration Tests")]
    public class PokemonShakespeareanDescriptionIntegrationTests
    {
        /// <summary>
        /// The test prefix
        /// </summary>
        public const string TestPrefix = "PokemonShakespeareanDescription - ";

        /// <summary>
        /// Determines whether this instance [can get shakespearean translation] the specified pokemon name.
        /// </summary>
        /// <param name="pokemonName">Name of the pokemon.</param>
        [Theory(DisplayName = TestPrefix + "Can translate")]
        [InlineData("charizard")]
        public async Task CanGetShakespeareanTranslationAsync(string pokemonName)
        {
            // Declare a new services collection
            var services = new ServiceCollection();

            // Add logging
            services.AddLogging();

            // Add http client 
            services.AddHttpClient<IFunTranslationsService, HttpFunTranslationsService>();

            // Configure options for http service
            services.Configure<HttpFunTranslationsServiceOptions>(options =>
            {
                options.BaseUrl = "https://api.funtranslations.com";
                options.TranslateEndpoint = "/translate/";
                options.ShakespeareTranslationEndpoint = "shakespeare.json";
            });

            // Add http client 
            services.AddHttpClient<IPokeApiService, HttpPokeApiService>();

            // Configure options for http service
            services.Configure<HttpPokeApiServiceOptions>(options =>
            {
                options.BaseUrl = "https://pokeapi.co";
                options.SpeciesEndpoint = "/api/v2/pokemon-species/";
            });

            // Add service implementation
            services.AddTransient<IPokemonShakespeareanDescriptionService, PokemonShakespeareanDescriptionService>();

            // Build the service provider
            var serviceProvider = services.BuildServiceProvider();

            // Retrieve PokemonShakespeareanDescriptionService instance from the service provider
            var service = serviceProvider.GetRequiredService<IPokemonShakespeareanDescriptionService>();

            // Act
            var result = await service.GetAsync(pokemonName);

            // Assert
            result.Should().NotBeNull("The result should not be null");
            result.Name.Should().Be(pokemonName, "The name should be the same");
            result.Description.Should().NotBeNullOrWhiteSpace("Description should contain something");
        }
    }
}
