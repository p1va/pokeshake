using Newtonsoft.Json;

namespace PokeShake.Dto
{
    /// <summary>
    /// The API error response
    /// </summary>
    /// <seealso cref="PokeShake.Dto.ApiResponseBase" />
    public class ApiErrorResponse : ApiResponseBase
    {
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }
    }
}
