using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Batch;

/// <summary>
/// Generate predictions based on time.
///
/// Setting the `window` field allows for a 'sliding window' approach, where a fixed-size window moves across the audio or video file in defined steps. This enables continuous analysis of prosody within subsets of the file, providing dynamic and localized insights into emotional expression.
/// </summary>
[Serializable]
public record Window : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The length of the sliding window.
    /// </summary>
    [JsonPropertyName("length")]
    public double? Length { get; set; }

    /// <summary>
    /// The step size of the sliding window.
    /// </summary>
    [JsonPropertyName("step")]
    public double? Step { get; set; }

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
