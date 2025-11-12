using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(StringEnumSerializer<PostedWebhookEventType>))]
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
