using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using PokeShake.Dto;
using PokeShake.Services.PokemonShakespeareanDescription.Contracts;

namespace PokeShake.WebApi.Controllers
{
    /// <summary>
    /// The pokemon controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("pokemon")]
    [Produces("application/json")]
    public class PokemonController : ControllerBase
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<PokemonController> logger;

        /// <summary>
        /// The service
        /// </summary>
        private readonly IPokemonShakespeareanDescriptionService service;

        /// <summary>
        /// The cache
        /// </summary>
        private readonly IMemoryCache cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="PokemonController" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="service">The service.</param>
        /// <param name="cache">The cache.</param>
        public PokemonController(ILogger<PokemonController> logger, IPokemonShakespeareanDescriptionService service, IMemoryCache cache)
        {
            this.logger = logger;
            this.service = service;
            this.cache = cache;
        }

        /// <summary>
        /// Gets the shakespearean description of a pokemon given the name.
        /// </summary>
        /// <param name="name">The pokemon name.</param>
        /// <returns>
        /// Returns the specified pokemon shakespearean description
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /pokemon/charizard
        ///     {
        ///         "name": "charizard",
        ///         "description": "Charizard flies 'round the sky in search of powerful opponents. 't breathes fire of such most wondrous heat yond 't melts aught. However,  't nev'r turns its fiery breath on any opponent weaker than itself."
        ///     }
        /// </remarks>
        /// <response code="200">Returns the specified pokemon shakespearean description</response>
        /// <response code="400">If the request arguments are not correct</response>
        /// <response code="404">If the specified pokemon was not found</response>
        /// <response code="500">If something went wrong</response>
        [HttpGet("{name}")]
        [ProducesResponseType(typeof(GetPokemonResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PokemonBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PokemonNotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(InternalServerErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(string name)
        {
            logger.LogInformation(
                eventId: WebApiLoggingEvents.GetPokemonShakespeareanDescr,
                message: "User requested shakespearean description of pokemon [{name}].",
                args: name);

            // Check if the name is valid
            // TODO: Understand if this check is enough
            // Or maybe we want to check also for numbers and special chars
            if (string.IsNullOrWhiteSpace(name))
            {
                logger.LogError(
                    eventId: WebApiLoggingEvents.InvalidPokemonName,
                    message: "User provided an invalid pokemon name:[{name}]. Ending the request.",
                    args: name);

                // Create a new error response
                var badRequestError = new PokemonBadRequestResponse
                {
                    ErrorCode = ErrorCodes.Pokemon.InvalidName,
                    ErrorMessage = "Please provide a valid Pokemon name",
                    ErrorDetails = null
                };

                // Return 400 because this is a malformed request
                BadRequest(badRequestError);
            }

            // Force name to lower case
            name = name.ToLower();

            // Retrieve cache key for the pokemon
            var cacheKey = GetCacheKey(name);

            // Try to read a cached value
            if (cache.TryGetValue(key: cacheKey, value: out string cachedDescription))
            {
                logger.LogInformation(
                    eventId: WebApiLoggingEvents.GetPokemonShakespeareanDescr,
                    message: "Successfully retrieved from cache (key: {cacheKey}) shakespearean description of pokemon {name}: {description}",
                    cacheKey, name, cachedDescription);

                // Return 200 status code along with the response object
                return Ok(new GetPokemonResponse
                {
                    Name = name,
                    Description = cachedDescription
                });
            }
            else
            {
                logger.LogDebug(
                    eventId: WebApiLoggingEvents.GetPokemonShakespeareanDescr,
                    message: "Cache does not contains any value for key {key}", cacheKey);
            }

            // Call the service and retrieve the description
            var pokemonDescriptionResult = await service.GetAsync(pokemonName: name);

            logger.LogInformation(
                eventId: WebApiLoggingEvents.GetPokemonShakespeareanDescr,
                message: "Successfully retrieved shakespearean description of pokemon {name}: {description}",
                pokemonDescriptionResult.Name, pokemonDescriptionResult.Description);

            // Cache the description for further use up to 1h
            cache.Set(cacheKey, pokemonDescriptionResult.Description, TimeSpan.FromHours(1));

            // Build a successful response
            // We don't need a mapper just for this
            var successfulResponse = new GetPokemonResponse
            {
                Name = pokemonDescriptionResult.Name,
                Description = pokemonDescriptionResult.Description
            };

            // Return 200 status code along with the response object
            return Ok(successfulResponse);
        }

        /// <summary>
        /// Gets the cache key.
        /// </summary>
        /// <param name="pokemonName">Name of the pokemon.</param>
        /// <returns>The cache key.</returns>
        private static string GetCacheKey(string pokemonName) => $"pokemon-{pokemonName}";
    }
}