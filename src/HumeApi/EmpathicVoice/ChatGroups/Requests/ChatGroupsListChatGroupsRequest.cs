using System.Text.Json.Serialization;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

[Serializable]
public record ChatGroupsListChatGroupsRequest
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
    /// Specifies the sorting order of the results based on their creation date. Set to true for ascending order (chronological, with the oldest records first) and false for descending order (reverse-chronological, with the newest records first). Defaults to true.
    /// </summary>
    [JsonIgnore]
    public bool? AscendingOrder { get; set; }

    /// <summary>
    /// The unique identifier for an EVI configuration.
    ///
    /// Filter Chat Groups to only include Chats that used this `config_id` in their most recent Chat.
    /// </summary>
    [JsonIgnore]
    public string? ConfigId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
