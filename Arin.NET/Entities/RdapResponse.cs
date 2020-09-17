using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Arin.NET.Entities
{
    public abstract class RdapResponse
    {
        [JsonPropertyName("rdapConformance")]
        public ICollection<string> Conformance { get; set; } = new List<string>();

        [JsonPropertyName("notices")]
        public ICollection<Notice> Notices { get; set; } = new List<Notice>();
    }
}
