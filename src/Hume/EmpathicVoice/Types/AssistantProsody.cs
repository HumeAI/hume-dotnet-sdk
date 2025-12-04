using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// **Expression measurement predictions of the assistant's audio output.** Contains inference model results including prosody scores for 48 emotions within the detected expression of the assistant's audio sample.
/// </summary>
[Serializable]
public record AssistantProsody : IJsonOnDeserialized
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
    /// Unique identifier for the segment.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Inference model results.
    /// </summary>
    [JsonPropertyName("models")]
    public required Inference Models { get; set; }

    /// <summary>
    /// The type of message sent through the socket; for an Assistant Prosody message, this must be `assistant_PROSODY`.
    ///
    /// This message the expression measurement predictions of the assistant's audio output.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type
    {
        get => "assistant_prosody";
        set => value.Assert(value == "assistant_prosody", "'Type' must be " + "assistant_prosody");
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
