using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[Serializable]
public record PostedPromptVersionDescription
{
    /// <summary>
    /// An optional description of the Prompt version.
    /// </summary>
    [JsonPropertyName("version_description")]
    public string? VersionDescription { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
