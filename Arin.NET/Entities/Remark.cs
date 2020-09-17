using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Arin.NET.Entities
{
    [DebuggerDisplay("{Title}: {Description}")]
    public class Remark
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public ICollection<string> Description { get; set; } = new List<string>();
    }
}