using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(
    typeof(ReturnChatGroupPagedChatsPaginationDirection.ReturnChatGroupPagedChatsPaginationDirectionSerializer)
)]
[Serializable]
public readonly record struct ReturnChatGroupPagedChatsPaginationDirection : IStringEnum
{
    public static readonly ReturnChatGroupPagedChatsPaginationDirection Asc = new(Values.Asc);

    public static readonly ReturnChatGroupPagedChatsPaginationDirection Desc = new(Values.Desc);

    public ReturnChatGroupPagedChatsPaginationDirection(string value)
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
    public static ReturnChatGroupPagedChatsPaginationDirection FromCustom(string value)
    {
        return new ReturnChatGroupPagedChatsPaginationDirection(value);
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
        ReturnChatGroupPagedChatsPaginationDirection value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ReturnChatGroupPagedChatsPaginationDirection value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(ReturnChatGroupPagedChatsPaginationDirection value) =>
        value.Value;

    public static explicit operator ReturnChatGroupPagedChatsPaginationDirection(string value) =>
        new(value);

    internal class ReturnChatGroupPagedChatsPaginationDirectionSerializer
        : JsonConverter<ReturnChatGroupPagedChatsPaginationDirection>
    {
        public override ReturnChatGroupPagedChatsPaginationDirection Read(
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
            return new ReturnChatGroupPagedChatsPaginationDirection(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ReturnChatGroupPagedChatsPaginationDirection value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ReturnChatGroupPagedChatsPaginationDirection ReadAsPropertyName(
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
            return new ReturnChatGroupPagedChatsPaginationDirection(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ReturnChatGroupPagedChatsPaginationDirection value,
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
