using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Stream;

/// <summary>
/// Error message
/// </summary>
[Serializable]
public record StreamErrorMessage : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Error message text.
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    /// <summary>
    /// Unique identifier for the error.
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    /// <summary>
    /// If a payload ID was passed in the request, the same payload ID will be sent back in the response body.
    /// </summary>
    [JsonPropertyName("payload_id")]
    public string? PayloadId { get; set; }

    /// <summary>
    /// If the job_details flag was set in the request, details about the current streaming job will be returned in the response body.
    /// </summary>
    [JsonPropertyName("job_details")]
    public JobDetails? JobDetails { get; set; }

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
