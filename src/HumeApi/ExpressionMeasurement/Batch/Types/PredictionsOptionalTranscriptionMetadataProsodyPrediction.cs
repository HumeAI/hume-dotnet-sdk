using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Batch;

[Serializable]
public record PredictionsOptionalTranscriptionMetadataProsodyPrediction : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("metadata")]
    public TranscriptionMetadata? Metadata { get; set; }

    [JsonPropertyName("grouped_predictions")]
    public IEnumerable<GroupedPredictionsProsodyPrediction> GroupedPredictions { get; set; } =
        new List<GroupedPredictionsProsodyPrediction>();

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
