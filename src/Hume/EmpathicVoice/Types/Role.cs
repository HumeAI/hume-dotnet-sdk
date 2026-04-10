using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(Role.RoleSerializer))]
[Serializable]
public readonly record struct Role : IStringEnum
{
    public static readonly Role Assistant = new(Values.Assistant);

    public static readonly Role System = new(Values.System);

    public static readonly Role User = new(Values.User);

    public static readonly Role All = new(Values.All);

    public static readonly Role Tool = new(Values.Tool);

    public static readonly Role Context = new(Values.Context);

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

    internal class RoleSerializer : JsonConverter<Role>
    {
        public override Role Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON value could not be read as a string."
                );
            return new Role(stringValue);
        }

        public override void Write(Utf8JsonWriter writer, Role value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }

        public override Role ReadAsPropertyName(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON property name could not be read as a string."
                );
            return new Role(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            Role value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Value);
        }
    }

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

        public const string Context = "context";
    }
}
