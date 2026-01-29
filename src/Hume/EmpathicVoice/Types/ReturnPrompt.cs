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
    /// Time at which the Prompt was created. Measured in seconds since the Unix epoch.
    /// </summary>
    [JsonPropertyName("created_on")]
    public required long CreatedOn { get; set; }

    /// <summary>
    /// Identifier for a Prompt. Formatted as a UUID.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// Time at which the Prompt was last modified. Measured in seconds since the Unix epoch.
    /// </summary>
    [JsonPropertyName("modified_on")]
    public required long ModifiedOn { get; set; }

    /// <summary>
    /// Name applied to all versions of a particular Prompt.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Instructions used to shape EVI's behavior, responses, and style.
    ///
    /// You can use the Prompt to define a specific goal or role for EVI, specifying how it should act or what it should focus on during the conversation. For example, EVI can be instructed to act as a customer support representative, a fitness coach, or a travel advisor, each with its own set of behaviors and response styles. For help writing a system prompt, see our [Prompting Guide](/docs/speech-to-speech-evi/guides/prompting).
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <summary>
    /// Version number for a Prompt. Version numbers should be integers. The combination of configId and version number is unique.
    /// </summary>
    [JsonPropertyName("version")]
    public required int Version { get; set; }

    /// <summary>
    /// An optional description of the Prompt version.
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
