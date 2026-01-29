using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[Serializable]
public record PostedPrompt
{
    /// <summary>
    /// Name applied to all versions of a particular Prompt.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Instructions used to shape EVI's behavior, responses, and style.
    ///
    /// You can use the Prompt to define a specific goal or role for EVI, specifying how it should act or what it should focus on during the conversation. For example, EVI can be instructed to act as a customer support representative, a fitness coach, or a travel advisor, each with its own set of behaviors and response styles. For help writing a system prompt, see our [Prompting Guide](/docs/speech-to-speech-evi/guides/prompting).
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    /// <summary>
    /// An optional description of the Prompt version.
    /// </summary>
    [JsonPropertyName("version_description")]
    public string? VersionDescription { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
