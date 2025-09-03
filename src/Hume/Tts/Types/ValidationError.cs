using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;
using OneOf;

namespace Hume.Tts;

[Serializable]
public record ValidationError : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("loc")]
    public IEnumerable<OneOf<string, int>> Loc { get; set; } = new List<OneOf<string, int>>();

    [JsonPropertyName("msg")]
    public required string Msg { get; set; }

    [JsonPropertyName("type")]
    public required string Type { get; set; }

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
