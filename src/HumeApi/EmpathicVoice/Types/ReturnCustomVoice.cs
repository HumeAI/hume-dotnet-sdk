using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

/// <summary>
/// A Custom Voice specification associated with this Config.
/// </summary>
[Serializable]
public record ReturnCustomVoice : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Identifier for a Custom Voice. Formatted as a UUID.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// Version number for a Custom Voice.
    ///
    /// Custom Voices, Prompts, Configs, and Tools are versioned. This versioning system supports iterative development, allowing you to progressively refine configurations and revert to previous versions if needed.
    ///
    /// Version numbers are integer values representing different iterations of the Custom Voice. Each update to the Custom Voice increments its version number.
    /// </summary>
    [JsonPropertyName("version")]
    public required int Version { get; set; }

    /// <summary>
    /// The name of the Custom Voice. Maximum length of 75 characters.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Time at which the Custom Voice was created. Measured in seconds since the Unix epoch.
    /// </summary>
    [JsonPropertyName("created_on")]
    public required long CreatedOn { get; set; }

    /// <summary>
    /// Time at which the Custom Voice was last modified. Measured in seconds since the Unix epoch.
    /// </summary>
    [JsonPropertyName("modified_on")]
    public required long ModifiedOn { get; set; }

    /// <summary>
    /// The base voice used to create the Custom Voice.
    /// </summary>
    [JsonPropertyName("base_voice")]
    public required ReturnCustomVoiceBaseVoice BaseVoice { get; set; }

    /// <summary>
    /// The name of the parameter model used to define which attributes are used by the `parameters` field. Currently, only `20241004-11parameter` is supported as the parameter model.
    /// </summary>
    [JsonPropertyName("parameter_model")]
    public string ParameterModel { get; set; } = "20241004-11parameter";

    /// <summary>
    /// The specified attributes of a Custom Voice. If a parameter's value is `0` (default), it will not be included in the response.
    /// </summary>
    [JsonPropertyName("parameters")]
    public required ReturnCustomVoiceParameters Parameters { get; set; }

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
