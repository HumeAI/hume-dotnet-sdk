using System.Text.Json.Serialization;
using Hume.Core;
using OneOf;

namespace Hume.EmpathicVoice;

[Serializable]
public record PostedConfig
{
    /// <summary>
    /// Built-in tool specification for a Config.
    /// </summary>
    [JsonPropertyName("builtin_tools")]
    public IEnumerable<PostedBuiltinTool>? BuiltinTools { get; set; }

    [JsonPropertyName("ellm_model")]
    public PostedEllmModel? EllmModel { get; set; }

    [JsonPropertyName("event_messages")]
    public PostedEventMessageSpecs? EventMessages { get; set; }

    /// <summary>
    /// The version of the EVI used with this config.
    /// </summary>
    [JsonPropertyName("evi_version")]
    public required string EviVersion { get; set; }

    [JsonPropertyName("language_model")]
    public PostedLanguageModel? LanguageModel { get; set; }

    /// <summary>
    /// Name applied to all versions of a particular Config.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("nudges")]
    public PostedNudgeSpec? Nudges { get; set; }

    [JsonPropertyName("prompt")]
    public PostedConfigPromptSpec? Prompt { get; set; }

    [JsonPropertyName("timeouts")]
    public PostedTimeoutSpecs? Timeouts { get; set; }

    /// <summary>
    /// Tool specification for a Config.
    /// </summary>
    [JsonPropertyName("tools")]
    public IEnumerable<PostedUserDefinedToolSpec>? Tools { get; set; }

    /// <summary>
    /// Description that is appended to a specific version of a Config.
    /// </summary>
    [JsonPropertyName("version_description")]
    public string? VersionDescription { get; set; }

    [JsonPropertyName("voice")]
    public OneOf<VoiceId, VoiceName>? Voice { get; set; }

    /// <summary>
    /// Webhook config specifications for each subscriber.
    /// </summary>
    [JsonPropertyName("webhooks")]
    public IEnumerable<PostedWebhookSpec>? Webhooks { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
