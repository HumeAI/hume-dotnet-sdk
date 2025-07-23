using System.Text.Json.Serialization;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Batch;

[JsonConverter(typeof(StringEnumSerializer<When>))]
[Serializable]
public readonly record struct When : IStringEnum
{
    public static readonly When CreatedBefore = new(Values.CreatedBefore);

    public static readonly When CreatedAfter = new(Values.CreatedAfter);

    public When(string value)
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
    public static When FromCustom(string value)
    {
        return new When(value);
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

    public static bool operator ==(When value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(When value1, string value2) => !value1.Value.Equals(value2);

    public static explicit operator string(When value) => value.Value;

    public static explicit operator When(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string CreatedBefore = "created_before";

        public const string CreatedAfter = "created_after";
    }
}
