using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.Tts;

[Serializable]
public record Snippet : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The segmented audio output in the requested format, encoded as a base64 string.
    /// </summary>
    [JsonPropertyName("audio")]
    public required string Audio { get; set; }

    /// <summary>
    /// The generation ID this snippet corresponds to.
    /// </summary>
    [JsonPropertyName("generation_id")]
    public required string GenerationId { get; set; }

    /// <summary>
    /// A unique ID associated with this **Snippet**.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// The text for this **Snippet**.
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <summary>
    /// A list of word or phoneme level timestamps for the generated audio. Timestamps are only returned for Octave 2 requests.
    /// </summary>
    [JsonPropertyName("timestamps")]
    public IEnumerable<Timestamp> Timestamps { get; set; } = new List<Timestamp>();

    /// <summary>
    /// The transcribed text of the generated audio. It is only present if `instant_mode` is set to `false`.
    /// </summary>
    [JsonPropertyName("transcribed_text")]
    public string? TranscribedText { get; set; }

    /// <summary>
    /// The index of the utterance in the request this snippet corresponds to.
    /// </summary>
    [JsonPropertyName("utterance_index")]
    public int? UtteranceIndex { get; set; }

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
