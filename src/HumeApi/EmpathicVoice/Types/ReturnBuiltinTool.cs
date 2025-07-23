using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

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
    /// Type of Tool. Either `BUILTIN` for natively implemented tools, like web search, or `FUNCTION` for user-defined tools.
    /// </summary>
    [JsonPropertyName("tool_type")]
    public required ReturnBuiltinToolToolType ToolType { get; set; }

    /// <summary>
    /// Name applied to all versions of a particular Tool.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Optional text passed to the supplemental LLM in place of the tool call result. The LLM then uses this text to generate a response back to the user, ensuring continuity in the conversation if the Tool errors.
    /// </summary>
    [JsonPropertyName("fallback_content")]
    public string? FallbackContent { get; set; }

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
