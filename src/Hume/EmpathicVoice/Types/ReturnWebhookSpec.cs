using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// Collection of webhook URL endpoints to be returned from the server
/// </summary>
[Serializable]
public record ReturnWebhookSpec : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Events this URL is subscribed to
    /// </summary>
    [JsonPropertyName("events")]
    public IEnumerable<ReturnWebhookEventType> Events { get; set; } =
        new List<ReturnWebhookEventType>();

    /// <summary>
    /// Webhook URL to send the event updates to
    /// </summary>
    [JsonPropertyName("url")]
    public required string Url { get; set; }

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
