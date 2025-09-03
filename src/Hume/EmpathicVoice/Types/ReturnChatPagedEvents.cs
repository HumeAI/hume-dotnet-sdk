using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// A description of chat status with a paginated list of chat events returned from the server
/// </summary>
[Serializable]
public record ReturnChatPagedEvents : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Identifier for a Chat. Formatted as a UUID.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// Identifier for the Chat Group. Any chat resumed from this Chat will have the same `chat_group_id`. Formatted as a UUID.
    /// </summary>
    [JsonPropertyName("chat_group_id")]
    public required string ChatGroupId { get; set; }

    /// <summary>
    /// Indicates the current state of the chat. There are six possible statuses:
    ///
    /// - `ACTIVE`: The chat is currently active and ongoing.
    ///
    /// - `USER_ENDED`: The chat was manually ended by the user.
    ///
    /// - `USER_TIMEOUT`: The chat ended due to a user-defined timeout.
    ///
    /// - `MAX_DURATION_TIMEOUT`: The chat ended because it reached the maximum allowed duration.
    ///
    /// - `INACTIVITY_TIMEOUT`: The chat ended due to an inactivity timeout.
    ///
    /// - `ERROR`: The chat ended unexpectedly due to an error.
    /// </summary>
    [JsonPropertyName("status")]
    public required ReturnChatPagedEventsStatus Status { get; set; }

    /// <summary>
    /// Time at which the Chat started. Measured in seconds since the Unix epoch.
    /// </summary>
    [JsonPropertyName("start_timestamp")]
    public required long StartTimestamp { get; set; }

    /// <summary>
    /// Time at which the Chat ended. Measured in seconds since the Unix epoch.
    /// </summary>
    [JsonPropertyName("end_timestamp")]
    public long? EndTimestamp { get; set; }

    /// <summary>
    /// Indicates the order in which the paginated results are presented, based on their creation date.
    ///
    /// It shows `ASC` for ascending order (chronological, with the oldest records first) or `DESC` for descending order (reverse-chronological, with the newest records first). This value corresponds to the `ascending_order` query parameter used in the request.
    /// </summary>
    [JsonPropertyName("pagination_direction")]
    public required ReturnChatPagedEventsPaginationDirection PaginationDirection { get; set; }

    /// <summary>
    /// List of Chat Events for the specified `page_number` and `page_size`.
    /// </summary>
    [JsonPropertyName("events_page")]
    public IEnumerable<ReturnChatEvent> EventsPage { get; set; } = new List<ReturnChatEvent>();

    /// <summary>
    /// Stringified JSON with additional metadata about the chat.
    /// </summary>
    [JsonPropertyName("metadata")]
    public string? Metadata { get; set; }

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

    [JsonPropertyName("config")]
    public ReturnConfigSpec? Config { get; set; }

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
