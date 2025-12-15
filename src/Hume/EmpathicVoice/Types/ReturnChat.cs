using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// A description of chat and its status
/// </summary>
[Serializable]
public record ReturnChat : IJsonOnDeserialized
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
    /// The total number of events currently in this chat.
    /// </summary>
    [JsonPropertyName("event_count")]
    public long? EventCount { get; set; }

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
    /// The timestamp when the chat started, formatted as a Unix epoch milliseconds.
    /// </summary>
    [JsonPropertyName("start_timestamp")]
    public required long StartTimestamp { get; set; }

    [JsonPropertyName("status")]
    public required ReturnChatStatus Status { get; set; }

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
