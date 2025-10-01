using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.Tts;

[Serializable]
public record ReturnTts : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("generations")]
    public IEnumerable<ReturnGeneration> Generations { get; set; } = new List<ReturnGeneration>();

    /// <summary>
    /// A unique ID associated with this request for tracking and troubleshooting. Use this ID when contacting [support](/support) for troubleshooting assistance.
    /// </summary>
    [JsonPropertyName("request_id")]
    public string? RequestId { get; set; }

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
