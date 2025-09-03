using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// A specific nudge configuration returned from the server
/// </summary>
[Serializable]
public record ReturnNudgeSpec : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// EVI will nudge user after inactivity
    /// </summary>
    [JsonPropertyName("enabled")]
    public required bool Enabled { get; set; }

    /// <summary>
    /// Time interval in seconds after which the nudge will be sent.
    /// </summary>
    [JsonPropertyName("interval_secs")]
    public int? IntervalSecs { get; set; }

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
