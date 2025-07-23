using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;
using OneOf;

namespace HumeApi.ExpressionMeasurement.Batch;

[Serializable]
public record ValidationArgs : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("positive_label")]
    public OneOf<long, double, string>? PositiveLabel { get; set; }

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
