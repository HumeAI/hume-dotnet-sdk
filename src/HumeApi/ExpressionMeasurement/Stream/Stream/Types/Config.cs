using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Stream;

/// <summary>
/// Configuration used to specify which models should be used and with what settings.
/// </summary>
[Serializable]
public record Config : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Configuration for the vocal burst emotion model.
    ///
    /// Note: Model configuration is not currently available in streaming.
    ///
    /// Please use the default configuration by passing an empty object `{}`.
    /// </summary>
    [JsonPropertyName("burst")]
    public Dictionary<string, object?>? Burst { get; set; }

    /// <summary>
    /// Configuration for the facial expression emotion model.
    ///
    /// Note: Using the `reset_stream` parameter does not have any effect on face identification. A single face identifier cache is maintained over a full session whether `reset_stream` is used or not.
    /// </summary>
    [JsonPropertyName("face")]
    public StreamFace? Face { get; set; }

    /// <summary>
    /// Configuration for the facemesh emotion model.
    ///
    /// Note: Model configuration is not currently available in streaming.
    ///
    /// Please use the default configuration by passing an empty object `{}`.
    /// </summary>
    [JsonPropertyName("facemesh")]
    public Dictionary<string, object?>? Facemesh { get; set; }

    /// <summary>
    /// Configuration for the language emotion model.
    /// </summary>
    [JsonPropertyName("language")]
    public StreamLanguage? Language { get; set; }

    /// <summary>
    /// Configuration for the speech prosody emotion model.
    ///
    /// Note: Model configuration is not currently available in streaming.
    ///
    /// Please use the default configuration by passing an empty object `{}`.
    /// </summary>
    [JsonPropertyName("prosody")]
    public Dictionary<string, object?>? Prosody { get; set; }

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
