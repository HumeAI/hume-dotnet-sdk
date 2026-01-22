using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// A specific builtin tool version returned from the server
/// </summary>
[Serializable]
public record ReturnBuiltinTool : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

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

    [JsonPropertyName("tool_type")]
    public required ReturnBuiltinToolToolType ToolType { get; set; }

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
