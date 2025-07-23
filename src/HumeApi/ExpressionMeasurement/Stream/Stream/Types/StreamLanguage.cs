using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Stream;

/// <summary>
/// Configuration for the language emotion model.
/// </summary>
[Serializable]
public record StreamLanguage : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Configuration for sentiment predictions. If missing or null, no sentiment predictions will be generated.
    /// </summary>
    [JsonPropertyName("sentiment")]
    public Dictionary<string, object?>? Sentiment { get; set; }

    /// <summary>
    /// Configuration for toxicity predictions. If missing or null, no toxicity predictions will be generated.
    /// </summary>
    [JsonPropertyName("toxicity")]
    public Dictionary<string, object?>? Toxicity { get; set; }

    /// <summary>
    /// The granularity at which to generate predictions. Values are `word`, `sentence`, `utterance`, or `passage`. To get a single prediction for the entire text of your streaming payload use `passage`. Default value is `word`.
    /// </summary>
    [JsonPropertyName("granularity")]
    public string? Granularity { get; set; }

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
