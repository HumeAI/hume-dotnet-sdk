using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(StringEnumSerializer<ReturnUserDefinedToolToolType>))]
[Serializable]
public readonly record struct ReturnUserDefinedToolToolType : IStringEnum
{
    public static readonly ReturnUserDefinedToolToolType Builtin = new(Values.Builtin);

    public static readonly ReturnUserDefinedToolToolType Function = new(Values.Function);

    public ReturnUserDefinedToolToolType(string value)
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
    public static ReturnUserDefinedToolToolType FromCustom(string value)
    {
        return new ReturnUserDefinedToolToolType(value);
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

    public static bool operator ==(ReturnUserDefinedToolToolType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ReturnUserDefinedToolToolType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ReturnUserDefinedToolToolType value) => value.Value;

    public static explicit operator ReturnUserDefinedToolToolType(string value) => new(value);

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
