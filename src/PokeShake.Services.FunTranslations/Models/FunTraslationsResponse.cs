using Newtonsoft.Json;

namespace PokeShake.Services.FunTranslations.Models
{
    /// <summary>
    /// The fun translations response
    /// </summary>
    public class FunTranslationsResponse
    {
        /// <summary>
        /// Gets or sets the success.
        /// </summary>
        /// <value>
        /// The success.
        /// </value>
        [JsonProperty("success")]
        public Success Success { get; set; }

        /// <summary>
        /// Gets or sets the contents.
        /// </summary>
        /// <value>
        /// The contents.
        /// </value>
        [JsonProperty("contents")]
        public Contents Contents { get; set; }
    }
}
