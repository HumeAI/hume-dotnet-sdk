using Hume;
using Hume.Core;
using OneOf;

namespace Hume.Tts;

[Serializable]
public record ConvertVoiceJsonRequest
{
    /// <summary>
    /// If enabled, the audio for all the chunks of a generation, once concatenated together, will constitute a single audio file. Otherwise, if disabled, each chunk's audio will be its own audio file, each with its own headers (if applicable).
    /// </summary>
    public bool? StripHeaders { get; set; }

    public FileParameter? Audio { get; set; }

    /// <summary>
    /// Utterances to use as context for generating consistent speech style and prosody across multiple requests. These will not be converted to speech output.
    /// </summary>
    public OneOf<PostedContextWithGenerationId, PostedContextWithUtterances>? Context { get; set; }

    public OneOf<PostedUtteranceVoiceWithId, PostedUtteranceVoiceWithName>? Voice { get; set; }

    /// <summary>
    /// Specifies the output audio file format.
    /// </summary>
    public OneOf<FormatMp3, FormatPcm, FormatWav>? Format { get; set; }

    /// <summary>
    /// The set of timestamp types to include in the response.
    /// </summary>
    public IEnumerable<TimestampType>? IncludeTimestampTypes { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
