using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// When provided, the output is an assistant message.
/// </summary>
[Serializable]
public record AssistantMessage : IJsonOnDeserialized
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
    /// Indicates if this message was inserted into the conversation as text from an [Assistant Input message](/reference/speech-to-speech-evi/chat#send.AssistantInput.text).
    /// </summary>
    [JsonPropertyName("from_text")]
    public required bool FromText { get; set; }

    /// <summary>
    /// ID of the assistant message. Allows the Assistant Message to be tracked and referenced.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

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
    /// The type of message sent through the socket; for an Assistant Message, this must be `assistant_message`.
    ///
    /// This message contains both a transcript of the assistant's response and the expression measurement predictions of the assistant's audio output.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type
    {
        get => "assistant_message";
        set => value.Assert(value == "assistant_message", "'Type' must be " + "assistant_message");
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
