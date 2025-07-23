using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Batch;

/// <summary>
/// Transcription metadata for your media file.
/// </summary>
[Serializable]
public record TranscriptionMetadata : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Value between `0.0` and `1.0` indicating our transcription model's relative confidence in the transcription of your media file.
    /// </summary>
    [JsonPropertyName("confidence")]
    public required double Confidence { get; set; }

    [JsonPropertyName("detected_language")]
    public Bcp47Tag? DetectedLanguage { get; set; }

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
