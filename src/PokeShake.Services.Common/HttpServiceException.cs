using System;
using System.Net;

namespace PokeShake.Services.Common
{
    /// <summary>
    /// The HTTP service exception class
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class HttpServiceException : Exception
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpServiceException"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="content">The content.</param>
        /// <param name="message">The message.</param>
        public HttpServiceException(HttpStatusCode code, string content,  string message) : base(message)
        {
            this.StatusCode = code;
            this.Content = content;
        }
    }
}
