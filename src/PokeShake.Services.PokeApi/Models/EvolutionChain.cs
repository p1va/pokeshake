using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeShake.Services.PokeApi.Models
{
    public class EvolutionChain
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
