using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// **Indicates that the supplemental LLM has detected a need to invoke the specified tool.** This message is only received for user-defined function tools.
///
/// Contains the tool name, parameters (as a stringified JSON schema), whether a response is required from the developer (either in the form of a `ToolResponseMessage` or a `ToolErrorMessage`), the unique tool call ID for tracking the request and response, and the tool type. See our [Tool Use Guide](/docs/speech-to-speech-evi/features/tool-use) for further details.
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
    /// Parameters of the tool.
    ///
    /// These parameters define the inputs needed for the tool's execution, including the expected data type and description for each input field. Structured as a stringified JSON schema, this format ensures the tool receives data in the expected format.
    /// </summary>
    [JsonPropertyName("parameters")]
    public required string Parameters { get; set; }

    /// <summary>
    /// Indicates whether a response to the tool call is required from the developer, either in the form of a [Tool Response message](/reference/speech-to-speech-evi/chat#send.ToolResponseMessage) or a [Tool Error message](/reference/speech-to-speech-evi/chat#send.ToolErrorMessage).
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
    public ToolType? ToolType { get; set; }

    /// <summary>
    /// The type of message sent through the socket; for a Tool Call message, this must be `tool_call`.
    ///
    /// This message indicates that the supplemental LLM has detected a need to invoke the specified tool.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type
    {
        get => "tool_call";
        set => value.Assert(value == "tool_call", "'Type' must be " + "tool_call");
    }

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
