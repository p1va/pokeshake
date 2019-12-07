using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeShake.Services.PokeApi.Models
{
    public partial class Name
    {
        [JsonProperty("name")]
        public string NameName { get; set; }

        [JsonProperty("language")]
        public ApiReference Language { get; set; }
    }
}
