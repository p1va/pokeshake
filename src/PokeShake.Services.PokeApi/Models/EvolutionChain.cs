using Newtonsoft.Json;

namespace PokeShake.Services.PokeApi.Models
{
    /// <summary>
    /// The evolution chain class
    /// </summary>
    public class EvolutionChain
    {
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => Url;
    }
}
