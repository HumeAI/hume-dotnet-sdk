using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.Tts;

[JsonConverter(typeof(OctaveVersion.OctaveVersionSerializer))]
[Serializable]
public readonly record struct OctaveVersion : IStringEnum
{
    public static readonly OctaveVersion One = new(Values.One);

    public static readonly OctaveVersion Two = new(Values.Two);

    public OctaveVersion(string value)
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
    public static OctaveVersion FromCustom(string value)
    {
        return new OctaveVersion(value);
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

    public static bool operator ==(OctaveVersion value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(OctaveVersion value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(OctaveVersion value) => value.Value;

    public static explicit operator OctaveVersion(string value) => new(value);

    internal class OctaveVersionSerializer : JsonConverter<OctaveVersion>
    {
        public override OctaveVersion Read(
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
            return new OctaveVersion(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            OctaveVersion value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override OctaveVersion ReadAsPropertyName(
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
            return new OctaveVersion(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            OctaveVersion value,
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
        public const string One = "1";

        public const string Two = "2";
    }
}
