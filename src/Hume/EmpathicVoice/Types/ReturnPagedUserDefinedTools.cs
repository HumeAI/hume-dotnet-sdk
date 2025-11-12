using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// A paginated list of user defined tool versions returned from the server
/// </summary>
[Serializable]
public record ReturnPagedUserDefinedTools : IJsonOnDeserialized
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
    /// List of tools returned for the specified `page_number` and `page_size`.
    /// </summary>
    [JsonPropertyName("tools_page")]
    public IEnumerable<ReturnUserDefinedTool> ToolsPage { get; set; } =
        new List<ReturnUserDefinedTool>();

    /// <summary>
    /// The total number of pages in the collection.
    /// </summary>
    [JsonPropertyName("total_pages")]
    public required int TotalPages { get; set; }

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
