using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(ReturnBuiltinToolToolType.ReturnBuiltinToolToolTypeSerializer))]
[Serializable]
public readonly record struct ReturnBuiltinToolToolType : IStringEnum
{
    public static readonly ReturnBuiltinToolToolType Builtin = new(Values.Builtin);

    public static readonly ReturnBuiltinToolToolType Function = new(Values.Function);

    public ReturnBuiltinToolToolType(string value)
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
    public static ReturnBuiltinToolToolType FromCustom(string value)
    {
        return new ReturnBuiltinToolToolType(value);
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

    public static bool operator ==(ReturnBuiltinToolToolType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ReturnBuiltinToolToolType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ReturnBuiltinToolToolType value) => value.Value;

    public static explicit operator ReturnBuiltinToolToolType(string value) => new(value);

    internal class ReturnBuiltinToolToolTypeSerializer : JsonConverter<ReturnBuiltinToolToolType>
    {
        public override ReturnBuiltinToolToolType Read(
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
            return new ReturnBuiltinToolToolType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ReturnBuiltinToolToolType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ReturnBuiltinToolToolType ReadAsPropertyName(
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
            return new ReturnBuiltinToolToolType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ReturnBuiltinToolToolType value,
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
