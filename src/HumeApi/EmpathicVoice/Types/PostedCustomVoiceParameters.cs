using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

/// <summary>
/// The specified attributes of a Custom Voice.
///
/// If no parameters are specified then all attributes will be set to their defaults, meaning no modfications will be made to the base voice.
/// </summary>
[Serializable]
public record PostedCustomVoiceParameters : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The perceived tonality of the voice, reflecting characteristics typically associated with masculinity and femininity.
    ///
    /// The default value is `0`, with a minimum of `-100` (more masculine) and a maximum of `100` (more feminine). A value of `0` leaves this parameter unchanged from the base voice.
    /// </summary>
    [JsonPropertyName("gender")]
    public int? Gender { get; set; }

    /// <summary>
    /// The perceived firmness of the voice, ranging between whiny and bold.
    ///
    /// The default value is `0`, with a minimum of `-100` (whiny) and a maximum of `100` (bold). A value of `0` leaves this parameter unchanged from the base voice.
    /// </summary>
    [JsonPropertyName("assertiveness")]
    public int? Assertiveness { get; set; }

    /// <summary>
    /// The perceived density of the voice, ranging between deflated and buoyant.
    ///
    /// The default value is `0`, with a minimum of `-100` (deflated) and a maximum of `100` (buoyant). A value of `0` leaves this parameter unchanged from the base voice.
    /// </summary>
    [JsonPropertyName("buoyancy")]
    public int? Buoyancy { get; set; }

    /// <summary>
    /// The perceived assuredness of the voice, ranging between shy and confident.
    ///
    /// The default value is `0`, with a minimum of `-100` (shy) and a maximum of `100` (confident). A value of `0` leaves this parameter unchanged from the base voice.
    /// </summary>
    [JsonPropertyName("confidence")]
    public int? Confidence { get; set; }

    /// <summary>
    /// The perceived excitement within the voice, ranging between calm and enthusiastic.
    ///
    /// The default value is `0`, with a minimum of `-100` (calm) and a maximum of `100` (enthusiastic). A value of `0` leaves this parameter unchanged from the base voice.
    /// </summary>
    [JsonPropertyName("enthusiasm")]
    public int? Enthusiasm { get; set; }

    /// <summary>
    /// The perceived openness of the voice, ranging between clear and nasal.
    ///
    /// The default value is `0`, with a minimum of `-100` (clear) and a maximum of `100` (nasal). A value of `0` leaves this parameter unchanged from the base voice.
    /// </summary>
    [JsonPropertyName("nasality")]
    public int? Nasality { get; set; }

    /// <summary>
    /// The perceived stress within the voice, ranging between tense and relaxed.
    ///
    /// The default value is `0`, with a minimum of `-100` (tense) and a maximum of `100` (relaxed). A value of `0` leaves this parameter unchanged from the base voice.
    /// </summary>
    [JsonPropertyName("relaxedness")]
    public int? Relaxedness { get; set; }

    /// <summary>
    /// The perceived texture of the voice, ranging between smooth and staccato.
    ///
    /// The default value is `0`, with a minimum of `-100` (smooth) and a maximum of `100` (staccato). A value of `0` leaves this parameter unchanged from the base voice.
    /// </summary>
    [JsonPropertyName("smoothness")]
    public int? Smoothness { get; set; }

    /// <summary>
    /// The perceived liveliness behind the voice, ranging between tepid and vigorous.
    ///
    /// The default value is `0`, with a minimum of `-100` (tepid) and a maximum of `100` (vigorous). A value of `0` leaves this parameter unchanged from the base voice.
    /// </summary>
    [JsonPropertyName("tepidity")]
    public int? Tepidity { get; set; }

    /// <summary>
    /// The perceived containment of the voice, ranging between tight and breathy.
    ///
    /// The default value is `0`, with a minimum of `-100` (tight) and a maximum of `100` (breathy). A value of `0` leaves this parameter unchanged from the base voice.
    /// </summary>
    [JsonPropertyName("tightness")]
    public int? Tightness { get; set; }

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
