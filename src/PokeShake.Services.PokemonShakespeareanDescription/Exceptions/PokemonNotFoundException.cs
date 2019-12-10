using System;
using System.Collections.Generic;
using System.Text;

namespace PokeShake.Services.PokemonShakespeareanDescription.Exceptions
{
    /// <summary>
    /// The Pokemon not found exception class
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class PokemonNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PokemonNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public PokemonNotFoundException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
