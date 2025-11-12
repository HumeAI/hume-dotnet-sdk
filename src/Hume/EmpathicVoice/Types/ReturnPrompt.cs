using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// A specific prompt version returned from the server
/// </summary>
[Serializable]
public record ReturnPrompt : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The timestamp when the first version of this prompt was created.
    /// </summary>
    [JsonPropertyName("created_on")]
    public required long CreatedOn { get; set; }

    /// <summary>
    /// Identifier for a Prompt. Formatted as a UUID.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// The timestamp when this version of the prompt was created.
    /// </summary>
    [JsonPropertyName("modified_on")]
    public required long ModifiedOn { get; set; }

    /// <summary>
    /// Name applied to all versions of a particular Prompt.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Text used for this version of the Prompt.
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <summary>
    /// Version number for a Prompt. Version numbers should be integers. The combination of configId and version number is unique.
    /// </summary>
    [JsonPropertyName("version")]
    public required int Version { get; set; }

    /// <summary>
    /// Description that is appended to a specific version of a Prompt.
    /// </summary>
    [JsonPropertyName("version_description")]
    public string? VersionDescription { get; set; }

    /// <summary>
    /// Indicates whether this prompt is using a fixed version number or auto-updating to the latest version. Values from the VersionType enum.
    /// </summary>
    [JsonPropertyName("version_type")]
    public required string VersionType { get; set; }

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
