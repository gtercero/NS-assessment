using Newtonsoft.Json;

namespace NS_App.Entities
{
    public record Metadata
    {

        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("size")]
        public decimal Size { get; set; }
        [JsonProperty("creationDate")]
        public DateTime CreationDate { get; set; }
    }
}