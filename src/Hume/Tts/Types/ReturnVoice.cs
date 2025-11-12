using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.Tts;

/// <summary>
/// An Octave voice available for text-to-speech
/// </summary>
[Serializable]
public record ReturnVoice : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("compatible_octave_models")]
    public IEnumerable<string>? CompatibleOctaveModels { get; set; }

    /// <summary>
    /// ID of the voice in the `Voice Library`.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Name of the voice in the `Voice Library`.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// The provider associated with the created voice.
    ///
    /// Voices created through this endpoint will always have the provider set to `CUSTOM_VOICE`, indicating a custom voice stored in your account.
    /// </summary>
    [JsonPropertyName("provider")]
    public VoiceProvider? Provider { get; set; }

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
