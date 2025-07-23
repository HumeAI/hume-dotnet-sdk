using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Batch;

[Serializable]
public record InferenceResults : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("predictions")]
    public IEnumerable<InferencePrediction> Predictions { get; set; } =
        new List<InferencePrediction>();

    [JsonPropertyName("errors")]
    public IEnumerable<Error> Errors { get; set; } = new List<Error>();

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
