using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// **Base64 encoded audio output.** This encoded audio is transmitted to the client, where it can be decoded and played back as part of the user interaction. The returned audio format is WAV and the sample rate is 48kHz.
///
/// Contains the audio data, an ID to track and reference the audio output, and an index indicating the chunk position relative to the whole audio segment. See our [Audio Guide](/docs/speech-to-speech-evi/guides/audio) for more details on preparing and processing audio.
/// </summary>
[Serializable]
public record AudioOutput : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Used to manage conversational state, correlate frontend and backend data, and persist conversations across EVI sessions.
    /// </summary>
    [JsonPropertyName("custom_session_id")]
    public string? CustomSessionId { get; set; }

    /// <summary>
    /// Base64 encoded audio output. This encoded audio is transmitted to the client, where it can be decoded and played back as part of the user interaction.
    /// </summary>
    [JsonPropertyName("data")]
    public required string Data { get; set; }

    /// <summary>
    /// ID of the audio output. Allows the Audio Output message to be tracked and referenced.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// Index of the chunk of audio relative to the whole audio segment.
    /// </summary>
    [JsonPropertyName("index")]
    public required int Index { get; set; }

    [JsonRequired]
    [JsonPropertyName("type")]
    public AudioOutput.TypeLiteral Type { get;
#if NET5_0_OR_GREATER
        init;
#else
        set;
#endif
    } = new();

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }

    [JsonConverter(typeof(TypeLiteralConverter))]
    public readonly struct TypeLiteral
    {
        public const string Value = "audio_output";

        public static implicit operator string(TypeLiteral _) => Value;

        public override string ToString() => Value;

        public override int GetHashCode() =>
            global::System.StringComparer.Ordinal.GetHashCode(Value);

        public override bool Equals(object? obj) => obj is TypeLiteral;

        public static bool operator ==(TypeLiteral _, TypeLiteral __) => true;

        public static bool operator !=(TypeLiteral _, TypeLiteral __) => false;

        internal sealed class TypeLiteralConverter : JsonConverter<TypeLiteral>
        {
            public override TypeLiteral Read(
                ref Utf8JsonReader reader,
                global::System.Type typeToConvert,
                JsonSerializerOptions options
            )
            {
                var value = reader.GetString();
                if (value != TypeLiteral.Value)
                {
                    throw new JsonException(
                        "Expected \""
                            + TypeLiteral.Value
                            + "\" for type discriminator but got \""
                            + value
                            + "\"."
                    );
                }
                return new TypeLiteral();
            }

            public override void Write(
                Utf8JsonWriter writer,
                TypeLiteral value,
                JsonSerializerOptions options
            ) => writer.WriteStringValue(TypeLiteral.Value);

            public override TypeLiteral ReadAsPropertyName(
                ref Utf8JsonReader reader,
                global::System.Type typeToConvert,
                JsonSerializerOptions options
            )
            {
                var value = reader.GetString();
                if (value != TypeLiteral.Value)
                {
                    throw new JsonException(
                        "Expected \""
                            + TypeLiteral.Value
                            + "\" for type discriminator but got \""
                            + value
                            + "\"."
                    );
                }
                return new TypeLiteral();
            }

            public override void WriteAsPropertyName(
                Utf8JsonWriter writer,
                TypeLiteral value,
                JsonSerializerOptions options
            ) => writer.WritePropertyName(TypeLiteral.Value);
        }
    }
}
