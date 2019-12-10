using Newtonsoft.Json;

namespace PokeShake.Services.PokeApi.Models
{
    /// <summary>
    /// The variety class
    /// </summary>
    public class Variety
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is default; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("is_default")]
        public bool IsDefault { get; set; }

        /// <summary>
        /// Gets or sets the pokemon.
        /// </summary>
        /// <value>
        /// The pokemon.
        /// </value>
        [JsonProperty("pokemon")]
        public ApiReference Pokemon { get; set; }
    }
}
