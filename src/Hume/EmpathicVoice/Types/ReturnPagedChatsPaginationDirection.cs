using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(StringEnumSerializer<ReturnPagedChatsPaginationDirection>))]
[Serializable]
public readonly record struct ReturnPagedChatsPaginationDirection : IStringEnum
{
    public static readonly ReturnPagedChatsPaginationDirection Asc = new(Values.Asc);

    public static readonly ReturnPagedChatsPaginationDirection Desc = new(Values.Desc);

    public ReturnPagedChatsPaginationDirection(string value)
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
    public static ReturnPagedChatsPaginationDirection FromCustom(string value)
    {
        return new ReturnPagedChatsPaginationDirection(value);
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

    public static bool operator ==(ReturnPagedChatsPaginationDirection value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ReturnPagedChatsPaginationDirection value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ReturnPagedChatsPaginationDirection value) =>
        value.Value;

    public static explicit operator ReturnPagedChatsPaginationDirection(string value) => new(value);

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
