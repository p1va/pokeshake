using System;

namespace PokeShake.Services.PokemonShakespeareanDescription.Exceptions
{
    /// <summary>
    /// The Pokemon exception class
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class PokemonGenericException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PokemonGenericException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public PokemonGenericException(string message, Exception exception) : base(message, exception)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PokemonGenericException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public PokemonGenericException(string message) : base(message)
        {

        }
    }
}
