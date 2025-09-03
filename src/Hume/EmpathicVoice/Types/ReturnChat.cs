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
    public required ReturnChatStatus Status { get; set; }

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
    /// The total number of events currently in this chat.
    /// </summary>
    [JsonPropertyName("event_count")]
    public long? EventCount { get; set; }

    /// <summary>
    /// Stringified JSON with additional metadata about the chat.
    /// </summary>
    [JsonPropertyName("metadata")]
    public string? Metadata { get; set; }

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
