using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PokeShake.Services.PokeApi.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokeShake.Services.PokeApi
{
    /// <summary>
    /// The HttpClient based implementation of the PokeApi service
    /// </summary>
    /// <seealso cref="PokeShake.Services.Common.GenericHttpServiceBase" />
    /// <seealso cref="PokeShake.Services.PokeApi.IPokeApiService" />
    public class HttpPokeApiService : Common.GenericHttpServiceBase, IPokeApiService
    {
        /// <summary>
        /// The base URL
        /// </summary>
        private readonly string baseUrl;

        /// <summary>
        /// The species endpoint
        /// </summary>
        private readonly string speciesEndpoint;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<HttpPokeApiService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpPokeApiService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="options">The options.</param>
        public HttpPokeApiService(HttpClient httpClient, IOptions<HttpPokeApiServiceOptions> options, ILogger<HttpPokeApiService> logger) : base(httpClient)
        {
            //TODO: Handle null or white spaces maybe with ensure that
            this.baseUrl = options.Value.BaseUrl;
            this.speciesEndpoint = options.Value.SpeciesEndpoint;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the pokemon.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// The specified pokemon.
        /// </returns>
        public async Task<PokemonSpecies> GetPokemonAsync(string name)
        {
            logger.LogDebug("Getting pokemon w/ name [{name}]", name);

            var pokemon = await GetAsync<PokemonSpecies>(
                url: CombineUrl(baseUrl, speciesEndpoint, name));

            logger.LogDebug("Pokemon w/ name [{name}] found:", name, pokemon);

            return pokemon;
        }
    }
}
