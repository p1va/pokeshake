using Newtonsoft.Json;
using System.Collections.Generic;

namespace PokeShake.Services.PokeApi.Models
{
    /// <summary>
    /// The pokemon species class
    /// </summary>
    public class PokemonSpecies
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        [JsonProperty("order")]
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the gender rate.
        /// </summary>
        /// <value>
        /// The gender rate.
        /// </value>
        [JsonProperty("gender_rate")]
        public int GenderRate { get; set; }

        /// <summary>
        /// Gets or sets the capture rate.
        /// </summary>
        /// <value>
        /// The capture rate.
        /// </value>
        [JsonProperty("capture_rate")]
        public int CaptureRate { get; set; }

        /// <summary>
        /// Gets or sets the base happiness.
        /// </summary>
        /// <value>
        /// The base happiness.
        /// </value>
        [JsonProperty("base_happiness")]
        public int BaseHappiness { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is baby.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is baby; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("is_baby")]
        public bool IsBaby { get; set; }

        /// <summary>
        /// Gets or sets the hatch counter.
        /// </summary>
        /// <value>
        /// The hatch counter.
        /// </value>
        [JsonProperty("hatch_counter")]
        public int HatchCounter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has gender differences.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has gender differences; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("has_gender_differences")]
        public bool HasGenderDifferences { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [forms switchable].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [forms switchable]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("forms_switchable")]
        public bool FormsSwitchable { get; set; }

        /// <summary>
        /// Gets or sets the growth rate.
        /// </summary>
        /// <value>
        /// The growth rate.
        /// </value>
        [JsonProperty("growth_rate")]
        public ApiReference GrowthRate { get; set; }

        /// <summary>
        /// Gets or sets the pokedex numbers.
        /// </summary>
        /// <value>
        /// The pokedex numbers.
        /// </value>
        [JsonProperty("pokedex_numbers")]
        public List<PokedexNumber> PokedexNumbers { get; set; }

        /// <summary>
        /// Gets or sets the egg groups.
        /// </summary>
        /// <value>
        /// The egg groups.
        /// </value>
        [JsonProperty("egg_groups")]
        public List<ApiReference> EggGroups { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        [JsonProperty("color")]
        public ApiReference Color { get; set; }

        /// <summary>
        /// Gets or sets the shape.
        /// </summary>
        /// <value>
        /// The shape.
        /// </value>
        [JsonProperty("shape")]
        public ApiReference Shape { get; set; }

        /// <summary>
        /// Gets or sets the evolves from species.
        /// </summary>
        /// <value>
        /// The evolves from species.
        /// </value>
        [JsonProperty("evolves_from_species")]
        public ApiReference EvolvesFromSpecies { get; set; }

        /// <summary>
        /// Gets or sets the evolution chain.
        /// </summary>
        /// <value>
        /// The evolution chain.
        /// </value>
        [JsonProperty("evolution_chain")]
        public EvolutionChain EvolutionChain { get; set; }

        /// <summary>
        /// Gets or sets the habitat.
        /// </summary>
        /// <value>
        /// The habitat.
        /// </value>
        [JsonProperty("habitat")]
        public object Habitat { get; set; }

        /// <summary>
        /// Gets or sets the generation.
        /// </summary>
        /// <value>
        /// The generation.
        /// </value>
        [JsonProperty("generation")]
        public ApiReference Generation { get; set; }

        /// <summary>
        /// Gets or sets the names.
        /// </summary>
        /// <value>
        /// The names.
        /// </value>
        [JsonProperty("names")]
        public List<SpeciesName> Names { get; set; }

        /// <summary>
        /// Gets or sets the flavor text entries.
        /// </summary>
        /// <value>
        /// The flavor text entries.
        /// </value>
        [JsonProperty("flavor_text_entries")]
        public List<FlavorTextEntry> FlavorTextEntries { get; set; }

        /// <summary>
        /// Gets or sets the form descriptions.
        /// </summary>
        /// <value>
        /// The form descriptions.
        /// </value>
        [JsonProperty("form_descriptions")]
        public List<FormDescription> FormDescriptions { get; set; }

        /// <summary>
        /// Gets or sets the genera.
        /// </summary>
        /// <value>
        /// The genera.
        /// </value>
        [JsonProperty("genera")]
        public List<Genus> Genera { get; set; }

        /// <summary>
        /// Gets or sets the varieties.
        /// </summary>
        /// <value>
        /// The varieties.
        /// </value>
        [JsonProperty("varieties")]
        public List<Variety> Varieties { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{Name} ( {Id} )";
    }
}
