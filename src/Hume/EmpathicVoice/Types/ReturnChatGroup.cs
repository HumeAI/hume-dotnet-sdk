using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// A description of chat_group and its status
/// </summary>
[Serializable]
public record ReturnChatGroup : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("active")]
    public bool? Active { get; set; }

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
    /// The chat_id of the most recent chat in this chat group. Formatted as a UUID.
    /// </summary>
    [JsonPropertyName("most_recent_chat_id")]
    public string? MostRecentChatId { get; set; }

    [JsonPropertyName("most_recent_config")]
    public ReturnConfigSpec? MostRecentConfig { get; set; }

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
