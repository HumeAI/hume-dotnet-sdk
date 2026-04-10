using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(ReturnWebhookEventType.ReturnWebhookEventTypeSerializer))]
[Serializable]
public readonly record struct ReturnWebhookEventType : IStringEnum
{
    public static readonly ReturnWebhookEventType ChatStarted = new(Values.ChatStarted);

    public static readonly ReturnWebhookEventType ChatEnded = new(Values.ChatEnded);

    public static readonly ReturnWebhookEventType ToolCall = new(Values.ToolCall);

    public ReturnWebhookEventType(string value)
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
    public static ReturnWebhookEventType FromCustom(string value)
    {
        return new ReturnWebhookEventType(value);
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

    public static bool operator ==(ReturnWebhookEventType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ReturnWebhookEventType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ReturnWebhookEventType value) => value.Value;

    public static explicit operator ReturnWebhookEventType(string value) => new(value);

    internal class ReturnWebhookEventTypeSerializer : JsonConverter<ReturnWebhookEventType>
    {
        public override ReturnWebhookEventType Read(
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
            return new ReturnWebhookEventType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ReturnWebhookEventType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ReturnWebhookEventType ReadAsPropertyName(
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
            return new ReturnWebhookEventType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ReturnWebhookEventType value,
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
