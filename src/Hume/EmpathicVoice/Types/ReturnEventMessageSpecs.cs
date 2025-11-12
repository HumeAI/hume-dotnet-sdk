using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// Collection of event messages returned by the server.
///
/// Event messages are sent by the server when specific events occur during a chat session. These messages are used to configure behaviors for EVI, such as controlling how EVI starts a new conversation.
/// </summary>
[Serializable]
public record ReturnEventMessageSpecs : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Specifies the message EVI provides when the chat is about to be disconnected due to a user inactivity timeout, such as a message mentioning a lack of user input for a period of time.
    ///
    /// Enabling an inactivity message allows developers to use this message event for "checking in" with the user if they are not responding to see if they are still active.
    ///
    /// If the user does not respond in the number of seconds specified in the `inactivity_timeout` field, then EVI will say the message and the user has 15 seconds to respond. If they respond in time, the conversation will continue; if not, the conversation will end.
    ///
    /// However, if the inactivity message is not enabled, then reaching the inactivity timeout will immediately end the connection.
    /// </summary>
    [JsonPropertyName("on_inactivity_timeout")]
    public ReturnEventMessageSpec? OnInactivityTimeout { get; set; }

    /// <summary>
    /// Specifies the message EVI provides when the chat is disconnected due to reaching the maximum chat duration, such as a message mentioning the time limit for the chat has been reached.
    /// </summary>
    [JsonPropertyName("on_max_duration_timeout")]
    public ReturnEventMessageSpec? OnMaxDurationTimeout { get; set; }

    /// <summary>
    /// Specifies the initial message EVI provides when a new chat is started, such as a greeting or welcome message.
    /// </summary>
    [JsonPropertyName("on_new_chat")]
    public ReturnEventMessageSpec? OnNewChat { get; set; }

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
