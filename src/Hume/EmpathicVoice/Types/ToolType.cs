using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(StringEnumSerializer<ToolType>))]
[Serializable]
public readonly record struct ToolType : IStringEnum
{
    public static readonly ToolType Builtin = new(Values.Builtin);

    public static readonly ToolType Function = new(Values.Function);

    public ToolType(string value)
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
    public static ToolType FromCustom(string value)
    {
        return new ToolType(value);
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

    public static bool operator ==(ToolType value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(ToolType value1, string value2) => !value1.Value.Equals(value2);

    public static explicit operator string(ToolType value) => value.Value;

    public static explicit operator ToolType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Builtin = "builtin";

        public const string Function = "function";
    }
}
