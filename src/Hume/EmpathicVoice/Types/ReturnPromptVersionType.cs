using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(StringEnumSerializer<ReturnPromptVersionType>))]
[Serializable]
public readonly record struct ReturnPromptVersionType : IStringEnum
{
    public static readonly ReturnPromptVersionType Fixed = new(Values.Fixed);

    public static readonly ReturnPromptVersionType Latest = new(Values.Latest);

    public ReturnPromptVersionType(string value)
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
    public static ReturnPromptVersionType FromCustom(string value)
    {
        return new ReturnPromptVersionType(value);
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

    public static bool operator ==(ReturnPromptVersionType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ReturnPromptVersionType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ReturnPromptVersionType value) => value.Value;

    public static explicit operator ReturnPromptVersionType(string value) => new(value);

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
