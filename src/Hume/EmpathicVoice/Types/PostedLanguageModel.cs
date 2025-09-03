using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// A LanguageModel to be posted to the server
/// </summary>
[Serializable]
public record PostedLanguageModel : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The provider of the supplemental language model.
    /// </summary>
    [JsonPropertyName("model_provider")]
    public ModelProviderEnum? ModelProvider { get; set; }

    /// <summary>
    /// String that specifies the language model to use with `model_provider`.
    /// </summary>
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
