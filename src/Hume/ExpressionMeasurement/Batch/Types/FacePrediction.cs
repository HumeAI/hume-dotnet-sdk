using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Batch;

[Serializable]
public record FacePrediction : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Frame number
    /// </summary>
    [JsonPropertyName("frame")]
    public required ulong Frame { get; set; }

    /// <summary>
    /// Time in seconds when face detection occurred.
    /// </summary>
    [JsonPropertyName("time")]
    public required double Time { get; set; }

    /// <summary>
    /// The predicted probability that a detected face was actually a face.
    /// </summary>
    [JsonPropertyName("prob")]
    public required double Prob { get; set; }

    [JsonPropertyName("box")]
    public required BoundingBox Box { get; set; }

    /// <summary>
    /// A high-dimensional embedding in emotion space.
    /// </summary>
    [JsonPropertyName("emotions")]
    public IEnumerable<EmotionScore> Emotions { get; set; } = new List<EmotionScore>();

    /// <summary>
    /// FACS 2.0 features and their scores.
    /// </summary>
    [JsonPropertyName("facs")]
    public IEnumerable<FacsScore>? Facs { get; set; }

    /// <summary>
    /// Modality-specific descriptive features and their scores.
    /// </summary>
    [JsonPropertyName("descriptions")]
    public IEnumerable<DescriptionsScore>? Descriptions { get; set; }

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
