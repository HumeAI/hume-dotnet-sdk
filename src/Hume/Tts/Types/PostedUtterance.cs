using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;
using OneOf;

namespace Hume.Tts;

[Serializable]
public record PostedUtterance : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Natural language instructions describing how the synthesized speech should sound, including but not limited to tone, intonation, pacing, and accent.
    ///
    /// **This field behaves differently depending on whether a voice is specified**:
    /// - **Voice specified**: the description will serve as acting directions for delivery. Keep directions concise—100 characters or fewer—for best results. See our guide on [acting instructions](/docs/text-to-speech-tts/acting-instructions).
    /// - **Voice not specified**: the description will serve as a voice prompt for generating a voice. See our [prompting guide](/docs/text-to-speech-tts/prompting) for design tips.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Speed multiplier for the synthesized speech. Extreme values below 0.75 and above 1.5 may sometimes cause instability to the generated output.
    /// </summary>
    [JsonPropertyName("speed")]
    public double? Speed { get; set; }

    /// <summary>
    /// The input text to be synthesized into speech.
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <summary>
    /// Duration of trailing silence (in seconds) to add to this utterance
    /// </summary>
    [JsonPropertyName("trailing_silence")]
    public double? TrailingSilence { get; set; }

    /// <summary>
    /// The `name` or `id` associated with a **Voice** from the **Voice Library** to be used as the speaker for this and all subsequent `utterances`, until the `voice` field is updated again.
    ///
    ///  See our [voices guide](/docs/text-to-speech-tts/voices) for more details on generating and specifying **Voices**.
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
