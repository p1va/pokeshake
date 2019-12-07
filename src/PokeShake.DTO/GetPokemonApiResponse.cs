using Newtonsoft.Json;

namespace PokeShake.DTO
{
    /// <summary>
    /// The GET Pokemon API response
    /// </summary>
    /// <seealso cref="PokeShake.DTO.ApiResponseBase" />
    public class GetPokemonApiResponse : ApiResponseBase
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
        /// Initializes a new instance of the <see cref="GetPokemonApiResponse"/> class.
        /// </summary>
        public GetPokemonApiResponse()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPokemonApiResponse"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        public GetPokemonApiResponse(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
