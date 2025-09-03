using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// URL and settings for a specific webhook to be posted to the server
/// </summary>
[Serializable]
public record PostedWebhookSpec : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The URL where event payloads will be sent. This must be a valid https URL to ensure secure communication. The server at this URL must accept POST requests with a JSON payload.
    /// </summary>
    [JsonPropertyName("url")]
    public required string Url { get; set; }

    /// <summary>
    /// The list of events the specified URL is subscribed to.
    ///
    /// See our [webhooks guide](/docs/speech-to-speech-evi/configuration/build-a-configuration#supported-events) for more information on supported events.
    /// </summary>
    [JsonPropertyName("events")]
    public IEnumerable<PostedWebhookEventType> Events { get; set; } =
        new List<PostedWebhookEventType>();

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
