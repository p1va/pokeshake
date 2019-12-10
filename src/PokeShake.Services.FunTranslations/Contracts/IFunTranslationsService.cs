using PokeShake.Services.FunTranslations.Models;
using System.Threading.Tasks;

namespace PokeShake.Services.FunTranslations.Contracts
{
    /// <summary>
    /// The fun translations service interface
    /// </summary>
    public interface IFunTranslationsService
    {
        /// <summary>
        /// Asynchronously translates the text into the specified translation.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="translation">The translation.</param>
        /// <returns>A task containing the translation response</returns>
        Task<FunTranslationsResponse> TranslateAsync(string text, string translation);

        /// <summary>
        /// Asynchronously translates the text into the shakespearean translation.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>A task containing the translation response</returns>
        Task<FunTranslationsResponse> TranslateShakespeareAsync(string text);
    }
}
