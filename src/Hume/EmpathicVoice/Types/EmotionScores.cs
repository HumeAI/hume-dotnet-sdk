using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

[Serializable]
public record EmotionScores : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("Admiration")]
    public required double Admiration { get; set; }

    [JsonPropertyName("Adoration")]
    public required double Adoration { get; set; }

    [JsonPropertyName("Aesthetic Appreciation")]
    public required double AestheticAppreciation { get; set; }

    [JsonPropertyName("Amusement")]
    public required double Amusement { get; set; }

    [JsonPropertyName("Anger")]
    public required double Anger { get; set; }

    [JsonPropertyName("Anxiety")]
    public required double Anxiety { get; set; }

    [JsonPropertyName("Awe")]
    public required double Awe { get; set; }

    [JsonPropertyName("Awkwardness")]
    public required double Awkwardness { get; set; }

    [JsonPropertyName("Boredom")]
    public required double Boredom { get; set; }

    [JsonPropertyName("Calmness")]
    public required double Calmness { get; set; }

    [JsonPropertyName("Concentration")]
    public required double Concentration { get; set; }

    [JsonPropertyName("Confusion")]
    public required double Confusion { get; set; }

    [JsonPropertyName("Contemplation")]
    public required double Contemplation { get; set; }

    [JsonPropertyName("Contempt")]
    public required double Contempt { get; set; }

    [JsonPropertyName("Contentment")]
    public required double Contentment { get; set; }

    [JsonPropertyName("Craving")]
    public required double Craving { get; set; }

    [JsonPropertyName("Desire")]
    public required double Desire { get; set; }

    [JsonPropertyName("Determination")]
    public required double Determination { get; set; }

    [JsonPropertyName("Disappointment")]
    public required double Disappointment { get; set; }

    [JsonPropertyName("Disgust")]
    public required double Disgust { get; set; }

    [JsonPropertyName("Distress")]
    public required double Distress { get; set; }

    [JsonPropertyName("Doubt")]
    public required double Doubt { get; set; }

    [JsonPropertyName("Ecstasy")]
    public required double Ecstasy { get; set; }

    [JsonPropertyName("Embarrassment")]
    public required double Embarrassment { get; set; }

    [JsonPropertyName("Empathic Pain")]
    public required double EmpathicPain { get; set; }

    [JsonPropertyName("Entrancement")]
    public required double Entrancement { get; set; }

    [JsonPropertyName("Envy")]
    public required double Envy { get; set; }

    [JsonPropertyName("Excitement")]
    public required double Excitement { get; set; }

    [JsonPropertyName("Fear")]
    public required double Fear { get; set; }

    [JsonPropertyName("Guilt")]
    public required double Guilt { get; set; }

    [JsonPropertyName("Horror")]
    public required double Horror { get; set; }

    [JsonPropertyName("Interest")]
    public required double Interest { get; set; }

    [JsonPropertyName("Joy")]
    public required double Joy { get; set; }

    [JsonPropertyName("Love")]
    public required double Love { get; set; }

    [JsonPropertyName("Nostalgia")]
    public required double Nostalgia { get; set; }

    [JsonPropertyName("Pain")]
    public required double Pain { get; set; }

    [JsonPropertyName("Pride")]
    public required double Pride { get; set; }

    [JsonPropertyName("Realization")]
    public required double Realization { get; set; }

    [JsonPropertyName("Relief")]
    public required double Relief { get; set; }

    [JsonPropertyName("Romance")]
    public required double Romance { get; set; }

    [JsonPropertyName("Sadness")]
    public required double Sadness { get; set; }

    [JsonPropertyName("Satisfaction")]
    public required double Satisfaction { get; set; }

    [JsonPropertyName("Shame")]
    public required double Shame { get; set; }

    [JsonPropertyName("Surprise (negative)")]
    public required double SurpriseNegative { get; set; }

    [JsonPropertyName("Surprise (positive)")]
    public required double SurprisePositive { get; set; }

    [JsonPropertyName("Sympathy")]
    public required double Sympathy { get; set; }

    [JsonPropertyName("Tiredness")]
    public required double Tiredness { get; set; }

    [JsonPropertyName("Triumph")]
    public required double Triumph { get; set; }

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
