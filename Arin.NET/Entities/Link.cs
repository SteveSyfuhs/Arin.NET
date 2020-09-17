using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Arin.NET.Entities
{
    [DebuggerDisplay("{Type} {Value} {Href}")]
    public class Link
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("rel")]
        public string Rel { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("href")]
        public string Href { get; set; }
    }
}