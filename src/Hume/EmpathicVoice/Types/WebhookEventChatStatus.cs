using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(WebhookEventChatStatus.WebhookEventChatStatusSerializer))]
[Serializable]
public readonly record struct WebhookEventChatStatus : IStringEnum
{
    public static readonly WebhookEventChatStatus Active = new(Values.Active);

    public static readonly WebhookEventChatStatus UserEnded = new(Values.UserEnded);

    public static readonly WebhookEventChatStatus UserTimeout = new(Values.UserTimeout);

    public static readonly WebhookEventChatStatus InactivityTimeout = new(Values.InactivityTimeout);

    public static readonly WebhookEventChatStatus MaxDurationTimeout = new(
        Values.MaxDurationTimeout
    );

    public static readonly WebhookEventChatStatus SilenceTimeout = new(Values.SilenceTimeout);

    public static readonly WebhookEventChatStatus Error = new(Values.Error);

    public WebhookEventChatStatus(string value)
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
    public static WebhookEventChatStatus FromCustom(string value)
    {
        return new WebhookEventChatStatus(value);
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

    public static bool operator ==(WebhookEventChatStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(WebhookEventChatStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(WebhookEventChatStatus value) => value.Value;

    public static explicit operator WebhookEventChatStatus(string value) => new(value);

    internal class WebhookEventChatStatusSerializer : JsonConverter<WebhookEventChatStatus>
    {
        public override WebhookEventChatStatus Read(
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
            return new WebhookEventChatStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            WebhookEventChatStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override WebhookEventChatStatus ReadAsPropertyName(
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
            return new WebhookEventChatStatus(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            WebhookEventChatStatus value,
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
        public const string Active = "ACTIVE";

        public const string UserEnded = "USER_ENDED";

        public const string UserTimeout = "USER_TIMEOUT";

        public const string InactivityTimeout = "INACTIVITY_TIMEOUT";

        public const string MaxDurationTimeout = "MAX_DURATION_TIMEOUT";

        public const string SilenceTimeout = "SILENCE_TIMEOUT";

        public const string Error = "ERROR";
    }
}
