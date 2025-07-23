using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Stream;

/// <summary>
/// A time range with a beginning and end, measured in seconds.
/// </summary>
[Serializable]
public record TimeRange : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Beginning of time range in seconds.
    /// </summary>
    [JsonPropertyName("begin")]
    public double? Begin { get; set; }

    /// <summary>
    /// End of time range in seconds.
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
