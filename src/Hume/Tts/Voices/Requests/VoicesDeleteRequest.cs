using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.Tts;

[Serializable]
public record VoicesDeleteRequest
{
    /// <summary>
    /// Name of the voice to delete
    /// </summary>
    [JsonIgnore]
    public required string Name { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
