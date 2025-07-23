using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

[Serializable]
public record ProsodyInference : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The confidence scores for 48 emotions within the detected expression of an audio sample.
    ///
    /// Scores typically range from 0 to 1, with higher values indicating a stronger confidence level in the measured attribute.
    ///
    /// See our guide on [interpreting expression measurement results](/docs/expression-measurement/faq#how-do-i-interpret-my-results) to learn more.
    /// </summary>
    [JsonPropertyName("scores")]
    public required EmotionScores Scores { get; set; }

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
