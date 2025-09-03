using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(StringEnumSerializer<ReturnChatPagedEventsPaginationDirection>))]
[Serializable]
public readonly record struct ReturnChatPagedEventsPaginationDirection : IStringEnum
{
    public static readonly ReturnChatPagedEventsPaginationDirection Asc = new(Values.Asc);

    public static readonly ReturnChatPagedEventsPaginationDirection Desc = new(Values.Desc);

    public ReturnChatPagedEventsPaginationDirection(string value)
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
    public static ReturnChatPagedEventsPaginationDirection FromCustom(string value)
    {
        return new ReturnChatPagedEventsPaginationDirection(value);
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

    public static bool operator ==(
        ReturnChatPagedEventsPaginationDirection value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ReturnChatPagedEventsPaginationDirection value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(ReturnChatPagedEventsPaginationDirection value) =>
        value.Value;

    public static explicit operator ReturnChatPagedEventsPaginationDirection(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Asc = "ASC";

        public const string Desc = "DESC";
    }
}
