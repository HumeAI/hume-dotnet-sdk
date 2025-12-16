using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// When provided, the output is a user message.
/// </summary>
[Serializable]
public record UserMessage : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Used to manage conversational state, correlate frontend and backend data, and persist conversations across EVI sessions.
    /// </summary>
    [JsonPropertyName("custom_session_id")]
    public string? CustomSessionId { get; set; }

    /// <summary>
    /// Indicates if this message was inserted into the conversation as text from a [User Input](/reference/empathic-voice-interface-evi/chat/chat#send.User%20Input.text) message.
    /// </summary>
    [JsonPropertyName("from_text")]
    public required bool FromText { get; set; }

    /// <summary>
    /// Indicates if this message contains an immediate and unfinalized transcript of the user's audio input. If it does, words may be repeated across successive UserMessage messages as our transcription model becomes more confident about what was said with additional context. Interim messages are useful to detect if the user is interrupting during audio playback on the client. Even without a finalized transcription, along with `UserInterrupt` messages, interim `UserMessages` are useful for detecting if the user is interrupting during audio playback on the client, signaling to stop playback in your application.
    /// </summary>
    [JsonPropertyName("interim")]
    public required bool Interim { get; set; }

    /// <summary>
    /// Detected language of the message text.
    /// </summary>
    [JsonPropertyName("language")]
    public string? Language { get; set; }

    /// <summary>
    /// Transcript of the message.
    /// </summary>
    [JsonPropertyName("message")]
    public required ChatMessage Message { get; set; }

    /// <summary>
    /// Inference model results.
    /// </summary>
    [JsonPropertyName("models")]
    public required Inference Models { get; set; }

    /// <summary>
    /// Start and End time of user message.
    /// </summary>
    [JsonPropertyName("time")]
    public required MillisecondInterval Time { get; set; }

    [JsonPropertyName("type")]
    public string Type
    {
        get => "user_message";
        set => value.Assert(value == "user_message", "'Type' must be " + "user_message");
    }

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
