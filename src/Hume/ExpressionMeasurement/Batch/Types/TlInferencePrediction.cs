using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Batch;

[Serializable]
public record TlInferencePrediction : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// A file path relative to the top level source URL or file.
    /// </summary>
    [JsonPropertyName("file")]
    public required string File { get; set; }

    [JsonPropertyName("file_type")]
    public required string FileType { get; set; }

    [JsonPropertyName("custom_models")]
    public Dictionary<string, CustomModelPrediction> CustomModels { get; set; } =
        new Dictionary<string, CustomModelPrediction>();

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
