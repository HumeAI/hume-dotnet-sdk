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

    [JsonPropertyName("active")]
    public bool? Active { get; set; }

    /// <summary>
    /// List of chats and their metadata returned for the specified page number and page size.
    /// </summary>
    [JsonPropertyName("chats_page")]
    public IEnumerable<ReturnChat> ChatsPage { get; set; } = new List<ReturnChat>();

    /// <summary>
    /// The timestamp when the first chat in this chat group started, formatted as a Unix epoch milliseconds.
    /// </summary>
    [JsonPropertyName("first_start_timestamp")]
    public required long FirstStartTimestamp { get; set; }

    /// <summary>
    /// Identifier for the chat group. Any chat resumed from this chat will have the same chat_group_id. Formatted as a UUID.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// The timestamp when the most recent chat in this chat group started, formatted as a Unix epoch milliseconds.
    /// </summary>
    [JsonPropertyName("most_recent_start_timestamp")]
    public required long MostRecentStartTimestamp { get; set; }

    /// <summary>
    /// The total number of chats in this chat group.
    /// </summary>
    [JsonPropertyName("num_chats")]
    public required int NumChats { get; set; }

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

    [JsonPropertyName("pagination_direction")]
    public required ReturnChatGroupPagedChatsPaginationDirection PaginationDirection { get; set; }

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
