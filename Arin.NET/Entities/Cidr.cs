using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Arin.NET.Entities
{
    [DebuggerDisplay("{V4Prefix} {Length}")]
    public class Cidr
    {
        [JsonPropertyName("v4prefix")]
        public string V4Prefix { get; set; }

        [JsonPropertyName("length")]
        public int Length { get; set; }
    }
}