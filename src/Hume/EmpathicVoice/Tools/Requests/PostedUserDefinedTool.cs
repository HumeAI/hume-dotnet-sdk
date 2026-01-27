using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[Serializable]
public record PostedUserDefinedTool
{
    /// <summary>
    /// An optional description of what the Tool does, used by the supplemental LLM to choose when and how to call the function.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Optional text passed to the supplemental LLM in place of the tool call result. The LLM then uses this text to generate a response back to the user, ensuring continuity in the conversation if the Tool errors.
    /// </summary>
    [JsonPropertyName("fallback_content")]
    public string? FallbackContent { get; set; }

    /// <summary>
    /// Name of the built-in tool to use. Hume supports the following built-in tools:
    ///
    /// - **web_search:** enables EVI to search the web for up-to-date information when applicable.
    /// - **hang_up:** closes the WebSocket connection when appropriate (e.g., after detecting a farewell in the conversation).
    ///
    /// For more information, see our guide on [using built-in tools](/docs/speech-to-speech-evi/features/tool-use#using-built-in-tools).
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Stringified JSON defining the parameters used by this version of the Tool.
    ///
    /// These parameters define the inputs needed for the Tool's execution, including the expected data type and description for each input field. Structured as a stringified JSON schema, this format ensures the Tool receives data in the expected format.
    /// </summary>
    [JsonPropertyName("parameters")]
    public required string Parameters { get; set; }

    /// <summary>
    /// An optional description of the Tool version.
    /// </summary>
    [JsonPropertyName("version_description")]
    public string? VersionDescription { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
