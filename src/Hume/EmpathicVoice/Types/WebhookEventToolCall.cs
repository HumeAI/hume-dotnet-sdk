using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

[Serializable]
public record WebhookEventToolCall : IJsonOnDeserialized
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
    /// Always `tool_call`.
    /// </summary>
    [JsonPropertyName("event_name")]
    public string? EventName { get; set; }

    /// <summary>
    /// Unix timestamp (in milliseconds) indicating when the tool call was triggered.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public required int Timestamp { get; set; }

    /// <summary>
    /// The tool call.
    /// </summary>
    [JsonPropertyName("tool_call_message")]
    public required ToolCallMessage ToolCallMessage { get; set; }

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
