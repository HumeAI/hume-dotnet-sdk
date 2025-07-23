using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Stream;

[Serializable]
public record StreamModelPredictionsLanguagePredictionsItem : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// A segment of text (like a word or a sentence).
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("position")]
    public TextPosition? Position { get; set; }

    [JsonPropertyName("emotions")]
    public IEnumerable<EmotionEmbeddingItem>? Emotions { get; set; }

    [JsonPropertyName("sentiment")]
    public IEnumerable<SentimentItem>? Sentiment { get; set; }

    [JsonPropertyName("toxicity")]
    public IEnumerable<ToxicityItem>? Toxicity { get; set; }

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
