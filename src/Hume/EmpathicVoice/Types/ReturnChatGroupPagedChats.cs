using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// A description of chat_group and its status with a paginated list of each chat in the chat_group
/// </summary>
[Serializable]
public record ReturnChatGroupPagedChats : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Identifier for the Chat Group. Any Chat resumed from this Chat Group will have the same `chat_group_id`. Formatted as a UUID.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// Time at which the first Chat in this Chat Group was created. Measured in seconds since the Unix epoch.
    /// </summary>
    [JsonPropertyName("first_start_timestamp")]
    public required long FirstStartTimestamp { get; set; }

    /// <summary>
    /// Time at which the most recent Chat in this Chat Group was created. Measured in seconds since the Unix epoch.
    /// </summary>
    [JsonPropertyName("most_recent_start_timestamp")]
    public required long MostRecentStartTimestamp { get; set; }

    /// <summary>
    /// The total number of Chats associated with this Chat Group.
    /// </summary>
    [JsonPropertyName("num_chats")]
    public required int NumChats { get; set; }

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
    /// Indicates the order in which the paginated results are presented, based on their creation date.
    ///
    /// It shows `ASC` for ascending order (chronological, with the oldest records first) or `DESC` for descending order (reverse-chronological, with the newest records first). This value corresponds to the `ascending_order` query parameter used in the request.
    /// </summary>
    [JsonPropertyName("pagination_direction")]
    public required ReturnChatGroupPagedChatsPaginationDirection PaginationDirection { get; set; }

    /// <summary>
    /// List of Chats for the specified `page_number` and `page_size`.
    /// </summary>
    [JsonPropertyName("chats_page")]
    public IEnumerable<ReturnChat> ChatsPage { get; set; } = new List<ReturnChat>();

    /// <summary>
    /// Denotes whether there is an active Chat associated with this Chat Group.
    /// </summary>
    [JsonPropertyName("active")]
    public bool? Active { get; set; }

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
