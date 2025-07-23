using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.Tts;

[Serializable]
public record PostedUtteranceVoiceWithId : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The unique ID associated with the **Voice**.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// Specifies the source provider associated with the chosen voice.
    ///
    /// - **`HUME_AI`**: Select voices from Hume's [Voice Library](https://platform.hume.ai/tts/voice-library), containing a variety of preset, shared voices.
    /// - **`CUSTOM_VOICE`**: Select from voices you've personally generated and saved in your account.
    ///
    /// If no provider is explicitly set, the default provider is `CUSTOM_VOICE`. When using voices from Hume's **Voice Library**, you must explicitly set the provider to `HUME_AI`.
    ///
    /// Preset voices from Hume's **Voice Library** are accessible by all users. In contrast, your custom voices are private and accessible only via requests authenticated with your API key.
    /// </summary>
    [JsonPropertyName("provider")]
    public VoiceProvider? Provider { get; set; }

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
