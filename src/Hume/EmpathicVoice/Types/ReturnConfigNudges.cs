using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// Configures nudges, brief audio prompts that can guide conversations when users pause or need encouragement to continue speaking. Nudges help create more natural, flowing interactions by providing gentle conversational cues.
/// </summary>
[Serializable]
public record ReturnConfigNudges : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// If true, EVI will 'nudge' the user to speak after a determined interval of silence.
    /// </summary>
    [JsonPropertyName("enabled")]
    public required bool Enabled { get; set; }

    /// <summary>
    /// The interval of inactivity (in seconds) before a nudge is triggered.
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
