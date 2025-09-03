using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Batch;

[Serializable]
public record ProsodyPrediction : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// A segment of text (like a word or a sentence).
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("time")]
    public required TimeInterval Time { get; set; }

    /// <summary>
    /// Value between `0.0` and `1.0` that indicates our transcription model's relative confidence in this text.
    /// </summary>
    [JsonPropertyName("confidence")]
    public double? Confidence { get; set; }

    /// <summary>
    /// Value between `0.0` and `1.0` that indicates our transcription model's relative confidence that this text was spoken by this speaker.
    /// </summary>
    [JsonPropertyName("speaker_confidence")]
    public double? SpeakerConfidence { get; set; }

    /// <summary>
    /// A high-dimensional embedding in emotion space.
    /// </summary>
    [JsonPropertyName("emotions")]
    public IEnumerable<EmotionScore> Emotions { get; set; } = new List<EmotionScore>();

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
