using System;
using System.Collections.Generic;
using System.Text;

namespace PokeShake.Services.FunTranslations
{
    /// <summary>
    /// The HTTP fun translations service options
    /// </summary>
    public class HttpFunTranslationsServiceOptions
    {
        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        /// <value>
        /// The base URL.
        /// </value>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the translation endpoint.
        /// </summary>
        /// <value>
        /// The translation endpoint.
        /// </value>
        public string TranslateEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the shakespeare translation endpoint.
        /// </summary>
        /// <value>
        /// The shakespeare translation endpoint.
        /// </value>
        public string ShakespeareTranslationEndpoint { get; set; }
    }
}
