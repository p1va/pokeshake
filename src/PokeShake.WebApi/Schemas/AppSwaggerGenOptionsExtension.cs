using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PokeShake.WebApi.Schemas
{
    /// <summary>
    /// The app swagger gen options extension class
    /// </summary>
    public static class AppSwaggerGenOptionsExtension
    {
        /// <summary>
        /// Adds the application specific schema filters to the swagger options.
        /// </summary>
        /// <param name="options">The options.</param>
        public static void AddAppSchemaFilters(this SwaggerGenOptions options) 
        {
            options.SchemaFilter<GetPokemonResponseSchemaFilter>();
            options.SchemaFilter<PokemonBadRequestErrorResponseSchemaFilter>();
            options.SchemaFilter<PokemonNotFoundErrorResponseSchemaFilter>();
            options.SchemaFilter<InternalServerErrorResponseSchemaFilter>();
        }
    }
}
