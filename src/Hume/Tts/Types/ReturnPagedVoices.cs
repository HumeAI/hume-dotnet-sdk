using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.Tts;

/// <summary>
/// A paginated list Octave voices available for text-to-speech
/// </summary>
[Serializable]
public record ReturnPagedVoices : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The page number of the returned results.
    /// </summary>
    [JsonPropertyName("page_number")]
    public required int PageNumber { get; set; }

    /// <summary>
    /// The maximum number of items returned per page.
    ///
    /// This value corresponds to the `page_size` parameter specified in the request.
    /// </summary>
    [JsonPropertyName("page_size")]
    public required int PageSize { get; set; }

    /// <summary>
    /// The total number of pages in the collection.
    /// </summary>
    [JsonPropertyName("total_pages")]
    public required int TotalPages { get; set; }

    /// <summary>
    /// List of voices returned for the specified `page_number` and `page_size`.
    /// </summary>
    [JsonPropertyName("voices_page")]
    public IEnumerable<ReturnVoice> VoicesPage { get; set; } = new List<ReturnVoice>();

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
