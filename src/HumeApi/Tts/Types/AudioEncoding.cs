using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.Tts;

/// <summary>
/// Encoding information about the generated audio, including the `format` and `sample_rate`.
/// </summary>
[Serializable]
public record AudioEncoding : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Format for the output audio.
    /// </summary>
    [JsonPropertyName("format")]
    public required AudioFormatType Format { get; set; }

    /// <summary>
    /// The sample rate (`Hz`) of the generated audio. The default sample rate is `48000 Hz`.
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
