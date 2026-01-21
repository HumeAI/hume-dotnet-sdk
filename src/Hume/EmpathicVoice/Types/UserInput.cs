using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// **User text to insert into the conversation.** Text sent through a User Input message is treated as the user's speech to EVI. EVI processes this input and provides a corresponding response.
///
/// Expression measurement results are not available for User Input messages, as the prosody model relies on audio input and cannot process text alone.
/// </summary>
[Serializable]
public record UserInput : IJsonOnDeserialized
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
    /// User text to insert into the conversation. Text sent through a User Input message is treated as the user's speech to EVI. EVI processes this input and provides a corresponding response.
    ///
    /// Expression measurement results are not available for User Input messages, as the prosody model relies on audio input and cannot process text alone.
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <summary>
    /// The type of message sent through the socket; must be `user_input` for our server to correctly identify and process it as a User Input message.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type
    {
        get => "user_input";
        set =>
            value.Assert(value == "user_input", string.Format("'Type' must be {0}", "user_input"));
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
