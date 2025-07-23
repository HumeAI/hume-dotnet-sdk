using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

/// <summary>
/// When provided, the output is a chat metadata message.
/// </summary>
[Serializable]
public record ChatMetadata : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// ID of the Chat Group.
    ///
    /// Used to resume a Chat when passed in the [resumed_chat_group_id](/reference/empathic-voice-interface-evi/chat/chat#request.query.resumed_chat_group_id) query parameter of a subsequent connection request. This allows EVI to continue the conversation from where it left off within the Chat Group.
    ///
    /// Learn more about [supporting chat resumability](/docs/empathic-voice-interface-evi/faq#does-evi-support-chat-resumability) from the EVI FAQ.
    /// </summary>
    [JsonPropertyName("chat_group_id")]
    public required string ChatGroupId { get; set; }

    /// <summary>
    /// ID of the Chat session. Allows the Chat session to be tracked and referenced.
    /// </summary>
    [JsonPropertyName("chat_id")]
    public required string ChatId { get; set; }

    /// <summary>
    /// Used to manage conversational state, correlate frontend and backend data, and persist conversations across EVI sessions.
    /// </summary>
    [JsonPropertyName("custom_session_id")]
    public string? CustomSessionId { get; set; }

    /// <summary>
    /// ID of the initiating request.
    /// </summary>
    [JsonPropertyName("request_id")]
    public string? RequestId { get; set; }

    /// <summary>
    /// The type of message sent through the socket; for a Chat Metadata message, this must be `chat_metadata`.
    ///
    /// The Chat Metadata message is the first message you receive after establishing a connection with EVI and contains important identifiers for the current Chat session.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = "chat_metadata";

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
