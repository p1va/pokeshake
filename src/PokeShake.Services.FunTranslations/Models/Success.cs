using Newtonsoft.Json;

namespace PokeShake.Services.FunTranslations.Models
{
    /// <summary>
    /// The success class
    /// </summary>
    public class Success
    {
        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
