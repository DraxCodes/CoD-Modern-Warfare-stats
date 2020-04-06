using System;
using Newtonsoft.Json;

namespace CodMwStats.ApiWrapper.Models
{
    public class Metadata
    {
        [JsonProperty("iconUrl")]
        public Uri IconUrl { get; set; }
    }
}
