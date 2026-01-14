using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[Serializable]
public record PostedUserDefinedToolVersion
{
    /// <summary>
    /// Text describing what the tool does.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Text to use if the tool fails to generate content.
    /// </summary>
    [JsonPropertyName("fallback_content")]
    public string? FallbackContent { get; set; }

    /// <summary>
    /// Stringified JSON defining the parameters used by this version of the Tool.
    /// </summary>
    [JsonPropertyName("parameters")]
    public required string Parameters { get; set; }

    /// <summary>
    /// Description that is appended to a specific version of a Tool.
    /// </summary>
    [JsonPropertyName("version_description")]
    public string? VersionDescription { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
