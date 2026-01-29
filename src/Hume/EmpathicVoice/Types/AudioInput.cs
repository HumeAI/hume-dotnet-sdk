using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// **Base64 encoded audio input to insert into the conversation.** The content is treated as the user's speech to EVI and must be streamed continuously. Pre-recorded audio files are not supported.
///
/// For optimal transcription quality, the audio data should be transmitted in small chunks. Hume recommends streaming audio with a buffer window of `20` milliseconds (ms), or `100` milliseconds (ms) for web applications. See our [Audio Guide](/docs/speech-to-speech-evi/guides/audio) for more details on preparing and processing audio.
/// </summary>
[Serializable]
public record AudioInput : IJsonOnDeserialized
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
    /// Base64 encoded audio input to insert into the conversation.
    ///
    /// The content of an Audio Input message is treated as the user's speech to EVI and must be streamed continuously. Pre-recorded audio files are not supported.
    ///
    /// For optimal transcription quality, the audio data should be transmitted in small chunks.
    ///
    /// Hume recommends streaming audio with a buffer window of 20 milliseconds (ms), or 100 milliseconds (ms) for web applications.
    /// </summary>
    [JsonPropertyName("data")]
    public required string Data { get; set; }

    /// <summary>
    /// The type of message sent through the socket; must be `audio_input` for our server to correctly identify and process it as an Audio Input message.
    ///
    /// This message is used for sending audio input data to EVI for processing and expression measurement. Audio data should be sent as a continuous stream, encoded in Base64.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type
    {
        get => "audio_input";
        set =>
            value.Assert(
                value == "audio_input",
                string.Format("'Type' must be {0}", "audio_input")
            );
    }

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
