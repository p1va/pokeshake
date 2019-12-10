using Newtonsoft.Json;
using System.Collections.Generic;

namespace PokeShake.Dto
{
    /// <summary>
    /// The detailed API error response
    /// </summary>
    /// <seealso cref="PokeShake.Dto.ApiErrorResponse" />
    public class DetailedApiErrorResponse : ApiErrorResponse
    {
        /// <summary>
        /// Gets or sets the error details.
        /// </summary>
        /// <value>
        /// The error details.
        /// </value>
        [JsonProperty("error_details")]
        public IDictionary<string, string> ErrorDetails { get; set; }
    }
}
