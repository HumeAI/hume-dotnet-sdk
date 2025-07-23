using System.Text.Json.Serialization;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Batch;

[JsonConverter(typeof(StringEnumSerializer<Direction>))]
[Serializable]
public readonly record struct Direction : IStringEnum
{
    public static readonly Direction Asc = new(Values.Asc);

    public static readonly Direction Desc = new(Values.Desc);

    public Direction(string value)
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
    public static Direction FromCustom(string value)
    {
        return new Direction(value);
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

    public static bool operator ==(Direction value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(Direction value1, string value2) => !value1.Value.Equals(value2);

    public static explicit operator string(Direction value) => value.Value;

    public static explicit operator Direction(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Asc = "asc";

        public const string Desc = "desc";
    }
}
