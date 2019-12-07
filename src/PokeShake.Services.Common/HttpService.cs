using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokeShake.Services.Common
{
    /// <summary>
    /// The HTTP service class
    /// </summary>
    public class HttpService
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        public HttpService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Gets the URL asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Something went wrong</exception>
        public async Task<T> GetAsync<T>(string url)
        {
            // Get the resource
            var httpResponse = await httpClient.GetAsync(url);

            // Read the content of the request
            var content = await httpResponse.Content?.ReadAsStringAsync();

            // Check if response is successful (200 or 201)
            if (httpResponse.IsSuccessStatusCode)
            {
                // Convert JSON to a POCO instance
                var response = JsonConvert.DeserializeObject<T>(content);

                // Return the response instace
                return response;
            }
            else
            {
                // Throw an exception containing the details of the failure
                throw new HttpServiceException(
                    code: httpResponse.StatusCode,
                    content: content,
                    message: $"GET {url} failed with code {httpResponse.StatusCode} - {httpResponse.ReasonPhrase}");
            }
        }

        /// <summary>
        /// Combines the URL.
        /// </summary>
        /// <param name="parts">The parts.</param>
        /// <returns></returns>
        protected static string CombineUrl(params string[] parts)
        {
            //TODO: Handle this properly: nulls, slashes etc.
            return parts.Aggregate((i, j) => i + j);
        }
    }
}
