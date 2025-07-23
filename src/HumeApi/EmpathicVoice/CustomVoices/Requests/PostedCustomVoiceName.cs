using System.Text.Json.Serialization;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

[Serializable]
public record PostedCustomVoiceName
{
    /// <summary>
    /// The name of the Custom Voice. Maximum length of 75 characters. Will be converted to all-uppercase. (e.g., "sample voice" becomes "SAMPLE VOICE")
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
