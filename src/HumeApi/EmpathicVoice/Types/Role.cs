using System.Text.Json.Serialization;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

[JsonConverter(typeof(StringEnumSerializer<Role>))]
[Serializable]
public readonly record struct Role : IStringEnum
{
    public static readonly Role Assistant = new(Values.Assistant);

    public static readonly Role System = new(Values.System);

    public static readonly Role User = new(Values.User);

    public static readonly Role All = new(Values.All);

    public static readonly Role Tool = new(Values.Tool);

    public Role(string value)
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
    public static Role FromCustom(string value)
    {
        return new Role(value);
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

    public static bool operator ==(Role value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(Role value1, string value2) => !value1.Value.Equals(value2);

    public static explicit operator string(Role value) => value.Value;

    public static explicit operator Role(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Assistant = "assistant";

        public const string System = "system";

        public const string User = "user";

        public const string All = "all";

        public const string Tool = "tool";
    }
}
