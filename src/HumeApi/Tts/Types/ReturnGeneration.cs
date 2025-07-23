using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.Tts;

[Serializable]
public record ReturnGeneration : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The generated audio output in the requested format, encoded as a base64 string.
    /// </summary>
    [JsonPropertyName("audio")]
    public required string Audio { get; set; }

    /// <summary>
    /// Duration of the generated audio in seconds.
    /// </summary>
    [JsonPropertyName("duration")]
    public required double Duration { get; set; }

    [JsonPropertyName("encoding")]
    public required AudioEncoding Encoding { get; set; }

    /// <summary>
    /// Size of the generated audio in bytes.
    /// </summary>
    [JsonPropertyName("file_size")]
    public required int FileSize { get; set; }

    /// <summary>
    /// A unique ID associated with this TTS generation that can be used as context for generating consistent speech style and prosody across multiple requests.
    /// </summary>
    [JsonPropertyName("generation_id")]
    public required string GenerationId { get; set; }

    /// <summary>
    /// A list of snippet groups where each group corresponds to an utterance in the request. Each group contains segmented snippets that represent the original utterance divided into more natural-sounding units optimized for speech delivery.
    /// </summary>
    [JsonPropertyName("snippets")]
    public IEnumerable<IEnumerable<Snippet>> Snippets { get; set; } =
        new List<IEnumerable<Snippet>>();

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
