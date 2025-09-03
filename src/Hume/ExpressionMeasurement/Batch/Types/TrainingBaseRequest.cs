using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;
using OneOf;

namespace Hume.ExpressionMeasurement.Batch;

[Serializable]
public record TrainingBaseRequest : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("custom_model")]
    public required CustomModelRequest CustomModel { get; set; }

    [JsonPropertyName("dataset")]
    public required OneOf<DatasetId, DatasetVersionId> Dataset { get; set; }

    [JsonPropertyName("target_feature")]
    public string? TargetFeature { get; set; }

    [JsonPropertyName("task")]
    public Task? Task { get; set; }

    [JsonPropertyName("evaluation")]
    public EvaluationArgs? Evaluation { get; set; }

    [JsonPropertyName("alternatives")]
    public IEnumerable<string>? Alternatives { get; set; }

    [JsonPropertyName("callback_url")]
    public string? CallbackUrl { get; set; }

    [JsonPropertyName("notify")]
    public bool? Notify { get; set; }

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
