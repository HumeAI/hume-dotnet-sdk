using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// When provided, the output is a user message.
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
    /// Indicates if this message was inserted into the conversation as text from a [User Input](/reference/speech-to-speech-evi/chat#send.UserInput.text) message.
    /// </summary>
    [JsonPropertyName("from_text")]
    public required bool FromText { get; set; }

    /// <summary>
    /// Indicates whether this `UserMessage` contains an interim (unfinalized) transcript.
    ///
    /// - `true`: the transcript is provisional; words may be repeated or refined in subsequent `UserMessage` responses as additional audio is processed.
    /// - `false`: the transcript is final and complete.
    ///
    /// Interim transcripts are only sent when the [`verbose_transcription`](/reference/speech-to-speech-evi/chat#request.query.verbose_transcription) query parameter is set to `true` in the initial handshake.
    /// </summary>
    [JsonPropertyName("interim")]
    public required bool Interim { get; set; }

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

    /// <summary>
    /// The type of message sent through the socket; for a User Message, this must be `user_message`.
    ///
    /// This message contains both a transcript of the user's input and the expression measurement predictions if the input was sent as an [Audio Input message](/reference/speech-to-speech-evi/chat#send.AudioInput). Expression measurement predictions are not provided for a [User Input message](/reference/speech-to-speech-evi/chat#send.UserInput), as the prosody model relies on audio input and cannot process text alone.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = "user_message";

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
