using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Batch;

[Serializable]
public record NerPrediction : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The recognized topic or entity.
    /// </summary>
    [JsonPropertyName("entity")]
    public required string Entity { get; set; }

    [JsonPropertyName("position")]
    public required PositionInterval Position { get; set; }

    /// <summary>
    /// Our NER model's relative confidence in the recognized topic or entity.
    /// </summary>
    [JsonPropertyName("entity_confidence")]
    public required double EntityConfidence { get; set; }

    /// <summary>
    /// A measure of how often the entity is linked to by other entities.
    /// </summary>
    [JsonPropertyName("support")]
    public required double Support { get; set; }

    /// <summary>
    /// A URL which provides more information about the recognized topic or entity.
    /// </summary>
    [JsonPropertyName("uri")]
    public required string Uri { get; set; }

    /// <summary>
    /// The specific word to which the emotion predictions are linked.
    /// </summary>
    [JsonPropertyName("link_word")]
    public required string LinkWord { get; set; }

    [JsonPropertyName("time")]
    public TimeInterval? Time { get; set; }

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
