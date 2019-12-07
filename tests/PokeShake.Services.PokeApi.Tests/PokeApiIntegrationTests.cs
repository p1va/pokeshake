using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PokeShake.Services.PokeApi.Tests
{
    public class PokeApiIntegrationTests
    {
        [Theory()]
        [InlineData("charizard")]
        public async Task CanGetPokemon(string name)
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddHttpClient<IPokeApiService, HttpPokeApiService>();
            services.Configure<HttpPokeApiServiceOptions>(options => 
            {
                options.BaseUrl = "https://pokeapi.co";
                options.SpeciesEndpoint = "/api/v2/pokemon-species/";
            });

            var serviceProvider = services.BuildServiceProvider();

            var pokeApiService = serviceProvider.GetRequiredService<IPokeApiService>();

            // Act
            var pokemon = await pokeApiService.GetPokemonAsync(name);

            // Assert
            pokemon.Should().NotBeNull();
            pokemon.Name.Should().Be(name);
        }
    }
}
