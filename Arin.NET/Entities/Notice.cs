using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Arin.NET.Entities
{
    public class Notice
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public List<string> Description { get; set; }

        [JsonPropertyName("links")]
        public List<Link> Links { get; set; }
    }
}
