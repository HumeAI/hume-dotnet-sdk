using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.Tts;

[JsonConverter(typeof(StringEnumSerializer<TimestampType>))]
[Serializable]
public readonly record struct TimestampType : IStringEnum
{
    public static readonly TimestampType Word = new(Values.Word);

    public static readonly TimestampType Phoneme = new(Values.Phoneme);

    public TimestampType(string value)
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
    public static TimestampType FromCustom(string value)
    {
        return new TimestampType(value);
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

    public static bool operator ==(TimestampType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(TimestampType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(TimestampType value) => value.Value;

    public static explicit operator TimestampType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Word = "word";

        public const string Phoneme = "phoneme";
    }
}
