using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(
    typeof(ReturnChatAudioReconstructionStatus.ReturnChatAudioReconstructionStatusSerializer)
)]
[Serializable]
public readonly record struct ReturnChatAudioReconstructionStatus : IStringEnum
{
    public static readonly ReturnChatAudioReconstructionStatus Queued = new(Values.Queued);

    public static readonly ReturnChatAudioReconstructionStatus InProgress = new(Values.InProgress);

    public static readonly ReturnChatAudioReconstructionStatus Complete = new(Values.Complete);

    public static readonly ReturnChatAudioReconstructionStatus Error = new(Values.Error);

    public static readonly ReturnChatAudioReconstructionStatus Cancelled = new(Values.Cancelled);

    public ReturnChatAudioReconstructionStatus(string value)
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
    public static ReturnChatAudioReconstructionStatus FromCustom(string value)
    {
        return new ReturnChatAudioReconstructionStatus(value);
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

    public static bool operator ==(ReturnChatAudioReconstructionStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ReturnChatAudioReconstructionStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ReturnChatAudioReconstructionStatus value) =>
        value.Value;

    public static explicit operator ReturnChatAudioReconstructionStatus(string value) => new(value);

    internal class ReturnChatAudioReconstructionStatusSerializer
        : JsonConverter<ReturnChatAudioReconstructionStatus>
    {
        public override ReturnChatAudioReconstructionStatus Read(
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
            return new ReturnChatAudioReconstructionStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ReturnChatAudioReconstructionStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ReturnChatAudioReconstructionStatus ReadAsPropertyName(
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
            return new ReturnChatAudioReconstructionStatus(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ReturnChatAudioReconstructionStatus value,
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
        public const string Queued = "QUEUED";

        public const string InProgress = "IN_PROGRESS";

        public const string Complete = "COMPLETE";

        public const string Error = "ERROR";

        public const string Cancelled = "CANCELLED";
    }
}
