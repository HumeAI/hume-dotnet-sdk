using System.Text.Json.Serialization;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

[JsonConverter(typeof(StringEnumSerializer<BuiltInTool>))]
[Serializable]
public readonly record struct BuiltInTool : IStringEnum
{
    public static readonly BuiltInTool WebSearch = new(Values.WebSearch);

    public static readonly BuiltInTool HangUp = new(Values.HangUp);

    public BuiltInTool(string value)
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
    public static BuiltInTool FromCustom(string value)
    {
        return new BuiltInTool(value);
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

    public static bool operator ==(BuiltInTool value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BuiltInTool value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BuiltInTool value) => value.Value;

    public static explicit operator BuiltInTool(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string WebSearch = "web_search";

        public const string HangUp = "hang_up";
    }
}
