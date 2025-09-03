using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Batch;

/// <summary>
/// The models used for inference.
/// </summary>
[Serializable]
public record Models : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("face")]
    public Face? Face { get; set; }

    [JsonPropertyName("burst")]
    public Dictionary<string, object?>? Burst { get; set; }

    [JsonPropertyName("prosody")]
    public Prosody? Prosody { get; set; }

    [JsonPropertyName("language")]
    public Language? Language { get; set; }

    [JsonPropertyName("ner")]
    public Ner? Ner { get; set; }

    [JsonPropertyName("facemesh")]
    public Dictionary<string, object?>? Facemesh { get; set; }

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
