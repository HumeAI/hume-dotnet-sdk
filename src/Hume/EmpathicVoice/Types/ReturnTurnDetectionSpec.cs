using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// A turn detection configuration returned from the server
/// </summary>
[Serializable]
public record ReturnTurnDetectionSpec : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// How long the user must be silent before EVI considers their turn complete and begins generating a response. Lower values make conversations feel snappier but increase the chance of the agent responding during a mid-thought pause. Higher values give users more room to pause and collect their thoughts before the agent responds. Accepts values between 500 and 3000 milliseconds.
    /// </summary>
    [JsonPropertyName("end_of_turn_silence_ms")]
    public int? EndOfTurnSilenceMs { get; set; }

    /// <summary>
    /// The duration of audio captured before the detected start of speech. This ensures the beginning of an utterance is not clipped. Accepts values between 0 and 1000 milliseconds.
    /// </summary>
    [JsonPropertyName("prefix_padding_ms")]
    public int? PrefixPaddingMs { get; set; }

    /// <summary>
    /// How confident the system must be that audio contains speech before it begins processing. Lower values increase sensitivity, capturing softer speech at the cost of more noise-triggered processing. Higher values require clearer, louder audio to register as speech. Accepts values between 0.0 and 1.0.
    /// </summary>
    [JsonPropertyName("speech_detection_threshold")]
    public float? SpeechDetectionThreshold { get; set; }

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
