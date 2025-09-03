using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[Serializable]
public record PostedUserDefinedToolVersionDescription
{
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
