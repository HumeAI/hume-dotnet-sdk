using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.Tts;

[Serializable]
public record SnippetAudioChunk : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The generation ID of the parent snippet that this chunk corresponds to.
    /// </summary>
    [JsonPropertyName("generation_id")]
    public required string GenerationId { get; set; }

    /// <summary>
    /// The ID of the parent snippet that this chunk corresponds to.
    /// </summary>
    [JsonPropertyName("snippet_id")]
    public required string SnippetId { get; set; }

    /// <summary>
    /// The text of the parent snippet that this chunk corresponds to.
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <summary>
    /// The transcribed text of the generated audio of the parent snippet that this chunk corresponds to. It is only present if `instant_mode` is set to `false`.
    /// </summary>
    [JsonPropertyName("transcribed_text")]
    public string? TranscribedText { get; set; }

    /// <summary>
    /// The index of the audio chunk in the snippet.
    /// </summary>
    [JsonPropertyName("chunk_index")]
    public required int ChunkIndex { get; set; }

    /// <summary>
    /// The generated audio output chunk in the requested format.
    /// </summary>
    [JsonPropertyName("audio")]
    public required string Audio { get; set; }

    /// <summary>
    /// The generated audio output format.
    /// </summary>
    [JsonPropertyName("audio_format")]
    public required AudioFormatType AudioFormat { get; set; }

    /// <summary>
    /// Whether or not this is the last chunk streamed back from the decoder for one input snippet.
    /// </summary>
    [JsonPropertyName("is_last_chunk")]
    public required bool IsLastChunk { get; set; }

    /// <summary>
    /// The index of the utterance in the request that the parent snippet of this chunk corresponds to.
    /// </summary>
    [JsonPropertyName("utterance_index")]
    public int? UtteranceIndex { get; set; }

    [JsonPropertyName("snippet")]
    public Snippet? Snippet { get; set; }

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
