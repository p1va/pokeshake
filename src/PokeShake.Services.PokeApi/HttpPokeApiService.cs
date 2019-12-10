using EnsureThat;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PokeShake.Services.PokeApi.Contracts;
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
    /// <seealso cref="PokeShake.Services.Common.HttpService" />
    /// <seealso cref="PokeShake.Services.PokeApi.IPokeApiService" />
    public class HttpPokeApiService : Common.HttpService, IPokeApiService
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
        /// Initializes a new instance of the <see cref="HttpPokeApiService" /> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="options">The options.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public HttpPokeApiService(HttpClient httpClient, IOptions<HttpPokeApiServiceOptions> options, ILoggerFactory loggerFactory) : base(httpClient)
        {
            EnsureArg.IsNotNull(httpClient, nameof(httpClient));
            EnsureArg.IsNotNull(loggerFactory, nameof(loggerFactory));
            EnsureArg.IsNotNull(options.Value, nameof(options.Value));
            EnsureArg.IsNotEmptyOrWhitespace(options.Value.BaseUrl, nameof(options.Value.BaseUrl));
            EnsureArg.IsNotEmptyOrWhitespace(options.Value.SpeciesEndpoint, nameof(options.Value.SpeciesEndpoint));

            this.baseUrl = options.Value.BaseUrl;
            this.speciesEndpoint = options.Value.SpeciesEndpoint;
            this.logger = loggerFactory.CreateLogger<HttpPokeApiService>();
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
            EnsureArg.IsNotEmptyOrWhitespace(name, nameof(name));

            logger.LogDebug("Getting pokemon w/ name [{name}]", name);

            // Combine the URL
            var url = CombineUrl(baseUrl, speciesEndpoint, name);

            logger.LogTrace("Combined URL {url}", url);

            // Execute the request
            var pokemon = await GetAsync<PokemonSpecies>(url);

            logger.LogDebug("Pokemon w/ name [{name}] found:", name, pokemon);

            return pokemon;
        }
    }
}
