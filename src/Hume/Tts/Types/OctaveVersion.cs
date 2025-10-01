using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.Tts;

[JsonConverter(typeof(StringEnumSerializer<OctaveVersion>))]
[Serializable]
public readonly record struct OctaveVersion : IStringEnum
{
    public static readonly OctaveVersion One = new(Values.One);

    public static readonly OctaveVersion Two = new(Values.Two);

    public OctaveVersion(string value)
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
    public static OctaveVersion FromCustom(string value)
    {
        return new OctaveVersion(value);
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

    public static bool operator ==(OctaveVersion value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(OctaveVersion value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(OctaveVersion value) => value.Value;

    public static explicit operator OctaveVersion(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string One = "1";

        public const string Two = "2";
    }
}
