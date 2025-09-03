using System.Text.Json.Serialization;
using Hume.Core;
using OneOf;

namespace Hume.EmpathicVoice;

[Serializable]
public record PostedConfigVersion
{
    /// <summary>
    /// The version of the EVI used with this config.
    /// </summary>
    [JsonPropertyName("evi_version")]
    public required string EviVersion { get; set; }

    /// <summary>
    /// An optional description of the Config version.
    /// </summary>
    [JsonPropertyName("version_description")]
    public string? VersionDescription { get; set; }

    [JsonPropertyName("prompt")]
    public PostedConfigPromptSpec? Prompt { get; set; }

    /// <summary>
    /// A voice specification associated with this Config version.
    /// </summary>
    [JsonPropertyName("voice")]
    public OneOf<VoiceId, VoiceName>? Voice { get; set; }

    /// <summary>
    /// The supplemental language model associated with this Config version.
    ///
    /// This model is used to generate longer, more detailed responses from EVI. Choosing an appropriate supplemental language model for your use case is crucial for generating fast, high-quality responses from EVI.
    /// </summary>
    [JsonPropertyName("language_model")]
    public PostedLanguageModel? LanguageModel { get; set; }

    /// <summary>
    /// The eLLM setup associated with this Config version.
    ///
    /// Hume's eLLM (empathic Large Language Model) is a multimodal language model that takes into account both expression measures and language. The eLLM generates short, empathic language responses and guides text-to-speech (TTS) prosody.
    /// </summary>
    [JsonPropertyName("ellm_model")]
    public PostedEllmModel? EllmModel { get; set; }

    /// <summary>
    /// List of user-defined tools associated with this Config version.
    /// </summary>
    [JsonPropertyName("tools")]
    public IEnumerable<PostedUserDefinedToolSpec>? Tools { get; set; }

    /// <summary>
    /// List of built-in tools associated with this Config version.
    /// </summary>
    [JsonPropertyName("builtin_tools")]
    public IEnumerable<PostedBuiltinTool>? BuiltinTools { get; set; }

    [JsonPropertyName("event_messages")]
    public PostedEventMessageSpecs? EventMessages { get; set; }

    [JsonPropertyName("timeouts")]
    public PostedTimeoutSpecs? Timeouts { get; set; }

    [JsonPropertyName("nudges")]
    public PostedNudgeSpec? Nudges { get; set; }

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
