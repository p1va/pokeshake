using Newtonsoft.Json;

namespace PokeShake.Services.PokeApi.Models
{
    /// <summary>
    /// The flavor text entry class
    /// </summary>
    public class FlavorTextEntry
    {
        /// <summary>
        /// Gets or sets the flavor text.
        /// </summary>
        /// <value>
        /// The flavor text.
        /// </value>
        [JsonProperty("flavor_text")]
        public string FlavorText { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        [JsonProperty("language")]
        public ApiReference Language { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        [JsonProperty("version")]
        public ApiReference Version { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"[{Language?.Name}] {FlavorText}";
        
    }
}
