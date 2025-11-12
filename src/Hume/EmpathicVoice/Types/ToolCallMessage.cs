using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// When provided, the output is a tool call.
/// </summary>
[Serializable]
public record ToolCallMessage : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Used to manage conversational state, correlate frontend and backend data, and persist conversations across EVI sessions.
    /// </summary>
    [JsonPropertyName("custom_session_id")]
    public string? CustomSessionId { get; set; }

    /// <summary>
    /// Name of the tool called.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Parameters of the tool call. Is a stringified JSON schema.
    /// </summary>
    [JsonPropertyName("parameters")]
    public required string Parameters { get; set; }

    /// <summary>
    /// Indicates whether a response to the tool call is required from the developer, either in the form of a [Tool Response message](/reference/empathic-voice-interface-evi/chat/chat#send.Tool%20Response%20Message.type) or a [Tool Error message](/reference/empathic-voice-interface-evi/chat/chat#send.Tool%20Error%20Message.type).
    /// </summary>
    [JsonPropertyName("response_required")]
    public required bool ResponseRequired { get; set; }

    /// <summary>
    /// The unique identifier for a specific tool call instance.
    ///
    /// This ID is used to track the request and response of a particular tool invocation, ensuring that the correct response is linked to the appropriate request.
    /// </summary>
    [JsonPropertyName("tool_call_id")]
    public required string ToolCallId { get; set; }

    /// <summary>
    /// Type of tool called. Either `builtin` for natively implemented tools, like web search, or `function` for user-defined tools.
    /// </summary>
    [JsonPropertyName("tool_type")]
    public required ToolType ToolType { get; set; }

    /// <summary>
    /// The type of message sent through the socket; for a Tool Call message, this must be `tool_call`.
    ///
    /// This message indicates that the supplemental LLM has detected a need to invoke the specified tool.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

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
