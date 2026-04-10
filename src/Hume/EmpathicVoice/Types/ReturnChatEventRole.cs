using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(ReturnChatEventRole.ReturnChatEventRoleSerializer))]
[Serializable]
public readonly record struct ReturnChatEventRole : IStringEnum
{
    public static readonly ReturnChatEventRole User = new(Values.User);

    public static readonly ReturnChatEventRole Agent = new(Values.Agent);

    public static readonly ReturnChatEventRole System = new(Values.System);

    public static readonly ReturnChatEventRole Tool = new(Values.Tool);

    public ReturnChatEventRole(string value)
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
    public static ReturnChatEventRole FromCustom(string value)
    {
        return new ReturnChatEventRole(value);
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

    public static bool operator ==(ReturnChatEventRole value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ReturnChatEventRole value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ReturnChatEventRole value) => value.Value;

    public static explicit operator ReturnChatEventRole(string value) => new(value);

    internal class ReturnChatEventRoleSerializer : JsonConverter<ReturnChatEventRole>
    {
        public override ReturnChatEventRole Read(
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
            return new ReturnChatEventRole(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ReturnChatEventRole value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ReturnChatEventRole ReadAsPropertyName(
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
            return new ReturnChatEventRole(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ReturnChatEventRole value,
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
        public const string User = "USER";

        public const string Agent = "AGENT";

        public const string System = "SYSTEM";

        public const string Tool = "TOOL";
    }
}
