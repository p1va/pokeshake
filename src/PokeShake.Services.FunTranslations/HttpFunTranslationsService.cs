using EnsureThat;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PokeShake.Services.Common;
using PokeShake.Services.FunTranslations.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokeShake.Services.FunTranslations
{
    /// <summary>
    /// The HTTP implementation of the fun translations service
    /// </summary>
    /// <seealso cref="PokeShake.Services.Common.HttpService" />
    /// <seealso cref="PokeShake.Services.FunTranslations.Contracts.IFunTranslationsService" />
    public class HttpFunTranslationsService : HttpService, Contracts.IFunTranslationsService
    {
        /// <summary>
        /// The base URL
        /// </summary>
        private readonly string baseUrl;

        /// <summary>
        /// The translate endpoint
        /// </summary>
        private readonly string translateEndpoint;

        /// <summary>
        /// The shakespeare translation endpoint
        /// </summary>
        private readonly string shakespeareTranslationEndpoint;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<HttpFunTranslationsService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpFunTranslationsService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="options">The options.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public HttpFunTranslationsService(HttpClient httpClient, IOptions<HttpFunTranslationsServiceOptions> options, ILoggerFactory loggerFactory) : base(httpClient)
        {
            EnsureArg.IsNotNull(httpClient, nameof(httpClient));
            EnsureArg.IsNotNull(options.Value, nameof(options.Value));
            EnsureArg.IsNotEmptyOrWhitespace(options.Value.BaseUrl, nameof(options.Value.BaseUrl));
            EnsureArg.IsNotEmptyOrWhitespace(options.Value.TranslateEndpoint, nameof(options.Value.TranslateEndpoint));
            EnsureArg.IsNotEmptyOrWhitespace(options.Value.ShakespeareTranslationEndpoint, nameof(options.Value.ShakespeareTranslationEndpoint));

            this.baseUrl = options.Value.BaseUrl;
            this.translateEndpoint = options.Value.TranslateEndpoint;
            this.shakespeareTranslationEndpoint = options.Value.ShakespeareTranslationEndpoint;

            this.logger = loggerFactory.CreateLogger<HttpFunTranslationsService>();
        }

        /// <summary>
        /// Asynchronously translates the text into the specified translation.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="translation">The translation.</param>
        /// <returns>
        /// A task containing the translation response
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<FunTranslationsResponse> TranslateAsync(string text, string translation)
        {
            EnsureArg.IsNotEmptyOrWhitespace(text, nameof(text));
            EnsureArg.IsNotEmptyOrWhitespace(translation, nameof(translation));

            logger.LogDebug("Requesting translation {translation} of text [{text}]", translation, text);

            // Combine the URL
            var url = CombineUrl(baseUrl, translateEndpoint, translation, "?text=", text);

            logger.LogTrace("Combined URL {url}", url);

            // Execute the post
            var translationResponse = await PostAsync<object, FunTranslationsResponse>(url: url, body: null);

            logger.LogDebug("Translation {translation} of text [{text}] is [{translated}]", translation, text, translationResponse?.Contents?.Translated);

            return translationResponse;
        }

        /// <summary>
        /// Asynchronously translates the text into the shakespearean translation.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// A task containing the translation response
        /// </returns>
        public async Task<FunTranslationsResponse> TranslateShakespeareAsync(string text) => await TranslateAsync(text, shakespeareTranslationEndpoint);
    }
}
