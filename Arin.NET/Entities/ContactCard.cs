using System.Collections.Generic;
using System.Diagnostics;

namespace Arin.NET.Entities
{
    public class ContactCard : Dictionary<string, ContactCardProperty>
    {
    }

    [DebuggerDisplay("{Name} {Type} {Parameters} {Value}")]
    public class ContactCardProperty
    {
        public string Name { get; set; }

        public IDictionary<string, object> Parameters { get; } = new Dictionary<string, object>();

        public string Type { get; set; }

        public ICollection<string> Value { get; } = new List<string>();
    }
}