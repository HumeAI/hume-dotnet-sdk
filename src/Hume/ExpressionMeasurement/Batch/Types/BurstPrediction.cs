using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Batch;

[Serializable]
public record BurstPrediction : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("time")]
    public required TimeInterval Time { get; set; }

    /// <summary>
    /// A high-dimensional embedding in emotion space.
    /// </summary>
    [JsonPropertyName("emotions")]
    public IEnumerable<EmotionScore> Emotions { get; set; } = new List<EmotionScore>();

    /// <summary>
    /// Modality-specific descriptive features and their scores.
    /// </summary>
    [JsonPropertyName("descriptions")]
    public IEnumerable<DescriptionsScore> Descriptions { get; set; } =
        new List<DescriptionsScore>();

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
