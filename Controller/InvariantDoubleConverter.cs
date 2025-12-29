using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

public sealed  class InvariantDoubleConverter : JsonConverter<double>
{
    public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
        {
            return reader.GetDouble();
        }

        if (reader.TokenType == JsonTokenType.String)
        {
            var s = reader.GetString();

            if (string.IsNullOrWhiteSpace(s))
            {
                throw new JsonException("Invalid double value.");
            }

            return double.Parse(s, CultureInfo.InvariantCulture);
        }

        throw new JsonException($"Unexpected token {reader.TokenType}");
    }

    public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}