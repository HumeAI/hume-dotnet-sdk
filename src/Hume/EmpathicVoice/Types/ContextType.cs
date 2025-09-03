using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(StringEnumSerializer<ContextType>))]
[Serializable]
public readonly record struct ContextType : IStringEnum
{
    public static readonly ContextType Persistent = new(Values.Persistent);

    public static readonly ContextType Temporary = new(Values.Temporary);

    public ContextType(string value)
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
    public static ContextType FromCustom(string value)
    {
        return new ContextType(value);
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

    public static bool operator ==(ContextType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ContextType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ContextType value) => value.Value;

    public static explicit operator ContextType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Persistent = "persistent";

        public const string Temporary = "temporary";
    }
}
