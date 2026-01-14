using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[Serializable]
public record ChatsListChatsRequest
{
    /// <summary>
    /// Specifies the page number to retrieve, enabling pagination.
    ///
    /// This parameter uses zero-based indexing. For example, setting `page_number` to 0 retrieves the first page of results (items 0-9 if `page_size` is 10), setting `page_number` to 1 retrieves the second page (items 10-19), and so on. Defaults to 0, which retrieves the first page.
    /// </summary>
    [JsonIgnore]
    public int? PageNumber { get; set; }

    /// <summary>
    /// Specifies the maximum number of results to include per page, enabling pagination. The value must be between 1 and 100, inclusive.
    ///
    /// For example, if `page_size` is set to 10, each page will include up to 10 items. Defaults to 10.
    /// </summary>
    [JsonIgnore]
    public int? PageSize { get; set; }

    /// <summary>
    /// Boolean to indicate if the results should be paginated in chronological order or reverse-chronological order. Defaults to true.
    /// </summary>
    [JsonIgnore]
    public bool? AscendingOrder { get; set; }

    /// <summary>
    /// Filter to only include chats that used this config.
    /// </summary>
    [JsonIgnore]
    public string? ConfigId { get; set; }

    /// <summary>
    /// Chat status to apply to the chat. String from the ChatStatus enum.
    /// </summary>
    [JsonIgnore]
    public string? Status { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
