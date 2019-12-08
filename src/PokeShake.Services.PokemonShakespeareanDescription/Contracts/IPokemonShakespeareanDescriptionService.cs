using PokeShake.Services.PokemonShakespeareanDescription.Models;
using System.Threading.Tasks;

namespace PokeShake.Services.PokemonShakespeareanDescription.Contracts
{
    /// <summary>
    /// The pokemon shakespearean description service interface
    /// </summary>
    public interface IPokemonShakespeareanDescriptionService
    {
        /// <summary>
        /// Asynchronously gets the shakespearean description of a pokemon given its name.
        /// </summary>
        /// <param name="pokemonName">Name of the pokemon.</param>
        /// <returns>The result.</returns>
        Task<PokemonShakespeareanDescriptionResult> GetAsync(string pokemonName);
    }
}
