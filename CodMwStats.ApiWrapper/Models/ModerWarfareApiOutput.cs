using Newtonsoft.Json;

namespace CodMwStats.ApiWrapper.Models
{
    public class ModerWarfareApiOutput
    {
        [JsonProperty("data")]
        public ModernWarfareUser Data { get; set; }
    }
}
