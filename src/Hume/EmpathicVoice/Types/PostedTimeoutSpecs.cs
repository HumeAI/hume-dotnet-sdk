using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// Collection of timeout specifications returned by the server.
///
/// Timeouts are sent by the server when specific time-based events occur during a chat session. These specifications set the inactivity timeout and the maximum duration an EVI WebSocket connection can stay open before it is automatically disconnected.
/// </summary>
[Serializable]
public record PostedTimeoutSpecs : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("inactivity")]
    public PostedTimeoutSpecsInactivity? Inactivity { get; set; }

    [JsonPropertyName("max_duration")]
    public PostedTimeoutSpecsMaxDuration? MaxDuration { get; set; }

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
