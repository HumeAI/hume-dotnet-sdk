using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// **Resume responses from EVI.** Chat history sent while paused will now be sent.
///
/// Upon resuming, if any audio input was sent during the pause, EVI will retain context from all messages sent but only respond to the last user message. See our [Pause Response Guide](/docs/speech-to-speech-evi/features/pause-responses) for further details.
/// </summary>
[Serializable]
public record ResumeAssistantMessage : IJsonOnDeserialized
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
    /// The type of message sent through the socket; must be `resume_assistant_message` for our server to correctly identify and process it as a Resume Assistant message.
    ///
    /// Upon resuming, if any audio input was sent during the pause, EVI will retain context from all messages sent but only respond to the last user message. (e.g., If you ask EVI two questions while paused and then send a `resume_assistant_message`, EVI will respond to the second question and have added the first question to its conversation context.)
    /// </summary>
    [JsonPropertyName("type")]
    public string Type
    {
        get => "resume_assistant_message";
        set =>
            value.Assert(
                value == "resume_assistant_message",
                "'Type' must be " + "resume_assistant_message"
            );
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
