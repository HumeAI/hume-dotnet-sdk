using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Batch;

[Serializable]
public record JobInference : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The ID associated with this job.
    /// </summary>
    [JsonPropertyName("job_id")]
    public required string JobId { get; set; }

    /// <summary>
    /// The request that initiated the job.
    /// </summary>
    [JsonPropertyName("request")]
    public required InferenceRequest Request { get; set; }

    /// <summary>
    /// The current state of the job.
    /// </summary>
    [JsonPropertyName("state")]
    public required StateInference State { get; set; }

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
