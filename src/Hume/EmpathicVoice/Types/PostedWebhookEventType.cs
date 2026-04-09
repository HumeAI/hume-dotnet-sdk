using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(PostedWebhookEventType.PostedWebhookEventTypeSerializer))]
[Serializable]
public readonly record struct PostedWebhookEventType : IStringEnum
{
    public static readonly PostedWebhookEventType ChatStarted = new(Values.ChatStarted);

    public static readonly PostedWebhookEventType ChatEnded = new(Values.ChatEnded);

    public static readonly PostedWebhookEventType ToolCall = new(Values.ToolCall);

    public PostedWebhookEventType(string value)
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
    public static PostedWebhookEventType FromCustom(string value)
    {
        return new PostedWebhookEventType(value);
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

    public static bool operator ==(PostedWebhookEventType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PostedWebhookEventType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PostedWebhookEventType value) => value.Value;

    public static explicit operator PostedWebhookEventType(string value) => new(value);

    internal class PostedWebhookEventTypeSerializer : JsonConverter<PostedWebhookEventType>
    {
        public override PostedWebhookEventType Read(
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
            return new PostedWebhookEventType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PostedWebhookEventType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PostedWebhookEventType ReadAsPropertyName(
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
            return new PostedWebhookEventType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PostedWebhookEventType value,
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
        public const string ChatStarted = "chat_started";

        public const string ChatEnded = "chat_ended";

        public const string ToolCall = "tool_call";
    }
}
