using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Stream;

[Serializable]
public record StreamModelPredictionsFacePredictionsItem : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Frame number
    /// </summary>
    [JsonPropertyName("frame")]
    public double? Frame { get; set; }

    /// <summary>
    /// Time in seconds when face detection occurred.
    /// </summary>
    [JsonPropertyName("time")]
    public double? Time { get; set; }

    [JsonPropertyName("bbox")]
    public StreamBoundingBox? Bbox { get; set; }

    /// <summary>
    /// The predicted probability that a detected face was actually a face.
    /// </summary>
    [JsonPropertyName("prob")]
    public double? Prob { get; set; }

    /// <summary>
    /// Identifier for a face. Not that this defaults to `unknown` unless face identification is enabled in the face model configuration.
    /// </summary>
    [JsonPropertyName("face_id")]
    public string? FaceId { get; set; }

    [JsonPropertyName("emotions")]
    public IEnumerable<EmotionEmbeddingItem>? Emotions { get; set; }

    [JsonPropertyName("facs")]
    public IEnumerable<EmotionEmbeddingItem>? Facs { get; set; }

    [JsonPropertyName("descriptions")]
    public IEnumerable<EmotionEmbeddingItem>? Descriptions { get; set; }

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
