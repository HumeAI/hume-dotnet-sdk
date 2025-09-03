using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(StringEnumSerializer<ReturnChatGroupPagedChatsPaginationDirection>))]
[Serializable]
public readonly record struct ReturnChatGroupPagedChatsPaginationDirection : IStringEnum
{
    public static readonly ReturnChatGroupPagedChatsPaginationDirection Asc = new(Values.Asc);

    public static readonly ReturnChatGroupPagedChatsPaginationDirection Desc = new(Values.Desc);

    public ReturnChatGroupPagedChatsPaginationDirection(string value)
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
    public static ReturnChatGroupPagedChatsPaginationDirection FromCustom(string value)
    {
        return new ReturnChatGroupPagedChatsPaginationDirection(value);
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
        ReturnChatGroupPagedChatsPaginationDirection value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ReturnChatGroupPagedChatsPaginationDirection value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(ReturnChatGroupPagedChatsPaginationDirection value) =>
        value.Value;

    public static explicit operator ReturnChatGroupPagedChatsPaginationDirection(string value) =>
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
