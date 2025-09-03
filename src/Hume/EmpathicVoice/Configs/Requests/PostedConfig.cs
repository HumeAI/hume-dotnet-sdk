using System.Text.Json.Serialization;
using Hume.Core;
using OneOf;

namespace Hume.EmpathicVoice;

[Serializable]
public record PostedConfig
{
    /// <summary>
    /// Specifies the EVI version to use. See our [EVI Version  Guide](/docs/speech-to-speech-evi/configuration/evi-version) for differences between versions.
    ///
    /// **We're officially sunsetting EVI versions 1 and 2 on August 30, 2025**. To keep things running smoothly, be sure to [migrate to EVI 3](/docs/speech-to-speech-evi/configuration/evi-version#migrating-to-evi-3) before then.
    /// </summary>
    [JsonPropertyName("evi_version")]
    public required string EviVersion { get; set; }

    /// <summary>
    /// Name applied to all versions of a particular Config.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// An optional description of the Config version.
    /// </summary>
    [JsonPropertyName("version_description")]
    public string? VersionDescription { get; set; }

    [JsonPropertyName("prompt")]
    public PostedConfigPromptSpec? Prompt { get; set; }

    /// <summary>
    /// A voice specification associated with this Config.
    /// </summary>
    [JsonPropertyName("voice")]
    public OneOf<VoiceId, VoiceName>? Voice { get; set; }

    /// <summary>
    /// The supplemental language model associated with this Config.
    ///
    /// This model is used to generate longer, more detailed responses from EVI. Choosing an appropriate supplemental language model for your use case is crucial for generating fast, high-quality responses from EVI.
    /// </summary>
    [JsonPropertyName("language_model")]
    public PostedLanguageModel? LanguageModel { get; set; }

    /// <summary>
    /// The eLLM setup associated with this Config.
    ///
    /// Hume's eLLM (empathic Large Language Model) is a multimodal language model that takes into account both expression measures and language. The eLLM generates short, empathic language responses and guides text-to-speech (TTS) prosody.
    /// </summary>
    [JsonPropertyName("ellm_model")]
    public PostedEllmModel? EllmModel { get; set; }

    /// <summary>
    /// List of user-defined tools associated with this Config.
    /// </summary>
    [JsonPropertyName("tools")]
    public IEnumerable<PostedUserDefinedToolSpec>? Tools { get; set; }

    /// <summary>
    /// List of built-in tools associated with this Config.
    /// </summary>
    [JsonPropertyName("builtin_tools")]
    public IEnumerable<PostedBuiltinTool>? BuiltinTools { get; set; }

    [JsonPropertyName("event_messages")]
    public PostedEventMessageSpecs? EventMessages { get; set; }

    /// <summary>
    /// Configures nudges, brief audio prompts that can guide conversations when users pause or need encouragement to continue speaking. Nudges help create more natural, flowing interactions by providing gentle conversational cues.
    /// </summary>
    [JsonPropertyName("nudges")]
    public PostedNudgeSpec? Nudges { get; set; }

    [JsonPropertyName("timeouts")]
    public PostedTimeoutSpecs? Timeouts { get; set; }

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
