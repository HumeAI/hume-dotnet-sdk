using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.Tts;

[JsonConverter(typeof(AudioFormatType.AudioFormatTypeSerializer))]
[Serializable]
public readonly record struct AudioFormatType : IStringEnum
{
    public static readonly AudioFormatType Mp3 = new(Values.Mp3);

    public static readonly AudioFormatType Pcm = new(Values.Pcm);

    public static readonly AudioFormatType Wav = new(Values.Wav);

    public AudioFormatType(string value)
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
    public static AudioFormatType FromCustom(string value)
    {
        return new AudioFormatType(value);
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

    public static bool operator ==(AudioFormatType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(AudioFormatType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(AudioFormatType value) => value.Value;

    public static explicit operator AudioFormatType(string value) => new(value);

    internal class AudioFormatTypeSerializer : JsonConverter<AudioFormatType>
    {
        public override AudioFormatType Read(
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
            return new AudioFormatType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            AudioFormatType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override AudioFormatType ReadAsPropertyName(
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
            return new AudioFormatType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            AudioFormatType value,
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
        public const string Mp3 = "mp3";

        public const string Pcm = "pcm";

        public const string Wav = "wav";
    }
}
