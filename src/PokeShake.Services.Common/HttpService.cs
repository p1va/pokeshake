using EnsureThat;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Text;
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

            //TODO: Handle ability to set headers
        }

        /// <summary>
        /// Gets the URL asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <returns>
        /// The response.
        /// </returns>
        /// <exception cref="PokeShake.Services.Common.HttpServiceException"></exception>
        public async Task<T> GetAsync<T>(string url)
        {
            EnsureArg.IsNotEmptyOrWhitespace(url, nameof(url));

            // Get the resource
            var httpResponse = await httpClient.GetAsync(url);

            // Read the content of the response
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
        /// Posts the body to the URL asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="body">The body.</param>
        /// <returns>The response.</returns>
        /// <exception cref="PokeShake.Services.Common.HttpServiceException"></exception>
        public async Task<TResult> PostAsync<T, TResult>(string url, T body) 
        {
            EnsureArg.IsNotEmptyOrWhitespace(url, nameof(url));

            // Declare a new empty body content
            StringContent bodyContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");

            // If body was provided
            if (body != null) 
            {
                // Create a new JSON serialization of the object
                // and put it into the content w/ type application/json
                bodyContent = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            }

            // Post the resource
            var httpResponse = await httpClient.PostAsync(url, bodyContent);

            // Read the content of the response
            var content = await httpResponse.Content?.ReadAsStringAsync();

            // Check if response is successful (200 or 201)
            if (httpResponse.IsSuccessStatusCode)
            {
                // Convert JSON to a POCO instance
                var response = JsonConvert.DeserializeObject<TResult>(content);

                // Return the response instace
                return response;
            }
            else
            {
                // Throw an exception containing the details of the failure
                throw new HttpServiceException(
                    code: httpResponse.StatusCode,
                    content: content,
                    message: $"POST {url} failed with code {httpResponse.StatusCode} - {httpResponse.ReasonPhrase}");
            }
        }

        /// <summary>
        /// Combines the URL.
        /// </summary>
        /// <param name="parts">The parts.</param>
        /// <returns></returns>
        protected static string CombineUrl(params string[] parts)
        {
            //TODO: Handle this properly: nulls, double slashes etc.
            return parts.Aggregate((i, j) => i + j);
        }
    }
}
