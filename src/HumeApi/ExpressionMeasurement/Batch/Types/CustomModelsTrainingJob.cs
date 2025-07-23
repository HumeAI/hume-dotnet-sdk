using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Batch;

[Serializable]
public record CustomModelsTrainingJob : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("type")]
    public required string Type { get; set; }

    /// <summary>
    /// The ID associated with this job.
    /// </summary>
    [JsonPropertyName("job_id")]
    public required string JobId { get; set; }

    [JsonPropertyName("user_id")]
    public required string UserId { get; set; }

    [JsonPropertyName("request")]
    public required TrainingBaseRequest Request { get; set; }

    [JsonPropertyName("state")]
    public required StateTraining State { get; set; }

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
