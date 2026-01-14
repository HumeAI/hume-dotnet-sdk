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
    /// Events this URL is subscribed to
    /// </summary>
    [JsonPropertyName("events")]
    public IEnumerable<PostedWebhookEventType> Events { get; set; } =
        new List<PostedWebhookEventType>();

    /// <summary>
    /// URL to send the webhook to
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
