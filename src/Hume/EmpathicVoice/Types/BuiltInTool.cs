using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(BuiltInTool.BuiltInToolSerializer))]
[Serializable]
public readonly record struct BuiltInTool : IStringEnum
{
    public static readonly BuiltInTool WebSearch = new(Values.WebSearch);

    public static readonly BuiltInTool HangUp = new(Values.HangUp);

    public BuiltInTool(string value)
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
    public static BuiltInTool FromCustom(string value)
    {
        return new BuiltInTool(value);
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

    public static bool operator ==(BuiltInTool value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BuiltInTool value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BuiltInTool value) => value.Value;

    public static explicit operator BuiltInTool(string value) => new(value);

    internal class BuiltInToolSerializer : JsonConverter<BuiltInTool>
    {
        public override BuiltInTool Read(
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
            return new BuiltInTool(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BuiltInTool value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override BuiltInTool ReadAsPropertyName(
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
            return new BuiltInTool(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            BuiltInTool value,
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
        public const string WebSearch = "web_search";

        public const string HangUp = "hang_up";
    }
}
