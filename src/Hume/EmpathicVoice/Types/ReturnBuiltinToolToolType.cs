using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(StringEnumSerializer<ReturnBuiltinToolToolType>))]
[Serializable]
public readonly record struct ReturnBuiltinToolToolType : IStringEnum
{
    public static readonly ReturnBuiltinToolToolType Builtin = new(Values.Builtin);

    public static readonly ReturnBuiltinToolToolType Function = new(Values.Function);

    public ReturnBuiltinToolToolType(string value)
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
    public static ReturnBuiltinToolToolType FromCustom(string value)
    {
        return new ReturnBuiltinToolToolType(value);
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

    public static bool operator ==(ReturnBuiltinToolToolType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ReturnBuiltinToolToolType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ReturnBuiltinToolToolType value) => value.Value;

    public static explicit operator ReturnBuiltinToolToolType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Builtin = "BUILTIN";

        public const string Function = "FUNCTION";
    }
}
