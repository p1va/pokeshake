using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        /// Initializes a new instance of the <see cref="PokemonController" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="service">The service.</param>
        public PokemonController(ILogger<PokemonController> logger, IPokemonShakespeareanDescriptionService service)
        {
            this.logger = logger;
            this.service = service;
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
                    //TODO: Keep track of error codes
                    ErrorCode = ErrorCodes.Pokemon.InvalidName,
                    ErrorMessage = "Please provide a valid Pokemon name",
                    ErrorDetails = new Dictionary<string, string> 
                    {
                        { "some_other_key", "some other value" }
                    }
                };

                // Return 400 because this is a malformed request
                BadRequest(badRequestError);
            }

            // Force name to lower case
            name = name.ToLower();

            // Call the service and retrieve the description
            var pokemonDescriptionResult = await service.GetAsync(pokemonName: name);

            logger.LogInformation(
                eventId: WebApiLoggingEvents.GetPokemonShakespeareanDescr,
                message: "Successfully retrieved shakespearean description of pokemon {name}: {description}",
                pokemonDescriptionResult.Name, pokemonDescriptionResult.Description);

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
    }
}