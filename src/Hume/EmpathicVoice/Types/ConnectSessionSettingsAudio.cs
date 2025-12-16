using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// Configuration details for the audio input used during the session. Ensures the audio is being correctly set up for processing.
///
/// This optional field is only required when the audio input is encoded in PCM Linear 16 (16-bit, little-endian, signed PCM WAV data). For detailed instructions on how to configure session settings for PCM Linear 16 audio, please refer to the [Session Settings section](/docs/empathic-voice-interface-evi/configuration#session-settings) on the EVI Configuration page.
/// </summary>
[Serializable]
public record ConnectSessionSettingsAudio : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Number of audio channels.
    /// </summary>
    [JsonPropertyName("channels")]
    public int? Channels { get; set; }

    /// <summary>
    /// Encoding format of the audio input, such as `linear16`.
    /// </summary>
    [JsonPropertyName("encoding")]
    public string? Encoding { get; set; }

    /// <summary>
    /// Audio sample rate. Number of samples per second in the audio input, measured in Hertz.
    /// </summary>
    [JsonPropertyName("sample_rate")]
    public int? SampleRate { get; set; }

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
