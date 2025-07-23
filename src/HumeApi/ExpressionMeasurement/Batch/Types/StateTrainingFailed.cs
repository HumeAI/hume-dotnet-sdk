using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Batch;

[Serializable]
public record StateTrainingFailed : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// When this job was created (Unix timestamp in milliseconds).
    /// </summary>
    [JsonPropertyName("created_timestamp_ms")]
    public required long CreatedTimestampMs { get; set; }

    /// <summary>
    /// When this job started (Unix timestamp in milliseconds).
    /// </summary>
    [JsonPropertyName("started_timestamp_ms")]
    public required long StartedTimestampMs { get; set; }

    /// <summary>
    /// When this job ended (Unix timestamp in milliseconds).
    /// </summary>
    [JsonPropertyName("ended_timestamp_ms")]
    public required long EndedTimestampMs { get; set; }

    /// <summary>
    /// An error message.
    /// </summary>
    [JsonPropertyName("message")]
    public required string Message { get; set; }

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
