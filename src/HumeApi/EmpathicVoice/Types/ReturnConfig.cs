using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

/// <summary>
/// A specific config version returned from the server
/// </summary>
[Serializable]
public record ReturnConfig : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Name applied to all versions of a particular Config.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Identifier for a Config. Formatted as a UUID.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Version number for a Config.
    ///
    /// Configs, Prompts, Custom Voices, and Tools are versioned. This versioning system supports iterative development, allowing you to progressively refine configurations and revert to previous versions if needed.
    ///
    /// Version numbers are integer values representing different iterations of the Config. Each update to the Config increments its version number.
    /// </summary>
    [JsonPropertyName("version")]
    public int? Version { get; set; }

    /// <summary>
    /// Specifies the EVI version to use. Use `"1"` for version 1, or `"2"` for the latest enhanced version. For a detailed comparison of the two versions, refer to our [guide](/docs/speech-to-speech-evi/configuration/evi-version).
    /// </summary>
    [JsonPropertyName("evi_version")]
    public string? EviVersion { get; set; }

    [JsonPropertyName("timeouts")]
    public ReturnTimeoutSpecs? Timeouts { get; set; }

    [JsonPropertyName("nudges")]
    public ReturnNudgeSpec? Nudges { get; set; }

    /// <summary>
    /// The eLLM setup associated with this Config.
    ///
    /// Hume's eLLM (empathic Large Language Model) is a multimodal language model that takes into account both expression measures and language. The eLLM generates short, empathic language responses and guides text-to-speech (TTS) prosody.
    /// </summary>
    [JsonPropertyName("ellm_model")]
    public ReturnEllmModel? EllmModel { get; set; }

    [JsonPropertyName("voice")]
    public object? Voice { get; set; }

    [JsonPropertyName("prompt")]
    public ReturnPrompt? Prompt { get; set; }

    /// <summary>
    /// List of user-defined tools associated with this Config.
    /// </summary>
    [JsonPropertyName("tools")]
    public IEnumerable<ReturnUserDefinedTool>? Tools { get; set; }

    /// <summary>
    /// Map of webhooks associated with this config.
    /// </summary>
    [JsonPropertyName("webhooks")]
    public IEnumerable<ReturnWebhookSpec>? Webhooks { get; set; }

    /// <summary>
    /// Time at which the Config was created. Measured in seconds since the Unix epoch.
    /// </summary>
    [JsonPropertyName("created_on")]
    public long? CreatedOn { get; set; }

    /// <summary>
    /// Time at which the Config was last modified. Measured in seconds since the Unix epoch.
    /// </summary>
    [JsonPropertyName("modified_on")]
    public long? ModifiedOn { get; set; }

    /// <summary>
    /// The supplemental language model associated with this Config.
    ///
    /// This model is used to generate longer, more detailed responses from EVI. Choosing an appropriate supplemental language model for your use case is crucial for generating fast, high-quality responses from EVI.
    /// </summary>
    [JsonPropertyName("language_model")]
    public ReturnLanguageModel? LanguageModel { get; set; }

    /// <summary>
    /// List of built-in tools associated with this Config.
    /// </summary>
    [JsonPropertyName("builtin_tools")]
    public IEnumerable<ReturnBuiltinTool>? BuiltinTools { get; set; }

    [JsonPropertyName("event_messages")]
    public ReturnEventMessageSpecs? EventMessages { get; set; }

    /// <summary>
    /// An optional description of the Config version.
    /// </summary>
    [JsonPropertyName("version_description")]
    public string? VersionDescription { get; set; }

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
