using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Batch;

[Serializable]
public record LanguagePrediction : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// A segment of text (like a word or a sentence).
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    [JsonPropertyName("position")]
    public required PositionInterval Position { get; set; }

    [JsonPropertyName("time")]
    public TimeInterval? Time { get; set; }

    /// <summary>
    /// Value between `0.0` and `1.0` that indicates our transcription model's relative confidence in this text.
    /// </summary>
    [JsonPropertyName("confidence")]
    public double? Confidence { get; set; }

    /// <summary>
    /// Value between `0.0` and `1.0` that indicates our transcription model's relative confidence that this text was spoken by this speaker.
    /// </summary>
    [JsonPropertyName("speaker_confidence")]
    public double? SpeakerConfidence { get; set; }

    /// <summary>
    /// A high-dimensional embedding in emotion space.
    /// </summary>
    [JsonPropertyName("emotions")]
    public IEnumerable<EmotionScore> Emotions { get; set; } = new List<EmotionScore>();

    /// <summary>
    /// Sentiment predictions returned as a distribution. This model predicts the probability that a given text could be interpreted as having each sentiment level from `1` (negative) to `9` (positive).
    ///
    /// Compared to returning one estimate of sentiment, this enables a more nuanced analysis of a text's meaning. For example, a text with very neutral sentiment would have an average rating of `5`. But also a text that could be interpreted as having very positive sentiment or very negative sentiment would also have an average rating of `5`. The average sentiment is less informative than the distribution over sentiment, so this API returns a value for each sentiment level.
    /// </summary>
    [JsonPropertyName("sentiment")]
    public IEnumerable<SentimentScore>? Sentiment { get; set; }

    /// <summary>
    /// Toxicity predictions returned as probabilities that the text can be classified into the following categories: `toxic`, `severe_toxic`, `obscene`, `threat`, `insult`, and `identity_hate`.
    /// </summary>
    [JsonPropertyName("toxicity")]
    public IEnumerable<ToxicityScore>? Toxicity { get; set; }

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
