using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

[Serializable]
public record WebhookEventChatEnded : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Phone number of the caller in E.164 format (e.g., `+12223333333`). This field is included only if the Chat was created via the [Twilio phone calling](/docs/empathic-voice-interface-evi/phone-calling) integration.
    /// </summary>
    [JsonPropertyName("caller_number")]
    public string? CallerNumber { get; set; }

    /// <summary>
    /// User-defined session ID. Relevant only when employing a [custom language model](/docs/empathic-voice-interface-evi/custom-language-model) in the EVI Config.
    /// </summary>
    [JsonPropertyName("custom_session_id")]
    public string? CustomSessionId { get; set; }

    /// <summary>
    /// Total duration of the session in seconds.
    /// </summary>
    [JsonPropertyName("duration_seconds")]
    public required int DurationSeconds { get; set; }

    /// <summary>
    /// Reason for the session's termination.
    /// </summary>
    [JsonPropertyName("end_reason")]
    public required WebhookEventChatStatus EndReason { get; set; }

    /// <summary>
    /// Unix timestamp (in milliseconds) indicating when the session ended.
    /// </summary>
    [JsonPropertyName("end_time")]
    public required int EndTime { get; set; }

    /// <summary>
    /// Always `chat_ended`.
    /// </summary>
    [JsonPropertyName("event_name")]
    public string? EventName { get; set; }

    /// <summary>
    /// Twilio metadata associated with the chat. This field is included only if the Chat was created via the [Twilio phone calling](/docs/empathic-voice-interface-evi/phone-calling) integration. Fields may include `call_sid`, `account_sid`, `from_number`, `to_number`, `caller_name`, `caller_number`, `from_city`, `from_state`, `from_zip`, `from_country`, `to_city`, `to_state`, `to_zip`, and `to_country`.If a specific metadata is not available, this field will be set to an empty string.
    /// </summary>
    [JsonPropertyName("twilio_metadata")]
    public Dictionary<string, string>? TwilioMetadata { get; set; }

    /// <summary>
    /// Unique ID of the **Chat Group** associated with the **Chat** session.
    /// </summary>
    [JsonPropertyName("chat_group_id")]
    public required string ChatGroupId { get; set; }

    /// <summary>
    /// Unique ID of the **Chat** session.
    /// </summary>
    [JsonPropertyName("chat_id")]
    public required string ChatId { get; set; }

    /// <summary>
    /// Unique ID of the EVI **Config** used for the session.
    /// </summary>
    [JsonPropertyName("config_id")]
    public string? ConfigId { get; set; }

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
