using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.Tts;

[JsonConverter(typeof(TimestampType.TimestampTypeSerializer))]
[Serializable]
public readonly record struct TimestampType : IStringEnum
{
    public static readonly TimestampType Word = new(Values.Word);

    public static readonly TimestampType Phoneme = new(Values.Phoneme);

    public TimestampType(string value)
    {
        Value = value;
    }

    /// <summary>
    /// The string value of the enum.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Create a string enum with the given value.
    /// </summary>
    public static TimestampType FromCustom(string value)
    {
        return new TimestampType(value);
    }

    public bool Equals(string? other)
    {
        return Value.Equals(other);
    }

    /// <summary>
    /// Returns the string value of the enum.
    /// </summary>
    public override string ToString()
    {
        return Value;
    }

    public static bool operator ==(TimestampType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(TimestampType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(TimestampType value) => value.Value;

    public static explicit operator TimestampType(string value) => new(value);

    internal class TimestampTypeSerializer : JsonConverter<TimestampType>
    {
        public override TimestampType Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON value could not be read as a string."
                );
            return new TimestampType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            TimestampType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override TimestampType ReadAsPropertyName(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON property name could not be read as a string."
                );
            return new TimestampType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            TimestampType value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Value);
        }
    }

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Word = "word";

        public const string Phoneme = "phoneme";
    }
}
