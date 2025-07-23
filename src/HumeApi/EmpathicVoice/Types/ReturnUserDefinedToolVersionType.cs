using System.Text.Json.Serialization;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

[JsonConverter(typeof(StringEnumSerializer<ReturnUserDefinedToolVersionType>))]
[Serializable]
public readonly record struct ReturnUserDefinedToolVersionType : IStringEnum
{
    public static readonly ReturnUserDefinedToolVersionType Fixed = new(Values.Fixed);

    public static readonly ReturnUserDefinedToolVersionType Latest = new(Values.Latest);

    public ReturnUserDefinedToolVersionType(string value)
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
    public static ReturnUserDefinedToolVersionType FromCustom(string value)
    {
        return new ReturnUserDefinedToolVersionType(value);
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

    public static bool operator ==(ReturnUserDefinedToolVersionType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ReturnUserDefinedToolVersionType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ReturnUserDefinedToolVersionType value) => value.Value;

    public static explicit operator ReturnUserDefinedToolVersionType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Fixed = "FIXED";

        public const string Latest = "LATEST";
    }
}
