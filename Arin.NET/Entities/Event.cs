using System;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Arin.NET.Entities
{
    [DebuggerDisplay("{EventAction} {EventDate}")]
    public class Event
    {
        [JsonPropertyName("eventAction")]
        public string EventAction { get; set; }

        [JsonPropertyName("eventDate")]
        public DateTimeOffset EventDate { get; set; }
    }
}