using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

[Serializable]
public record Tool : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// An optional description of what the tool does, used by the supplemental LLM to choose when and how to call the function.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Optional text passed to the supplemental LLM if the tool call fails. The LLM then uses this text to generate a response back to the user, ensuring continuity in the conversation.
    /// </summary>
    [JsonPropertyName("fallback_content")]
    public string? FallbackContent { get; set; }

    /// <summary>
    /// Name of the user-defined tool to be enabled.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Parameters of the tool. Is a stringified JSON schema.
    ///
    /// These parameters define the inputs needed for the tool’s execution, including the expected data type and description for each input field. Structured as a JSON schema, this format ensures the tool receives data in the expected format.
    /// </summary>
    [JsonPropertyName("parameters")]
    public required string Parameters { get; set; }

    /// <summary>
    /// Type of tool. Set to `function` for user-defined tools.
    /// </summary>
    [JsonPropertyName("type")]
    public required ToolType Type { get; set; }

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
