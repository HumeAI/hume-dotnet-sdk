using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.Tts;

[Serializable]
public record MillisecondInterval : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Start time of the interval in milliseconds.
    /// </summary>
    [JsonPropertyName("begin")]
    public required int Begin { get; set; }

    /// <summary>
    /// End time of the interval in milliseconds.
    /// </summary>
    [JsonPropertyName("end")]
    public required int End { get; set; }

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
