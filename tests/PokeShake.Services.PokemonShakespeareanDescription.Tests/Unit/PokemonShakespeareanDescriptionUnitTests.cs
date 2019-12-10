using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using PokeShake.Services.FunTranslations.Contracts;
using PokeShake.Services.FunTranslations.Models;
using PokeShake.Services.PokeApi.Contracts;
using PokeShake.Services.PokeApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PokeShake.Services.PokemonShakespeareanDescription.Tests.Unit
{
    [Collection("PokemonShakespeareanDescription Unit Tests")]
    public class PokemonShakespeareanDescriptionUnitTests
    {
        /// The test prefix
        /// </summary>
        public const string TestPrefix = "PokeShakeDesc - ";

        /// <summary>
        /// Determines whether this instance [can throw argument exception] with the specified invalid pokemon name.
        /// </summary>
        /// <param name="invalidPokemonName">Name of the invalid pokemon.</param>
        [Theory(DisplayName = TestPrefix + "Can throw args exception")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public async Task CanThrowArgumentExceptionAsync(string invalidPokemonName)
        {
            // Arrange

            // Avoid mocking logger factory and use the null logger factory instead
            var nullLoggerFactory = new NullLoggerFactory();

            // Mock the poke api service
            var pokeApiMock = new Mock<IPokeApiService>();

            // Mock the fun translation service
            var funTranslationsMock = new Mock<IFunTranslationsService>();

            // Create a new instance of the description service with only mocked args
            var service = new PokemonShakespeareanDescriptionService(
                loggerFactory: nullLoggerFactory,
                pokeApi: pokeApiMock.Object,
                funTranslations: funTranslationsMock.Object);

            // Act
            var argException = await Record.ExceptionAsync(
                async () => await service.GetAsync(invalidPokemonName));

            // Assert
            argException.Should().NotBeNull("This should throw an exception");
        }

        /// <summary>
        /// Determines whether this instance [can get pokemon description].
        /// </summary>
        [Fact(DisplayName = TestPrefix + "Can get pokemon description")]
        public async Task CanGetPokemonDescriptionAsync()
        {
            // Arrange

            // Declare a pokemon description
            var pokemonDescription = "Charizard is very nice";

            // Declare a pokemon translated description
            var pokemonTranslatedDescription = "Nice is very Charizard";

            // Avoid mocking logger factory and use the null logger factory instead
            var nullLoggerFactory = new NullLoggerFactory();

            // Mock the poke api service
            var pokeApiMock = new Mock<IPokeApiService>();
            pokeApiMock
                .Setup(pokeApi => pokeApi.GetPokemonAsync("charizard"))
                .Returns(Task.FromResult(new PokemonSpecies
                {
                    Id = 0,
                    Name = "charizard",
                    FlavorTextEntries = new List<FlavorTextEntry>
                    {
                        new FlavorTextEntry{
                            FlavorText = pokemonDescription,
                            Language = new ApiReference{ Name = "en" } }
                    }
                }));

            // Mock the fun translation service
            var funTranslationsMock = new Mock<IFunTranslationsService>();
            funTranslationsMock
                .Setup(funTranslations => funTranslations.TranslateShakespeareAsync(pokemonDescription))
                .Returns(Task.FromResult(new FunTranslationsResponse
                {
                    Success = new Success { Total = 1 },
                    Contents = new Contents
                    {
                        Text = pokemonDescription,
                        Translated = pokemonTranslatedDescription,
                        Translation = "shakespeare"
                    }
                }));

            // Create a new instance of the description service with only mocked args
            var service = new PokemonShakespeareanDescriptionService(
                loggerFactory: nullLoggerFactory,
                pokeApi: pokeApiMock.Object,
                funTranslations: funTranslationsMock.Object);

            // Act
            var result = await service.GetAsync("charizard");

            // Assert
            result.Should().NotBeNull("Result object should not be null");
            result.Name.Should().Be("charizard", "The name of the pokemon should be the same");
            result.Description.Should().Be(pokemonTranslatedDescription, "Result description should be the translated one");
        }
    }
}
