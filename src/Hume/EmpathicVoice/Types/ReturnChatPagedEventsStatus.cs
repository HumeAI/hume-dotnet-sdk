using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(ReturnChatPagedEventsStatus.ReturnChatPagedEventsStatusSerializer))]
[Serializable]
public readonly record struct ReturnChatPagedEventsStatus : IStringEnum
{
    public static readonly ReturnChatPagedEventsStatus Active = new(Values.Active);

    public static readonly ReturnChatPagedEventsStatus UserEnded = new(Values.UserEnded);

    public static readonly ReturnChatPagedEventsStatus UserTimeout = new(Values.UserTimeout);

    public static readonly ReturnChatPagedEventsStatus MaxDurationTimeout = new(
        Values.MaxDurationTimeout
    );

    public static readonly ReturnChatPagedEventsStatus InactivityTimeout = new(
        Values.InactivityTimeout
    );

    public static readonly ReturnChatPagedEventsStatus Error = new(Values.Error);

    public ReturnChatPagedEventsStatus(string value)
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
    public static ReturnChatPagedEventsStatus FromCustom(string value)
    {
        return new ReturnChatPagedEventsStatus(value);
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

    public static bool operator ==(ReturnChatPagedEventsStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ReturnChatPagedEventsStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ReturnChatPagedEventsStatus value) => value.Value;

    public static explicit operator ReturnChatPagedEventsStatus(string value) => new(value);

    internal class ReturnChatPagedEventsStatusSerializer
        : JsonConverter<ReturnChatPagedEventsStatus>
    {
        public override ReturnChatPagedEventsStatus Read(
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
            return new ReturnChatPagedEventsStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ReturnChatPagedEventsStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ReturnChatPagedEventsStatus ReadAsPropertyName(
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
            return new ReturnChatPagedEventsStatus(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ReturnChatPagedEventsStatus value,
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

        public const string MaxDurationTimeout = "MAX_DURATION_TIMEOUT";

        public const string InactivityTimeout = "INACTIVITY_TIMEOUT";

        public const string Error = "ERROR";
    }
}
