using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arin.NET.Entities
{
    public class JCardSerializer : JsonConverter<ContactCard>
    {
        public override ContactCard Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                return null;
            }

            reader.Read();

            if (reader.TokenType != JsonTokenType.String)
            {
                return null;
            }

            var vcardIdentifier = reader.GetString();

            if (!"vcard".Equals(vcardIdentifier, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var card = new ContactCard();

            reader.Read();

            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException($"Expected StartArray; Actual: {reader.TokenType}");
            }

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    break;
                }

                ReadCardProperty(ref reader, card);

                if (reader.TokenType != JsonTokenType.EndArray)
                {
                    throw new JsonException($"Expected EndArray; Actual: {reader.TokenType}");
                }
            }

            reader.Read();

            return card;
        }

        private void ReadCardProperty(ref Utf8JsonReader reader, ContactCard card)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException($"Expected StartArray; Actual: {reader.TokenType}");
            }

            reader.Read();

            var cardProperty = new ContactCardProperty() { Name = reader.GetString() };

            reader.Read();

            card[cardProperty.Name] = cardProperty;

            ReadCardPropertyParameters(ref reader, cardProperty);

            ReadCardPropertyValue(ref reader, cardProperty);
        }

        private void ReadCardPropertyValue(ref Utf8JsonReader reader, ContactCardProperty cardProperty)
        {
            cardProperty.Type = reader.GetString();

            reader.Read();

            bool array = false;

            if (reader.TokenType == JsonTokenType.StartArray)
            {
                array = true;

                reader.Read();
            }

            while (reader.TokenType == JsonTokenType.String)
            {
                cardProperty.Value.Add(reader.GetString());

                reader.Read();
            }

            if (array)
            {
                reader.Read();
            }
        }

        private void ReadCardPropertyParameters(ref Utf8JsonReader reader, ContactCardProperty cardProperty)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException($"Expected StartObject; Actual: {reader.TokenType}");
            }

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                var key = reader.GetString();
                reader.Read();

                object value;

                if (reader.TokenType == JsonTokenType.StartArray)
                {
                    var array = new List<string>();

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        array.Add(reader.GetString());
                    }

                    value = array;
                }
                else
                {
                    value = reader.GetString();
                }

                cardProperty.Parameters[key] = value;
            }

            reader.Read();
        }

        public override void Write(Utf8JsonWriter writer, ContactCard value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}
