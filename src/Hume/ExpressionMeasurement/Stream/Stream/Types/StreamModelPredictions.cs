using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Stream;

/// <summary>
/// Model predictions
/// </summary>
[Serializable]
public record StreamModelPredictions : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// If a payload ID was passed in the request, the same payload ID will be sent back in the response body.
    /// </summary>
    [JsonPropertyName("payload_id")]
    public string? PayloadId { get; set; }

    /// <summary>
    /// If the job_details flag was set in the request, details about the current streaming job will be returned in the response body.
    /// </summary>
    [JsonPropertyName("job_details")]
    public StreamModelPredictionsJobDetails? JobDetails { get; set; }

    /// <summary>
    /// Response for the vocal burst emotion model.
    /// </summary>
    [JsonPropertyName("burst")]
    public StreamModelPredictionsBurst? Burst { get; set; }

    /// <summary>
    /// Response for the facial expression emotion model.
    /// </summary>
    [JsonPropertyName("face")]
    public StreamModelPredictionsFace? Face { get; set; }

    /// <summary>
    /// Response for the facemesh emotion model.
    /// </summary>
    [JsonPropertyName("facemesh")]
    public StreamModelPredictionsFacemesh? Facemesh { get; set; }

    /// <summary>
    /// Response for the language emotion model.
    /// </summary>
    [JsonPropertyName("language")]
    public StreamModelPredictionsLanguage? Language { get; set; }

    /// <summary>
    /// Response for the speech prosody emotion model.
    /// </summary>
    [JsonPropertyName("prosody")]
    public StreamModelPredictionsProsody? Prosody { get; set; }

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
