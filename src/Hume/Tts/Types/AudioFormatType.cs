using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.Tts;

[JsonConverter(typeof(StringEnumSerializer<AudioFormatType>))]
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
