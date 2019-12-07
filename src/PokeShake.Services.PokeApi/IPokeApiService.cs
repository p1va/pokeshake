using PokeShake.Services.PokeApi.Models;
using System;
using System.Threading.Tasks;

namespace PokeShake.Services.PokeApi
{
    /// <summary>
    /// The PokeApi service
    /// </summary>
    public interface IPokeApiService
    {
        /// <summary>
        /// Gets the pokemon.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The specified pokemon.</returns>
        Task<PokemonSpecies> GetPokemonAsync(string name);
    }
}
