using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

/// <summary>
/// The Config associated with this Chat.
/// </summary>
[Serializable]
public record ReturnConfigSpec : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Identifier for a Config. Formatted as a UUID.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// Version number for a Config.
    ///
    /// Configs, Prompts, Custom Voices, and Tools are versioned. This versioning system supports iterative development, allowing you to progressively refine configurations and revert to previous versions if needed.
    ///
    /// Version numbers are integer values representing different iterations of the Config. Each update to the Config increments its version number.
    /// </summary>
    [JsonPropertyName("version")]
    public int? Version { get; set; }

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
