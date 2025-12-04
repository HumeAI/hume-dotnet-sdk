using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// **Indicates a disruption in the WebSocket connection**, such as an unexpected disconnection, protocol error, or data transmission issue.
///
/// Contains an error code identifying the type of error encountered, a detailed description of the error, and a short, human-readable identifier and description (slug) for the error.
/// </summary>
[Serializable]
public record WebSocketError : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Error code. Identifies the type of error encountered.
    /// </summary>
    [JsonPropertyName("code")]
    public required string Code { get; set; }

    /// <summary>
    /// Used to manage conversational state, correlate frontend and backend data, and persist conversations across EVI sessions.
    /// </summary>
    [JsonPropertyName("custom_session_id")]
    public string? CustomSessionId { get; set; }

    /// <summary>
    /// Detailed description of the error.
    /// </summary>
    [JsonPropertyName("message")]
    public required string Message { get; set; }

    /// <summary>
    /// ID of the initiating request.
    /// </summary>
    [JsonPropertyName("request_id")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Short, human-readable identifier and description for the error. See a complete list of error slugs on the [Errors page](/docs/resources/errors).
    /// </summary>
    [JsonPropertyName("slug")]
    public required string Slug { get; set; }

    /// <summary>
    /// The type of message sent through the socket; for a Web Socket Error message, this must be `error`.
    ///
    /// This message indicates a disruption in the WebSocket connection, such as an unexpected disconnection, protocol error, or data transmission issue.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type
    {
        get => "error";
        set => value.Assert(value == "error", "'Type' must be " + "error");
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
