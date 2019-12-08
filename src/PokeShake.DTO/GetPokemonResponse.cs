using Newtonsoft.Json;

namespace PokeShake.Dto
{
    /// <summary>
    /// The GET Pokemon response
    /// </summary>
    /// <seealso cref="PokeShake.Dto.ApiResponseBase" />
    public class GetPokemonResponse : ApiResponseBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPokemonResponse"/> class.
        /// </summary>
        public GetPokemonResponse()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPokemonResponse"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        public GetPokemonResponse(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
