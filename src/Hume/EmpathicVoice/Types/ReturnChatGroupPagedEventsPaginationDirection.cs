using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(
    typeof(ReturnChatGroupPagedEventsPaginationDirection.ReturnChatGroupPagedEventsPaginationDirectionSerializer)
)]
[Serializable]
public readonly record struct ReturnChatGroupPagedEventsPaginationDirection : IStringEnum
{
    public static readonly ReturnChatGroupPagedEventsPaginationDirection Asc = new(Values.Asc);

    public static readonly ReturnChatGroupPagedEventsPaginationDirection Desc = new(Values.Desc);

    public ReturnChatGroupPagedEventsPaginationDirection(string value)
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
    public static ReturnChatGroupPagedEventsPaginationDirection FromCustom(string value)
    {
        return new ReturnChatGroupPagedEventsPaginationDirection(value);
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

    public static bool operator ==(
        ReturnChatGroupPagedEventsPaginationDirection value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ReturnChatGroupPagedEventsPaginationDirection value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(ReturnChatGroupPagedEventsPaginationDirection value) =>
        value.Value;

    public static explicit operator ReturnChatGroupPagedEventsPaginationDirection(string value) =>
        new(value);

    internal class ReturnChatGroupPagedEventsPaginationDirectionSerializer
        : JsonConverter<ReturnChatGroupPagedEventsPaginationDirection>
    {
        public override ReturnChatGroupPagedEventsPaginationDirection Read(
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
            return new ReturnChatGroupPagedEventsPaginationDirection(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ReturnChatGroupPagedEventsPaginationDirection value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ReturnChatGroupPagedEventsPaginationDirection ReadAsPropertyName(
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
            return new ReturnChatGroupPagedEventsPaginationDirection(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ReturnChatGroupPagedEventsPaginationDirection value,
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
        public const string Asc = "ASC";

        public const string Desc = "DESC";
    }
}
