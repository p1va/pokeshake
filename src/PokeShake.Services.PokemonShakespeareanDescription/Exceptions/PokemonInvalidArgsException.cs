using System;
using System.Collections.Generic;
using System.Text;

namespace PokeShake.Services.PokemonShakespeareanDescription.Exceptions
{
    /// <summary>
    /// The pokemon invalid args exception
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class PokemonInvalidArgsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PokemonInvalidArgsException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public PokemonInvalidArgsException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
