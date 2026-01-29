using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

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
    /// List of built-in tools associated with this Config.
    /// </summary>
    [JsonPropertyName("builtin_tools")]
    public IEnumerable<ReturnBuiltinTool>? BuiltinTools { get; set; }

    /// <summary>
    /// Time at which the Config was created. Measured in seconds since the Unix epoch.
    /// </summary>
    [JsonPropertyName("created_on")]
    public long? CreatedOn { get; set; }

    [JsonPropertyName("ellm_model")]
    public ReturnEllmModel? EllmModel { get; set; }

    [JsonPropertyName("event_messages")]
    public ReturnEventMessageSpecs? EventMessages { get; set; }

    /// <summary>
    /// Specifies the EVI version to use. See our [EVI Version  Guide](/docs/speech-to-speech-evi/configuration/evi-version) for differences between versions.
    ///
    /// **We're officially sunsetting EVI versions 1 and 2 on August 30, 2025**. To keep things running smoothly, be sure to [migrate to EVI 3](/docs/speech-to-speech-evi/configuration/evi-version#migrating-to-evi-3) before then.
    /// </summary>
    [JsonPropertyName("evi_version")]
    public string? EviVersion { get; set; }

    /// <summary>
    /// Identifier for a Config. Formatted as a UUID.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("language_model")]
    public ReturnLanguageModel? LanguageModel { get; set; }

    /// <summary>
    /// Time at which the Config was last modified. Measured in seconds since the Unix epoch.
    /// </summary>
    [JsonPropertyName("modified_on")]
    public long? ModifiedOn { get; set; }

    /// <summary>
    /// Name applied to all versions of a particular Config.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("nudges")]
    public ReturnNudgeSpec? Nudges { get; set; }

    [JsonPropertyName("prompt")]
    public ReturnPrompt? Prompt { get; set; }

    [JsonPropertyName("timeouts")]
    public ReturnTimeoutSpecs? Timeouts { get; set; }

    /// <summary>
    /// List of user-defined tools associated with this Config.
    /// </summary>
    [JsonPropertyName("tools")]
    public IEnumerable<ReturnUserDefinedTool>? Tools { get; set; }

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
    /// An optional description of the Config version.
    /// </summary>
    [JsonPropertyName("version_description")]
    public string? VersionDescription { get; set; }

    [JsonPropertyName("voice")]
    public ReturnVoice? Voice { get; set; }

    /// <summary>
    /// Map of webhooks associated with this config.
    /// </summary>
    [JsonPropertyName("webhooks")]
    public IEnumerable<ReturnWebhookSpec>? Webhooks { get; set; }

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
