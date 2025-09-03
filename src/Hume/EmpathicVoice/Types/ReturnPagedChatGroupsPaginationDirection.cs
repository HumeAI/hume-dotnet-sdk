using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(StringEnumSerializer<ReturnPagedChatGroupsPaginationDirection>))]
[Serializable]
public readonly record struct ReturnPagedChatGroupsPaginationDirection : IStringEnum
{
    public static readonly ReturnPagedChatGroupsPaginationDirection Asc = new(Values.Asc);

    public static readonly ReturnPagedChatGroupsPaginationDirection Desc = new(Values.Desc);

    public ReturnPagedChatGroupsPaginationDirection(string value)
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
    public static ReturnPagedChatGroupsPaginationDirection FromCustom(string value)
    {
        return new ReturnPagedChatGroupsPaginationDirection(value);
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
        ReturnPagedChatGroupsPaginationDirection value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ReturnPagedChatGroupsPaginationDirection value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(ReturnPagedChatGroupsPaginationDirection value) =>
        value.Value;

    public static explicit operator ReturnPagedChatGroupsPaginationDirection(string value) =>
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
