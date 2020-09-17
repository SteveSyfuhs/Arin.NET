using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Arin.NET.Entities
{
    public class ErrorResponse : RdapResponse
    {
        [JsonPropertyName("errorCode")]
        public int ErrorCode { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public ICollection<string> Description { get; set; } = new List<string>();
    }
}
