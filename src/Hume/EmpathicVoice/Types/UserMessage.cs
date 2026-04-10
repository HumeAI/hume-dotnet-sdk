using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// **Transcript of the user's message.** Contains the message role and content, along with a `from_text` field indicating if this message was inserted into the conversation as text from a `UserInput` message.
///
/// Includes an `interim` field indicating whether the transcript is provisional (words may be repeated or refined in subsequent `UserMessage` responses as additional audio is processed) or final and complete. Interim transcripts are only sent when the `verbose_transcription` query parameter is set to true in the initial handshake.
/// </summary>
[Serializable]
public record UserMessage : IJsonOnDeserialized
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
    /// Indicates if this message was inserted into the conversation as text from a [User Input](/reference/empathic-voice-interface-evi/chat/chat#send.User%20Input.text) message.
    /// </summary>
    [JsonPropertyName("from_text")]
    public required bool FromText { get; set; }

    /// <summary>
    /// Indicates if this message contains an immediate and unfinalized transcript of the user's audio input. If it does, words may be repeated across successive UserMessage messages as our transcription model becomes more confident about what was said with additional context. Interim messages are useful to detect if the user is interrupting during audio playback on the client. Even without a finalized transcription, along with `UserInterrupt` messages, interim `UserMessages` are useful for detecting if the user is interrupting during audio playback on the client, signaling to stop playback in your application.
    /// </summary>
    [JsonPropertyName("interim")]
    public required bool Interim { get; set; }

    /// <summary>
    /// Detected language of the message text.
    /// </summary>
    [JsonPropertyName("language")]
    public string? Language { get; set; }

    /// <summary>
    /// Transcript of the message.
    /// </summary>
    [JsonPropertyName("message")]
    public required ChatMessage Message { get; set; }

    /// <summary>
    /// Inference model results.
    /// </summary>
    [JsonPropertyName("models")]
    public required Inference Models { get; set; }

    /// <summary>
    /// Start and End time of user message.
    /// </summary>
    [JsonPropertyName("time")]
    public required MillisecondInterval Time { get; set; }

    [JsonRequired]
    [JsonPropertyName("type")]
    public UserMessage.TypeLiteral Type { get;
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
        public const string Value = "user_message";

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
