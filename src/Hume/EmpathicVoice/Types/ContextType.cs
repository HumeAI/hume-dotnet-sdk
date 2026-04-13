using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(ContextType.ContextTypeSerializer))]
[Serializable]
public readonly record struct ContextType : IStringEnum
{
    public static readonly ContextType Persistent = new(Values.Persistent);

    public static readonly ContextType Temporary = new(Values.Temporary);

    public ContextType(string value)
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
    public static ContextType FromCustom(string value)
    {
        return new ContextType(value);
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

    public static bool operator ==(ContextType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ContextType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ContextType value) => value.Value;

    public static explicit operator ContextType(string value) => new(value);

    internal class ContextTypeSerializer : JsonConverter<ContextType>
    {
        public override ContextType Read(
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
            return new ContextType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ContextType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ContextType ReadAsPropertyName(
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
            return new ContextType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ContextType value,
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
        public const string Persistent = "persistent";

        public const string Temporary = "temporary";
    }
}
