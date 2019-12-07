using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeShake.Services.PokeApi.Models
{
    public class Variety
    {
        [JsonProperty("is_default")]
        public bool IsDefault { get; set; }

        [JsonProperty("pokemon")]
        public ApiReference Pokemon { get; set; }
    }
}
