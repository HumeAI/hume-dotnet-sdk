using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[Serializable]
public record PostedUserDefinedToolVersionDescription
{
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
