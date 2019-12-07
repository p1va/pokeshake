using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokeShake.Services.Common
{
    /// <summary>
    /// The generic HTTP service base class
    /// </summary>
    public abstract class GenericHttpServiceBase
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericHttpServiceBase"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        public GenericHttpServiceBase(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(string url) 
        {
            // Get the resource
            var httpResponse = await httpClient.GetAsync(url);

            // Check if response is successful (200 or 201)
            if (httpResponse.IsSuccessStatusCode)
            {
                // Read the content of the request
                var content = await httpResponse.Content.ReadAsStringAsync();

                // Convert JSON to a POCO instance
                var response = JsonConvert.DeserializeObject<T>(content);

                // Return the response instace
                return response;
            }
            else
            {
                //TODO: Handle
                throw new Exception("Something went wrong");
            }
        }

        /// <summary>
        /// Combines the URL.
        /// </summary>
        /// <param name="parts">The parts.</param>
        /// <returns></returns>
        protected string CombineUrl(params string[] parts) 
        {
            //TODO: Handle this properly: nulls, slashes etc.
            return parts.Aggregate((i, j) => i + j);
        }
    }
}
