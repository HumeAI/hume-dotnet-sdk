using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(PostedBuiltinToolName.PostedBuiltinToolNameSerializer))]
[Serializable]
public readonly record struct PostedBuiltinToolName : IStringEnum
{
    public static readonly PostedBuiltinToolName WebSearch = new(Values.WebSearch);

    public static readonly PostedBuiltinToolName HangUp = new(Values.HangUp);

    public PostedBuiltinToolName(string value)
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
    public static PostedBuiltinToolName FromCustom(string value)
    {
        return new PostedBuiltinToolName(value);
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

    public static bool operator ==(PostedBuiltinToolName value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PostedBuiltinToolName value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PostedBuiltinToolName value) => value.Value;

    public static explicit operator PostedBuiltinToolName(string value) => new(value);

    internal class PostedBuiltinToolNameSerializer : JsonConverter<PostedBuiltinToolName>
    {
        public override PostedBuiltinToolName Read(
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
            return new PostedBuiltinToolName(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PostedBuiltinToolName value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PostedBuiltinToolName ReadAsPropertyName(
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
            return new PostedBuiltinToolName(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PostedBuiltinToolName value,
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
