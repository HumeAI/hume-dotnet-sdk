using System.Text.Json;
using System.Text.Json.Serialization;
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

    [JsonPropertyName("type")]
    public string Type
    {
        get => "audio_output";
        set =>
            value.Assert(
                value == "audio_output",
                string.Format("'Type' must be {0}", "audio_output")
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
