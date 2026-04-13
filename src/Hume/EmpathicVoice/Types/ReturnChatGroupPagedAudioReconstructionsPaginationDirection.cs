using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(
    typeof(ReturnChatGroupPagedAudioReconstructionsPaginationDirection.ReturnChatGroupPagedAudioReconstructionsPaginationDirectionSerializer)
)]
[Serializable]
public readonly record struct ReturnChatGroupPagedAudioReconstructionsPaginationDirection
    : IStringEnum
{
    public static readonly ReturnChatGroupPagedAudioReconstructionsPaginationDirection Asc = new(
        Values.Asc
    );

    public static readonly ReturnChatGroupPagedAudioReconstructionsPaginationDirection Desc = new(
        Values.Desc
    );

    public ReturnChatGroupPagedAudioReconstructionsPaginationDirection(string value)
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
    public static ReturnChatGroupPagedAudioReconstructionsPaginationDirection FromCustom(
        string value
    )
    {
        return new ReturnChatGroupPagedAudioReconstructionsPaginationDirection(value);
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
        ReturnChatGroupPagedAudioReconstructionsPaginationDirection value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ReturnChatGroupPagedAudioReconstructionsPaginationDirection value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        ReturnChatGroupPagedAudioReconstructionsPaginationDirection value
    ) => value.Value;

    public static explicit operator ReturnChatGroupPagedAudioReconstructionsPaginationDirection(
        string value
    ) => new(value);

    internal class ReturnChatGroupPagedAudioReconstructionsPaginationDirectionSerializer
        : JsonConverter<ReturnChatGroupPagedAudioReconstructionsPaginationDirection>
    {
        public override ReturnChatGroupPagedAudioReconstructionsPaginationDirection Read(
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
            return new ReturnChatGroupPagedAudioReconstructionsPaginationDirection(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ReturnChatGroupPagedAudioReconstructionsPaginationDirection value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ReturnChatGroupPagedAudioReconstructionsPaginationDirection ReadAsPropertyName(
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
            return new ReturnChatGroupPagedAudioReconstructionsPaginationDirection(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ReturnChatGroupPagedAudioReconstructionsPaginationDirection value,
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
