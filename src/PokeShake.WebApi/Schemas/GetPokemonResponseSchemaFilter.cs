using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using PokeShake.Dto;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PokeShake.WebApi.Schemas
{
    /// <summary>
    /// The get pokemon response schema filter
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.SwaggerGen.ISchemaFilter" />
    public class GetPokemonResponseSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Applies the specified schema.
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <param name="context">The context.</param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.ApiModel.Type == typeof(GetPokemonResponse))
            {
                schema.Example = new OpenApiObject
                {
                    ["name"] = new OpenApiString("charizard"),
                    ["description"] = new OpenApiString("Charizard flies 'round the sky in search of powerful opponents. 't breathes fire of such most wondrous heat yond 't melts aught. However,  't nev'r turns its fiery breath on any opponent weaker than itself.")
                };
            }           
        }
    }
}
