using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

/// <summary>
/// Specifies the duration of user inactivity (in seconds) after which the EVI WebSocket connection will be automatically disconnected. Default is 600 seconds (10 minutes).
///
/// Accepts a minimum value of 30 seconds and a maximum value of 1,800 seconds.
/// </summary>
[Serializable]
public record PostedTimeoutSpecsInactivity : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Boolean indicating if this timeout is enabled.
    ///
    /// If set to false, EVI will not timeout due to a specified duration of user inactivity being reached. However, the conversation will eventually disconnect after 1,800 seconds (30 minutes), which is the maximum WebSocket duration limit for EVI.
    /// </summary>
    [JsonPropertyName("enabled")]
    public required bool Enabled { get; set; }

    /// <summary>
    /// Duration in seconds for the timeout (e.g. 600 seconds represents 10 minutes).
    /// </summary>
    [JsonPropertyName("duration_secs")]
    public int? DurationSecs { get; set; }

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
