namespace PokeShake.Services.PokeApi
{
    /// <summary>
    /// The HTTP PokeApi service options
    /// </summary>
    public class HttpPokeApiServiceOptions
    {
        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        /// <value>
        /// The base URL.
        /// </value>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the species endpoint.
        /// </summary>
        /// <value>
        /// The species endpoint.
        /// </value>
        public string SpeciesEndpoint { get; set; }
    }
}
