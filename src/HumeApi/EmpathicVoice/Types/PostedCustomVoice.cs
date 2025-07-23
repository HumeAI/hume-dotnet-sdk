using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

/// <summary>
/// A Custom Voice specification to be associated with this Config.
///
/// If a Custom Voice specification is not provided then the [name](/reference/empathic-voice-interface-evi/configs/create-config#request.body.voice.name) of a base voice or previously created Custom Voice must be provided.
///
///  See our [Voices guide](/docs/empathic-voice-interface-evi/configuration/voices) for a tutorial on how to craft a Custom Voice.
/// </summary>
[Serializable]
public record PostedCustomVoice : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The name of the Custom Voice. Maximum length of 75 characters. Will be converted to all-uppercase. (e.g., "sample voice" becomes "SAMPLE VOICE")
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Specifies the base voice used to create the Custom Voice.
    /// </summary>
    [JsonPropertyName("base_voice")]
    public required PostedCustomVoiceBaseVoice BaseVoice { get; set; }

    /// <summary>
    /// The name of the parameter model used to define which attributes are used by the `parameters` field. Currently, only `20241004-11parameter` is supported as the parameter model.
    /// </summary>
    [JsonPropertyName("parameter_model")]
    public string ParameterModel { get; set; } = "20241004-11parameter";

    /// <summary>
    /// The specified attributes of a Custom Voice.
    ///
    /// If no parameters are specified then all attributes will be set to their defaults, meaning no modfications will be made to the base voice.
    /// </summary>
    [JsonPropertyName("parameters")]
    public PostedCustomVoiceParameters? Parameters { get; set; }

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
