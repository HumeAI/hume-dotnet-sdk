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
    /// The `chat_id` of the most recent Chat in this Chat Group. Formatted as a UUID.
    /// </summary>
    [JsonPropertyName("most_recent_chat_id")]
    public string? MostRecentChatId { get; set; }

    [JsonPropertyName("most_recent_config")]
    public ReturnConfigSpec? MostRecentConfig { get; set; }

    /// <summary>
    /// The total number of Chats in this Chat Group.
    /// </summary>
    [JsonPropertyName("num_chats")]
    public required int NumChats { get; set; }

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
