using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Batch;

[Serializable]
public record ModelsPredictions : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("face")]
    public PredictionsOptionalNullFacePrediction? Face { get; set; }

    [JsonPropertyName("burst")]
    public PredictionsOptionalNullBurstPrediction? Burst { get; set; }

    [JsonPropertyName("prosody")]
    public PredictionsOptionalTranscriptionMetadataProsodyPrediction? Prosody { get; set; }

    [JsonPropertyName("language")]
    public PredictionsOptionalTranscriptionMetadataLanguagePrediction? Language { get; set; }

    [JsonPropertyName("ner")]
    public PredictionsOptionalTranscriptionMetadataNerPrediction? Ner { get; set; }

    [JsonPropertyName("facemesh")]
    public PredictionsOptionalNullFacemeshPrediction? Facemesh { get; set; }

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
