using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

/// <summary>
/// A specific tool version returned from the server
/// </summary>
[Serializable]
public record ReturnUserDefinedTool : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Type of Tool. Either `BUILTIN` for natively implemented tools, like web search, or `FUNCTION` for user-defined tools.
    /// </summary>
    [JsonPropertyName("tool_type")]
    public required ReturnUserDefinedToolToolType ToolType { get; set; }

    /// <summary>
    /// Identifier for a Tool. Formatted as a UUID.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// Version number for a Tool.
    ///
    /// Tools, Configs, Custom Voices, and Prompts are versioned. This versioning system supports iterative development, allowing you to progressively refine tools and revert to previous versions if needed.
    ///
    /// Version numbers are integer values representing different iterations of the Tool. Each update to the Tool increments its version number.
    /// </summary>
    [JsonPropertyName("version")]
    public required int Version { get; set; }

    /// <summary>
    /// Versioning method for a Tool. Either `FIXED` for using a fixed version number or `LATEST` for auto-updating to the latest version.
    /// </summary>
    [JsonPropertyName("version_type")]
    public required ReturnUserDefinedToolVersionType VersionType { get; set; }

    /// <summary>
    /// An optional description of the Tool version.
    /// </summary>
    [JsonPropertyName("version_description")]
    public string? VersionDescription { get; set; }

    /// <summary>
    /// Name applied to all versions of a particular Tool.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Time at which the Tool was created. Measured in seconds since the Unix epoch.
    /// </summary>
    [JsonPropertyName("created_on")]
    public required long CreatedOn { get; set; }

    /// <summary>
    /// Time at which the Tool was last modified. Measured in seconds since the Unix epoch.
    /// </summary>
    [JsonPropertyName("modified_on")]
    public required long ModifiedOn { get; set; }

    /// <summary>
    /// Optional text passed to the supplemental LLM in place of the tool call result. The LLM then uses this text to generate a response back to the user, ensuring continuity in the conversation if the Tool errors.
    /// </summary>
    [JsonPropertyName("fallback_content")]
    public string? FallbackContent { get; set; }

    /// <summary>
    /// An optional description of what the Tool does, used by the supplemental LLM to choose when and how to call the function.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Stringified JSON defining the parameters used by this version of the Tool.
    ///
    /// These parameters define the inputs needed for the Tool’s execution, including the expected data type and description for each input field. Structured as a stringified JSON schema, this format ensures the tool receives data in the expected format.
    /// </summary>
    [JsonPropertyName("parameters")]
    public required string Parameters { get; set; }

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
