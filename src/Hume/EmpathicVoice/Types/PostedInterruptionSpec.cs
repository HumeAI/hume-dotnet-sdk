using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// An interruption specification posted to the server
/// </summary>
[Serializable]
public record PostedInterruptionSpec : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// How long the user must speak while the agent is outputting audio before the agent stops and yields the floor. Lower values make the agent more responsive to the user speaking up, but increase the chance of noise or backchannels stopping the agent unnecessarily. Higher values make the agent harder to interrupt, allowing it to finish more of its response before yielding. Accepts values between 50 and 2000 milliseconds.
    /// </summary>
    [JsonPropertyName("min_interruption_ms")]
    public int? MinInterruptionMs { get; set; }

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
