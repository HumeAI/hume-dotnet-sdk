using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(StringEnumSerializer<ReturnChatStatus>))]
[Serializable]
public readonly record struct ReturnChatStatus : IStringEnum
{
    public static readonly ReturnChatStatus Active = new(Values.Active);

    public static readonly ReturnChatStatus UserEnded = new(Values.UserEnded);

    public static readonly ReturnChatStatus UserTimeout = new(Values.UserTimeout);

    public static readonly ReturnChatStatus MaxDurationTimeout = new(Values.MaxDurationTimeout);

    public static readonly ReturnChatStatus InactivityTimeout = new(Values.InactivityTimeout);

    public static readonly ReturnChatStatus Error = new(Values.Error);

    public ReturnChatStatus(string value)
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
    public static ReturnChatStatus FromCustom(string value)
    {
        return new ReturnChatStatus(value);
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

    public static bool operator ==(ReturnChatStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ReturnChatStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ReturnChatStatus value) => value.Value;

    public static explicit operator ReturnChatStatus(string value) => new(value);

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
