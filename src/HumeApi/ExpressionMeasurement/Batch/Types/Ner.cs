using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Batch;

/// <summary>
/// The NER (Named-entity Recognition) model identifies real-world objects and concepts in passages of text. This also supports audio and video files by transcribing and then directly analyzing the transcribed text.
///
/// Recommended input filetypes: `.txt`, `.mp3`, `.wav`, `.mp4`
/// </summary>
[Serializable]
public record Ner : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

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
