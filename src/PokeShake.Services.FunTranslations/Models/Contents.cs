using Newtonsoft.Json;

namespace PokeShake.Services.FunTranslations.Models
{
    /// <summary>
    /// The contents class
    /// </summary>
    public class Contents
    {
        /// <summary>
        /// Gets or sets the translated.
        /// </summary>
        /// <value>
        /// The translated.
        /// </value>
        [JsonProperty("translated")]
        public string Translated { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the translation.
        /// </summary>
        /// <value>
        /// The translation.
        /// </value>
        [JsonProperty("translation")]
        public string Translation { get; set; }
    }
}
