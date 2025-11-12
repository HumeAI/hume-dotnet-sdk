using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// List of chat audio reconstructions returned for the specified page number and page size.
/// </summary>
[Serializable]
public record ReturnChatAudioReconstruction : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Name of the chat audio reconstruction file.
    /// </summary>
    [JsonPropertyName("filename")]
    public string? Filename { get; set; }

    /// <summary>
    /// Identifier for the chat. Formatted as a UUID.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// The timestamp of the most recent status change for this audio reconstruction, formatted milliseconds since the Unix epoch.
    /// </summary>
    [JsonPropertyName("modified_at")]
    public long? ModifiedAt { get; set; }

    /// <summary>
    /// Signed URL used to download the chat audio reconstruction file.
    /// </summary>
    [JsonPropertyName("signed_audio_url")]
    public string? SignedAudioUrl { get; set; }

    /// <summary>
    /// The timestamp when the signed URL will expire, formatted as a Unix epoch milliseconds.
    /// </summary>
    [JsonPropertyName("signed_url_expiration_timestamp_millis")]
    public long? SignedUrlExpirationTimestampMillis { get; set; }

    /// <summary>
    /// Indicates the current state of the audio reconstruction job. There are five possible statuses:
    ///
    /// - `QUEUED`: The reconstruction job is waiting to be processed.
    ///
    /// - `IN_PROGRESS`: The reconstruction is currently being processed.
    ///
    /// - `COMPLETE`: The audio reconstruction is finished and ready for download.
    ///
    /// - `ERROR`: An error occurred during the reconstruction process.
    ///
    /// - `CANCELED`: The reconstruction job has been canceled.
    /// </summary>
    [JsonPropertyName("status")]
    public required ReturnChatAudioReconstructionStatus Status { get; set; }

    /// <summary>
    /// Identifier for the user that owns this chat. Formatted as a UUID.
    /// </summary>
    [JsonPropertyName("user_id")]
    public required string UserId { get; set; }

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
