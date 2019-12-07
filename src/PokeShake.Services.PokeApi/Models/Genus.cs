using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeShake.Services.PokeApi.Models
{
    public class Genus
    {
        [JsonProperty("genus")]
        public string Name { get; set; }

        [JsonProperty("language")]
        public ApiReference Language { get; set; }
    }
}
