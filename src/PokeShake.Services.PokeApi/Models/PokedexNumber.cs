using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeShake.Services.PokeApi.Models
{
    public class PokedexNumber
    {
        [JsonProperty("entry_number")]
        public long EntryNumber { get; set; }

        [JsonProperty("pokedex")]
        public ApiReference Pokedex { get; set; }
    }
}
