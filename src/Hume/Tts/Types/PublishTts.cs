using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;
using OneOf;

namespace Hume.Tts;

/// <summary>
/// Input message type for the TTS stream.
/// </summary>
[Serializable]
public record PublishTts : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Force the generation of audio and close the stream.
    /// </summary>
    [JsonPropertyName("close")]
    public bool? Close { get; set; }

    /// <summary>
    /// Natural language instructions describing how the text should be spoken by the model (e.g., `"a soft, gentle voice with a strong British accent"`).
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Force the generation of audio regardless of how much text has been supplied.
    /// </summary>
    [JsonPropertyName("flush")]
    public bool? Flush { get; set; }

    /// <summary>
    /// A relative measure of how fast this utterance should be spoken.
    /// </summary>
    [JsonPropertyName("speed")]
    public double? Speed { get; set; }

    /// <summary>
    /// The input text to be converted to speech output.
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    /// <summary>
    /// Duration of trailing silence (in seconds) to add to this utterance
    /// </summary>
    [JsonPropertyName("trailing_silence")]
    public double? TrailingSilence { get; set; }

    /// <summary>
    /// The name or ID of the voice from the `Voice Library` to be used as the speaker for this and all subsequent utterances, until the `"voice"` field is updated again.
    /// </summary>
    [JsonPropertyName("voice")]
    public OneOf<PostedUtteranceVoiceWithId, PostedUtteranceVoiceWithName>? Voice { get; set; }

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
