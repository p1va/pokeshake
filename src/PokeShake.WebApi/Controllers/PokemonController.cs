using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PokeShake.DTO;

namespace PokeShake.WebApi.Controllers
{
    [ApiController]
    [Route("pokemon")]
    [Produces("application/json")]
    public class PokemonController : ControllerBase
    {
        private readonly ILogger<PokemonController> logger;

        public PokemonController(ILogger<PokemonController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Gets the shakespearean description of a pokemon given the name.
        /// </summary>
        /// <param name="name">The pokemon name.</param>
        /// <returns>
        /// Returns the specified pokemon shakespearean description
        /// </returns>
        /// <remarks>
        /// 
        /// Example request:
        /// GET /pokemon/charizard
        /// {
        ///     "name": "charizard",
        ///     "description": "Charizard is a very nice"
        /// }
        /// 
        /// </remarks>
        /// <response code="200">Returns the specified pokemon shakespearean description</response>
        /// <response code="400">If the request arguments are not correct</response>
        /// <response code="404">If the specified pokemon was not found</response>
        /// <response code="500">If something went wrong</response>
        [HttpGet("{name}")]
        [ProducesResponseType(typeof(GetPokemonApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string name)
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
                var invalidRequestError = new DetailedApiErrorResponse 
                {
                    //TODO: Keep track of error codes
                    ErrorCode = "invalid_pokemon_name",
                    ErrorMessage = "Please provide a valid Pokemon name",
                    ErrorDetails = new Dictionary<string, string> 
                    {
                        { "some_other_key", "some other value" }
                    }
                };

                // Return 400 because this is a malformed request
                BadRequest(invalidRequestError);
            }

            //TODO: Call our service and retrieve everything
            var pokemonDescription = $"{name} is a very nice pokemon";

            logger.LogInformation(
                eventId: WebApiLoggingEvents.GetPokemonShakespeareanDescr,
                message: "Successfully retrieved shakespearean description of pokemon [{name}]: {description}",
                name, pokemonDescription);

            // Build a successful response
            var successfulResponse = new GetPokemonApiResponse
            {
                Name = name,
                Description = pokemonDescription
            };

            // Return 200 status code along with the response object
            return Ok(successfulResponse);
        }
    }
}