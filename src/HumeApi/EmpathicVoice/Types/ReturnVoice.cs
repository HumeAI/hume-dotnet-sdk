using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

/// <summary>
/// A specific voice specification
/// </summary>
[Serializable]
public record ReturnVoice : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The provider of the voice to use. Supported values are `HUME_AI` and `CUSTOM_VOICE`.
    /// </summary>
    [JsonPropertyName("provider")]
    public required ReturnVoiceProvider Provider { get; set; }

    /// <summary>
    /// The name of the specified voice.
    ///
    /// This will either be the name of a previously created Custom Voice or one of our 8 base voices: `ITO`, `KORA`, `DACHER`, `AURA`, `FINN`, `WHIMSY`, `STELLA`, or `SUNNY`.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("custom_voice")]
    public ReturnCustomVoice? CustomVoice { get; set; }

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
