using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(WebhookEventChatStartType.WebhookEventChatStartTypeSerializer))]
[Serializable]
public readonly record struct WebhookEventChatStartType : IStringEnum
{
    public static readonly WebhookEventChatStartType NewChatGroup = new(Values.NewChatGroup);

    public static readonly WebhookEventChatStartType ResumedChatGroup = new(
        Values.ResumedChatGroup
    );

    public WebhookEventChatStartType(string value)
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
    public static WebhookEventChatStartType FromCustom(string value)
    {
        return new WebhookEventChatStartType(value);
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

    public static bool operator ==(WebhookEventChatStartType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(WebhookEventChatStartType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(WebhookEventChatStartType value) => value.Value;

    public static explicit operator WebhookEventChatStartType(string value) => new(value);

    internal class WebhookEventChatStartTypeSerializer : JsonConverter<WebhookEventChatStartType>
    {
        public override WebhookEventChatStartType Read(
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
            return new WebhookEventChatStartType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            WebhookEventChatStartType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override WebhookEventChatStartType ReadAsPropertyName(
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
            return new WebhookEventChatStartType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            WebhookEventChatStartType value,
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
        public const string NewChatGroup = "new_chat_group";

        public const string ResumedChatGroup = "resumed_chat_group";
    }
}
