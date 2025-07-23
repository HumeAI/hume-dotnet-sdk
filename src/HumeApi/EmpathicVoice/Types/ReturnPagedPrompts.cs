using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

/// <summary>
/// A paginated list of prompt versions returned from the server
/// </summary>
[Serializable]
public record ReturnPagedPrompts : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The page number of the returned list.
    ///
    /// This value corresponds to the `page_number` parameter specified in the request. Pagination uses zero-based indexing.
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
    /// List of prompts returned for the specified `page_number` and `page_size`.
    /// </summary>
    [JsonPropertyName("prompts_page")]
    public IEnumerable<ReturnPrompt> PromptsPage { get; set; } = new List<ReturnPrompt>();

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
