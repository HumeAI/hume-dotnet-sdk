using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

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

    /// <summary>
    /// Specifies the duration of user inactivity (in seconds) after which the EVI WebSocket connection will be automatically disconnected. Default is 600 seconds (10 minutes).
    ///
    /// Accepts a minimum value of 30 seconds and a maximum value of 1,800 seconds.
    /// </summary>
    [JsonPropertyName("inactivity")]
    public PostedTimeoutSpecsInactivity? Inactivity { get; set; }

    /// <summary>
    /// Specifies the maximum allowed duration (in seconds) for an EVI WebSocket connection before it is automatically disconnected. Default is 1,800 seconds (30 minutes).
    ///
    /// Accepts a minimum value of 30 seconds and a maximum value of 1,800 seconds.
    /// </summary>
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
