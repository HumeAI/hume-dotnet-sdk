using System.Text.Json.Serialization;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

[JsonConverter(typeof(StringEnumSerializer<ReturnWebhookEventType>))]
[Serializable]
public readonly record struct ReturnWebhookEventType : IStringEnum
{
    public static readonly ReturnWebhookEventType ChatStarted = new(Values.ChatStarted);

    public static readonly ReturnWebhookEventType ChatEnded = new(Values.ChatEnded);

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

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string ChatStarted = "chat_started";

        public const string ChatEnded = "chat_ended";
    }
}
