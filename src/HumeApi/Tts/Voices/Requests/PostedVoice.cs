using System.Text.Json.Serialization;
using HumeApi.Core;

namespace HumeApi.Tts;

[Serializable]
public record PostedVoice
{
    /// <summary>
    /// A unique ID associated with this TTS generation that can be used as context for generating consistent speech style and prosody across multiple requests.
    /// </summary>
    [JsonPropertyName("generation_id")]
    public required string GenerationId { get; set; }

    /// <summary>
    /// Name of the voice in the `Voice Library`.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
