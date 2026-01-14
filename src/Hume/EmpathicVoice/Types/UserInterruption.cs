using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// When provided, the output is an interruption.
/// </summary>
[Serializable]
public record UserInterruption : IJsonOnDeserialized
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
    /// Unix timestamp of the detected user interruption.
    /// </summary>
    [JsonPropertyName("time")]
    public required int Time { get; set; }

    /// <summary>
    /// The type of message sent through the socket; for a User Interruption message, this must be `user_interruption`.
    ///
    /// This message indicates the user has interrupted the assistant's response. EVI detects the interruption in real-time and sends this message to signal the interruption event. This message allows the system to stop the current audio playback, clear the audio queue, and prepare to handle new user input.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type
    {
        get => "user_interruption";
        set => value.Assert(value == "user_interruption", "'Type' must be " + "user_interruption");
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
