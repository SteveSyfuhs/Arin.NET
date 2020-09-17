using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Arin.NET.Entities
{
    [DebuggerDisplay("{Name} {Type} {Handle}")]
    public class IpResponse : RdapResponse
    {
        [JsonPropertyName("handle")]
        public string Handle { get; set; }

        [JsonPropertyName("startAddress")]
        public string StartAddress { get; set; }

        [JsonPropertyName("endAddress")]
        public string EndAddress { get; set; }

        [JsonPropertyName("ipVersion")]
        public string IpVersion { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("parentHandle")]
        public string ParentHandle { get; set; }

        [JsonPropertyName("events")]
        public ICollection<Event> Events { get; set; } = new List<Event>();

        [JsonPropertyName("links")]
        public ICollection<Link> Links { get; set; } = new List<Link>();

        [JsonPropertyName("entities")]
        public ICollection<Entity> Entities { get; set; } = new List<Entity>();

        [JsonPropertyName("port43")]
        public string Port43 { get; set; }

        [JsonPropertyName("status")]
        public ICollection<string> Status { get; set; }

        [JsonPropertyName("objectClassName")]
        public string ObjectClassName { get; set; }

        [JsonPropertyName("cidr0_cidrs")]
        public ICollection<Cidr> Cidrs { get; set; } = new List<Cidr>();

        [JsonPropertyName("arin_originas0_originautnums")]
        public ICollection<long> OriginAsn { get; set; } = new List<long>();
    }
}
