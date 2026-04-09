using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(ReturnUserDefinedToolToolType.ReturnUserDefinedToolToolTypeSerializer))]
[Serializable]
public readonly record struct ReturnUserDefinedToolToolType : IStringEnum
{
    public static readonly ReturnUserDefinedToolToolType Builtin = new(Values.Builtin);

    public static readonly ReturnUserDefinedToolToolType Function = new(Values.Function);

    public ReturnUserDefinedToolToolType(string value)
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
    public static ReturnUserDefinedToolToolType FromCustom(string value)
    {
        return new ReturnUserDefinedToolToolType(value);
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

    public static bool operator ==(ReturnUserDefinedToolToolType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ReturnUserDefinedToolToolType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ReturnUserDefinedToolToolType value) => value.Value;

    public static explicit operator ReturnUserDefinedToolToolType(string value) => new(value);

    internal class ReturnUserDefinedToolToolTypeSerializer
        : JsonConverter<ReturnUserDefinedToolToolType>
    {
        public override ReturnUserDefinedToolToolType Read(
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
            return new ReturnUserDefinedToolToolType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ReturnUserDefinedToolToolType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ReturnUserDefinedToolToolType ReadAsPropertyName(
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
            return new ReturnUserDefinedToolToolType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ReturnUserDefinedToolToolType value,
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
        public const string Builtin = "BUILTIN";

        public const string Function = "FUNCTION";
    }
}
