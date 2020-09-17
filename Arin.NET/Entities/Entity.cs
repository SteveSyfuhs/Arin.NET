using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Arin.NET.Entities
{
    [DebuggerDisplay("{Handle}")]
    public class Entity
    {
        [JsonPropertyName("handle")]
        public string Handle { get; set; }

        [JsonPropertyName("vcardArray")]
        public ContactCard VCard { get; set; }

        [JsonPropertyName("roles")]
        public ICollection<string> Roles { get; set; } = new List<string>();

        [JsonPropertyName("links")]
        public ICollection<Link> Links { get; set; } = new List<Link>();

        [JsonPropertyName("events")]
        public ICollection<Event> Events { get; set; } = new List<Event>();

        [JsonPropertyName("entities")]
        public ICollection<Entity> Entities { get; set; } = new List<Entity>();

        [JsonPropertyName("port43")]
        public string Port43 { get; set; }

        [JsonPropertyName("objectClassName")]
        public string ObjectClassName { get; set; }

        [JsonPropertyName("remarks")]
        public ICollection<Remark> Remarks { get; set; } = new List<Remark>();
    }

}