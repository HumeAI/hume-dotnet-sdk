using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

[Serializable]
public record AudioConfiguration : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Number of audio channels.
    /// </summary>
    [JsonPropertyName("channels")]
    public required int Channels { get; set; }

    /// <summary>
    /// Encoding format of the audio input, such as `linear16`.
    /// </summary>
    [JsonPropertyName("encoding")]
    public string Encoding { get; set; } = "linear16";

    /// <summary>
    /// Audio sample rate. Number of samples per second in the audio input, measured in Hertz.
    /// </summary>
    [JsonPropertyName("sample_rate")]
    public required int SampleRate { get; set; }

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
