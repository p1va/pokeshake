using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using PokeShake.DTO;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PokeShake.WebApi.Schemas
{
    /// <summary>
    /// The pokemon bad request error response schema filter
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.SwaggerGen.ISchemaFilter" />
    public class PokemonBadRequestErrorResponseSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Applies the specified schema.
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <param name="context">The context.</param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.ApiModel.Type == typeof(PokemonBadRequestResponse))
            {
                schema.Example = new OpenApiObject
                {
                    ["error_code"] = new OpenApiString(ErrorCodes.Pokemon.InvalidName),
                    ["error_message"] = new OpenApiString("Your request was not that good"),
                    ["error_details"] = new OpenApiObject
                    {
                        ["additional_info"] = new OpenApiString("This is an additional info"),
                        ["yet_another_additional_info"] = new OpenApiString("This is another additional info"),
                    }
                };
            }
        }
    }
}
