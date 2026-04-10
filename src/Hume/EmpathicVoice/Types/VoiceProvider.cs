using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(VoiceProvider.VoiceProviderSerializer))]
[Serializable]
public readonly record struct VoiceProvider : IStringEnum
{
    public static readonly VoiceProvider HumeAi = new(Values.HumeAi);

    public static readonly VoiceProvider CustomVoice = new(Values.CustomVoice);

    public VoiceProvider(string value)
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
    public static VoiceProvider FromCustom(string value)
    {
        return new VoiceProvider(value);
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

    public static bool operator ==(VoiceProvider value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(VoiceProvider value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(VoiceProvider value) => value.Value;

    public static explicit operator VoiceProvider(string value) => new(value);

    internal class VoiceProviderSerializer : JsonConverter<VoiceProvider>
    {
        public override VoiceProvider Read(
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
            return new VoiceProvider(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            VoiceProvider value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override VoiceProvider ReadAsPropertyName(
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
            return new VoiceProvider(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            VoiceProvider value,
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
        public const string HumeAi = "HUME_AI";

        public const string CustomVoice = "CUSTOM_VOICE";
    }
}
