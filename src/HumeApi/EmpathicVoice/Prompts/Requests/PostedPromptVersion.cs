using System.Text.Json.Serialization;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

[Serializable]
public record PostedPromptVersion
{
    /// <summary>
    /// An optional description of the Prompt version.
    /// </summary>
    [JsonPropertyName("version_description")]
    public string? VersionDescription { get; set; }

    /// <summary>
    /// Instructions used to shape EVI’s behavior, responses, and style for this version of the Prompt.
    ///
    /// You can use the Prompt to define a specific goal or role for EVI, specifying how it should act or what it should focus on during the conversation. For example, EVI can be instructed to act as a customer support representative, a fitness coach, or a travel advisor, each with its own set of behaviors and response styles.
    ///
    /// For help writing a system prompt, see our [Prompting Guide](/docs/empathic-voice-interface-evi/guides/prompting).
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
