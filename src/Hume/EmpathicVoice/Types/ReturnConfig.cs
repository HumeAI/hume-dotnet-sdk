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
    /// List of built-in tools associated with this config
    /// </summary>
    [JsonPropertyName("builtin_tools")]
    public IEnumerable<ReturnBuiltinTool>? BuiltinTools { get; set; }

    /// <summary>
    /// The timestamp when the first version of this config was created.
    /// </summary>
    [JsonPropertyName("created_on")]
    public long? CreatedOn { get; set; }

    [JsonPropertyName("ellm_model")]
    public ReturnEllmModel? EllmModel { get; set; }

    [JsonPropertyName("event_messages")]
    public ReturnEventMessageSpecs? EventMessages { get; set; }

    /// <summary>
    /// The version of the EVI used with this config.
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
    /// The timestamp when this version of the config was created.
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
    /// List of user-defined tools associated with this config.
    /// </summary>
    [JsonPropertyName("tools")]
    public IEnumerable<ReturnUserDefinedTool>? Tools { get; set; }

    /// <summary>
    /// Version number for a Config. Version numbers should be integers. The combination of configId and version number is unique.
    /// </summary>
    [JsonPropertyName("version")]
    public int? Version { get; set; }

    /// <summary>
    /// Description that is appended to a specific version of a Config.
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
