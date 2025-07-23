using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

/// <summary>
/// A Voice specification posted to the server
/// </summary>
[Serializable]
public record PostedVoice : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The provider of the voice to use. Supported values are `HUME_AI` and `CUSTOM_VOICE`.
    /// </summary>
    [JsonPropertyName("provider")]
    public required PostedVoiceProvider Provider { get; set; }

    /// <summary>
    /// Specifies the name of the voice to use.
    ///
    /// This can be either the name of a previously created Custom Voice or one of our 8 base voices: `ITO`, `KORA`, `DACHER`, `AURA`, `FINN`, `WHIMSY`, `STELLA`, or `SUNNY`.
    ///
    /// The name will be automatically converted to uppercase (e.g., "Ito" becomes "ITO"). If a name is not specified, then a [Custom Voice](/reference/empathic-voice-interface-evi/configs/create-config#request.body.voice.custom_voice) specification must be provided.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("custom_voice")]
    public PostedCustomVoice? CustomVoice { get; set; }

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
