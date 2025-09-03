using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Batch;

/// <summary>
/// The Speech Prosody model analyzes the intonation, stress, and rhythm of spoken word.
///
/// Recommended input file types: `.wav`, `.mp3`, `.mp4`
/// </summary>
[Serializable]
public record Prosody : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("granularity")]
    public Granularity? Granularity { get; set; }

    [JsonPropertyName("window")]
    public Window? Window { get; set; }

    /// <summary>
    /// Whether to return identifiers for speakers over time. If `true`, unique identifiers will be assigned to spoken words to differentiate different speakers. If `false`, all speakers will be tagged with an `unknown` ID.
    /// </summary>
    [JsonPropertyName("identify_speakers")]
    public bool? IdentifySpeakers { get; set; }

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
