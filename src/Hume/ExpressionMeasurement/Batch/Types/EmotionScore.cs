using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Batch;

[Serializable]
public record EmotionScore : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Name of the emotion being expressed.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Embedding value for the emotion being expressed.
    /// </summary>
    [JsonPropertyName("score")]
    public required float Score { get; set; }

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
