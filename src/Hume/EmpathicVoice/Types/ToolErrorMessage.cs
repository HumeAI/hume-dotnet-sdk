using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// When provided, the output is a function call error.
/// </summary>
[Serializable]
public record ToolErrorMessage : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The type of message sent through the socket; for a Tool Error message, this must be `tool_error`.
    ///
    /// Upon receiving a [Tool Call message](/reference/speech-to-speech-evi/chat#receive.ToolCallMessage) and failing to invoke the function, this message is sent to notify EVI of the tool's failure.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = "tool_error";

    /// <summary>
    /// Used to manage conversational state, correlate frontend and backend data, and persist conversations across EVI sessions.
    /// </summary>
    [JsonPropertyName("custom_session_id")]
    public string? CustomSessionId { get; set; }

    /// <summary>
    /// Type of tool called. Either `builtin` for natively implemented tools, like web search, or `function` for user-defined tools.
    /// </summary>
    [JsonPropertyName("tool_type")]
    public ToolType? ToolType { get; set; }

    /// <summary>
    /// The unique identifier for a specific tool call instance.
    ///
    /// This ID is used to track the request and response of a particular tool invocation, ensuring that the Tool Error message is linked to the appropriate tool call request. The specified `tool_call_id` must match the one received in the [Tool Call message](/reference/speech-to-speech-evi/chat#receive.ToolCallMessage).
    /// </summary>
    [JsonPropertyName("tool_call_id")]
    public required string ToolCallId { get; set; }

    /// <summary>
    /// Optional text passed to the supplemental LLM in place of the tool call result. The LLM then uses this text to generate a response back to the user, ensuring continuity in the conversation if the tool errors.
    /// </summary>
    [JsonPropertyName("content")]
    public string? Content { get; set; }

    /// <summary>
    /// Error message from the tool call, not exposed to the LLM or user.
    /// </summary>
    [JsonPropertyName("error")]
    public required string Error { get; set; }

    /// <summary>
    /// Error code. Identifies the type of error encountered.
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    /// <summary>
    /// Indicates the severity of an error; for a Tool Error message, this must be `warn` to signal an unexpected event.
    /// </summary>
    [JsonPropertyName("level")]
    public string? Level { get; set; }

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
