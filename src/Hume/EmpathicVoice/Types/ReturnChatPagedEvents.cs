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
    /// Identifier for the chat group. Any chat resumed from this chat will have the same chat_group_id. Formatted as a UUID.
    /// </summary>
    [JsonPropertyName("chat_group_id")]
    public required string ChatGroupId { get; set; }

    [JsonPropertyName("config")]
    public ReturnConfigSpec? Config { get; set; }

    /// <summary>
    /// The timestamp when the chat ended, formatted as a Unix epoch milliseconds.
    /// </summary>
    [JsonPropertyName("end_timestamp")]
    public long? EndTimestamp { get; set; }

    /// <summary>
    /// List of chat events with the specified page number and page size.
    /// </summary>
    [JsonPropertyName("events_page")]
    public IEnumerable<ReturnChatEvent> EventsPage { get; set; } = new List<ReturnChatEvent>();

    /// <summary>
    /// Identifier for a chat. Formatted as a UUID.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// Stringified JSON with additional metadata about the chat.
    /// </summary>
    [JsonPropertyName("metadata")]
    public string? Metadata { get; set; }

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
    public required ReturnChatPagedEventsPaginationDirection PaginationDirection { get; set; }

    /// <summary>
    /// The timestamp when the chat started, formatted as a Unix epoch milliseconds.
    /// </summary>
    [JsonPropertyName("start_timestamp")]
    public required long StartTimestamp { get; set; }

    [JsonPropertyName("status")]
    public required ReturnChatPagedEventsStatus Status { get; set; }

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
