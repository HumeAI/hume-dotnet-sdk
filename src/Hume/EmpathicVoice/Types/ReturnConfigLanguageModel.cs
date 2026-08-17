using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// The supplemental language model associated with this Config.
///
/// This model is used to generate longer, more detailed responses from EVI. Choosing an appropriate supplemental language model for your use case is crucial for generating fast, high-quality responses from EVI.
/// </summary>
[Serializable]
public record ReturnConfigLanguageModel : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("model_provider")]
    public ModelProviderEnum? ModelProvider { get; set; }

    [JsonPropertyName("model_resource")]
    public LanguageModelType? ModelResource { get; set; }

    /// <summary>
    /// The model temperature, with values between 0 to 1 (inclusive).
    ///
    /// Controls the randomness of the LLM's output, with values closer to 0 yielding focused, deterministic responses and values closer to 1 producing more creative, diverse responses.
    /// </summary>
    [JsonPropertyName("temperature")]
    public float? Temperature { get; set; }

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
