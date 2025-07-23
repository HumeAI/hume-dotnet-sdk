using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Stream;

/// <summary>
/// Position of a segment of text within a larger document, measured in characters. Uses zero-based indexing. The beginning index is inclusive and the end index is exclusive.
/// </summary>
[Serializable]
public record TextPosition : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The index of the first character in the text segment, inclusive.
    /// </summary>
    [JsonPropertyName("begin")]
    public double? Begin { get; set; }

    /// <summary>
    /// The index of the last character in the text segment, exclusive.
    /// </summary>
    [JsonPropertyName("end")]
    public double? End { get; set; }

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
