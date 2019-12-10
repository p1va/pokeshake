using EnsureThat;
using Microsoft.Extensions.Logging;
using PokeShake.Services.Common;
using PokeShake.Services.FunTranslations.Contracts;
using PokeShake.Services.FunTranslations.Models;
using PokeShake.Services.PokeApi.Contracts;
using PokeShake.Services.PokeApi.Models;
using PokeShake.Services.PokemonShakespeareanDescription.Contracts;
using PokeShake.Services.PokemonShakespeareanDescription.Exceptions;
using PokeShake.Services.PokemonShakespeareanDescription.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PokeShake.Services.PokemonShakespeareanDescription
{
    /// <summary>
    /// The pokemon shakespearean description service implementation
    /// </summary>
    /// <seealso cref="PokeShake.Services.PokemonShakespeareanDescription.Contracts.IPokemonShakespeareanDescriptionService" />
    public class PokemonShakespeareanDescriptionService : IPokemonShakespeareanDescriptionService
    {
        /// <summary>
        /// The description default language
        /// </summary>
        private const string DescriptionDefaultLanguage = "en";

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<PokemonShakespeareanDescriptionService> logger;

        /// <summary>
        /// The poke API service
        /// </summary>
        private readonly IPokeApiService pokeApiService;

        /// <summary>
        /// The fun translations service
        /// </summary>
        private readonly IFunTranslationsService funTranslationsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PokemonShakespeareanDescriptionService"/> class.
        /// </summary>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="pokeApi">The poke API.</param>
        /// <param name="funTranslations">The fun translations.</param>
        public PokemonShakespeareanDescriptionService(ILoggerFactory loggerFactory, IPokeApiService pokeApi, IFunTranslationsService funTranslations)
        {
            EnsureArg.IsNotNull(loggerFactory, nameof(loggerFactory));
            EnsureArg.IsNotNull(pokeApi, nameof(pokeApi));
            EnsureArg.IsNotNull(funTranslations, nameof(funTranslations));

            this.logger = loggerFactory.CreateLogger<PokemonShakespeareanDescriptionService>();
            this.pokeApiService = pokeApi;
            this.funTranslationsService = funTranslations;
        }


        /// <summary>
        /// Asynchronously gets the shakespearean description of a pokemon given its name.
        /// </summary>
        /// <param name="pokemonName">Name of the pokemon.</param>
        /// <returns>
        /// The result.
        /// </returns>
        /// <exception cref="PokeShake.Services.PokemonShakespeareanDescription.Exceptions.PokemonNotFoundException">No pokemon found having name {pokemon}</exception>
        /// <exception cref="PokeShake.Services.PokemonShakespeareanDescription.Exceptions.PokemonInvalidArgsException">Pokemon name {pokemonName} is not valid</exception>
        /// <exception cref="PokeShake.Services.PokemonShakespeareanDescription.Exceptions.PokemonGenericException">
        /// An error occurred retrieving pokemon {pokemonName} info
        /// or
        /// An unexpected error occurred while retrieving pokemon {pokemonName} info
        /// or
        /// Pokemon {pokemonName} has no description
        /// or
        /// Pokemon {pokemonName} has no description in the preferred language ({DescriptionDefaultLanguage})
        /// or
        /// An error occourred while retrieving shakespearean translation of {pokemonName}'s description
        /// or
        /// Failed executing shakespearean translation of {pokemonName}'s description
        /// </exception>
        public async Task<PokemonShakespeareanDescriptionResult> GetAsync(string pokemonName)
        {
            EnsureArg.IsNotEmptyOrWhitespace(pokemonName, nameof(pokemonName));

            logger.LogInformation("Getting shakespearean description for pokemon {pokemonName}", pokemonName);

            // Declare a null pokemon object
            PokemonSpecies pokemon = null;

            logger.LogDebug("Calling PokeApi service requesting info about pokemon {pokemonName}", pokemonName);

            try
            {
                // Retrieve data from the PokeAPI service
                pokemon = await pokeApiService.GetPokemonAsync(pokemonName);
            }
            catch (HttpServiceException serviceException)
            {
                logger.LogError(
                    exception: serviceException,
                    message: "Error occurred while retrieving pokemon {pokemonName} info from pokeapi service",
                    args: pokemonName);

                // Perform a switch based on the exception HTTP status code
                switch (serviceException.StatusCode)
                {
                    // This is the case where a pokemon was not found
                    case System.Net.HttpStatusCode.NotFound:
                        throw new PokemonNotFoundException($"No pokemon found having name {pokemonName}", serviceException);

                    // This is the case where something illegal was sent
                    case System.Net.HttpStatusCode.BadRequest:
                        throw new PokemonInvalidArgsException($"Pokemon name {pokemonName} is not valid", serviceException);

                    // This is the default case
                    default:
                        throw new PokemonGenericException($"An error occurred retrieving pokemon {pokemonName} info", serviceException);

                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error occurred while retrieving pokemon {pokemonName} info from pokeapi service", pokemonName);

                // Not sure what is going one here since it is not an expected exception
                // Re throw the exception up and include stack
                throw new PokemonGenericException($"An unexpected error occurred while retrieving pokemon {pokemonName} info", ex);
            }

            // Handle null pokemon case even if it is unlikely to happen
            if (pokemon == null)
            {
                logger.LogError("PokeAPI response for pokemon {pokemonName} is null", pokemonName);

                throw new PokemonGenericException($"An unexpected error occurred while retrieving pokemon {pokemonName} info");
            }

            logger.LogDebug("Found a match for pokemon {pokemonName}. It has ID {id}", pokemonName, pokemon.Id);

            logger.LogTrace("Pokemon {pokemonName} has {count} descriptions", pokemonName, pokemon.Id, pokemon.FlavorTextEntries?.Count);

            // Check if the pokemon does have any descriptions
            if (pokemon.FlavorTextEntries == null || !pokemon.FlavorTextEntries.Any())
            {
                logger.LogError("Pokemon {pokemonName} has null or empty FlavorTextEntries field", pokemonName);

                // This is the case where the pokemon does not have any descriptions
                throw new PokemonGenericException($"Pokemon {pokemonName} has no description");
            }

            // Retrieve the first pokemon description in the specified language
            var description = pokemon.FlavorTextEntries.FirstOrDefault(desc => desc?.Language?.Name == DescriptionDefaultLanguage);
            if (description == null)
            {
                logger.LogError("Pokemon {pokemonName} has no description matching langugage {language}", pokemonName, DescriptionDefaultLanguage);

                // This is the case where none of the descriptions are in the desired language
                throw new PokemonGenericException($"Pokemon {pokemonName} has no description in the preferred language ({DescriptionDefaultLanguage})");
            }

            // Clean the description by replacing new lines with spaces
            var cleanDescription = description.FlavorText.Replace("\n", " ").Trim();

            logger.LogTrace("Cleaned pokemon {pokemonName} description [{desc}]", pokemonName, cleanDescription);

            logger.LogDebug("Requesting shakespearean translation for text [{desc}]", cleanDescription);

            // Declare a null translation response
            FunTranslationsResponse translationResponse = null;

            try
            {
                // Call translation service
                translationResponse = await funTranslationsService.TranslateShakespeareAsync(cleanDescription);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    exception: ex,
                    message: "An error occourred while retrieving shakespearean translation of {pokemon}'s description",
                    args: pokemonName);

                throw new PokemonGenericException($"An error occourred while retrieving shakespearean translation of {pokemonName}'s description", ex);
            }

            // Check that response is successful and contains data
            if (translationResponse == null ||
                translationResponse.Success == null ||
                translationResponse.Success.Total <= 0 ||
                translationResponse.Contents == null ||
                string.IsNullOrWhiteSpace(translationResponse.Contents.Translated))
            {
                logger.LogError("Translation response of {pokemon}'s description is null or empty", pokemonName);

                throw new PokemonGenericException($"Failed executing shakespearean translation of {pokemonName}'s description");
            }

            // Get the translated description
            var translatedDescription = translationResponse.Contents.Translated;

            logger.LogDebug("Successsfully retrieved {pokemon}'s shakespearean description [{desc}]", pokemonName, translatedDescription);

            // Return the description result object
            return new PokemonShakespeareanDescriptionResult
            {
                Name = pokemonName,
                Description = translatedDescription
            }; ;
        }
    }
}
