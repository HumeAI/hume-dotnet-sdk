using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// When provided, the input is spoken by EVI.
/// </summary>
[Serializable]
public record AssistantInput : IJsonOnDeserialized
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
    /// Assistant text to synthesize into spoken audio and insert into the conversation.
    ///
    /// EVI uses this text to generate spoken audio using our proprietary expressive text-to-speech model. Our model adds appropriate emotional inflections and tones to the text based on the user's expressions and the context of the conversation. The synthesized audio is streamed back to the user as an [Assistant Message](/reference/speech-to-speech-evi/chat#receive.AssistantMessage).
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <summary>
    /// The type of message sent through the socket; must be `assistant_input` for our server to correctly identify and process it as an Assistant Input message.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = "assistant_input";

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
