using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(StringEnumSerializer<WebhookEventChatStartType>))]
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
