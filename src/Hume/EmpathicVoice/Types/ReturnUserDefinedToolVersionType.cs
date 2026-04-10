using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(ReturnUserDefinedToolVersionType.ReturnUserDefinedToolVersionTypeSerializer))]
[Serializable]
public readonly record struct ReturnUserDefinedToolVersionType : IStringEnum
{
    public static readonly ReturnUserDefinedToolVersionType Fixed = new(Values.Fixed);

    public static readonly ReturnUserDefinedToolVersionType Latest = new(Values.Latest);

    public ReturnUserDefinedToolVersionType(string value)
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
    public static ReturnUserDefinedToolVersionType FromCustom(string value)
    {
        return new ReturnUserDefinedToolVersionType(value);
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

    public static bool operator ==(ReturnUserDefinedToolVersionType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ReturnUserDefinedToolVersionType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ReturnUserDefinedToolVersionType value) => value.Value;

    public static explicit operator ReturnUserDefinedToolVersionType(string value) => new(value);

    internal class ReturnUserDefinedToolVersionTypeSerializer
        : JsonConverter<ReturnUserDefinedToolVersionType>
    {
        public override ReturnUserDefinedToolVersionType Read(
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
            return new ReturnUserDefinedToolVersionType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ReturnUserDefinedToolVersionType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ReturnUserDefinedToolVersionType ReadAsPropertyName(
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
            return new ReturnUserDefinedToolVersionType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ReturnUserDefinedToolVersionType value,
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
        public const string Fixed = "FIXED";

        public const string Latest = "LATEST";
    }
}
