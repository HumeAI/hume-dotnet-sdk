using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.Tts;

[Serializable]
public record TtsConversionStreamJsonV0TtsStreamJsonMultipartPostRequest
{
    /// <summary>
    /// Access token used for authenticating the client. If not provided, an `api_key` must be provided to authenticate.
    ///
    /// The access token is generated using both an API key and a Secret key, which provides an additional layer of security compared to using just an API key.
    ///
    /// For more details, refer to the [Authentication Strategies Guide](/docs/introduction/api-key#authentication-strategies).
    /// </summary>
    [JsonIgnore]
    public string? AccessToken { get; set; }

    /// <summary>
    /// The ID of a prior TTS generation to use as context for generating consistent speech style and prosody across multiple requests. Including context may increase audio generation times.
    /// </summary>
    public string? ContextGenerationId { get; set; }

    /// <summary>
    /// Natural language instructions describing how the synthesized speech should sound, including but not limited to tone, intonation, pacing, and accent.
    ///
    /// **This field behaves differently depending on whether a voice is specified**:
    /// - **Voice specified**: the description will serve as acting directions for delivery. Keep directions concise—100 characters or fewer—for best results. See our guide on [acting instructions](/docs/text-to-speech-tts/acting-instructions).
    /// - **Voice not specified**: the description will serve as a voice prompt for generating a voice. See our [prompting guide](/docs/text-to-speech-tts/prompting) for design tips.
    /// </summary>
    public string? ContextUtterancesNDescription { get; set; }

    /// <summary>
    /// Speed multiplier for the synthesized speech. Extreme values below 0.75 and above 1.5 may sometimes cause instability to the generated output.
    /// </summary>
    public double? ContextUtterancesNSpeed { get; set; }

    /// <summary>
    /// The input text to be synthesized into speech.
    /// </summary>
    public string? ContextUtterancesNText { get; set; }

    /// <summary>
    /// Duration of trailing silence (in seconds) to add to this utterance
    /// </summary>
    public double? ContextUtterancesNTrailingSilence { get; set; }

    /// <summary>
    /// The unique ID associated with the **Voice**.
    /// </summary>
    public string? ContextUtterancesNVoiceId { get; set; }

    /// <summary>
    /// The name of a **Voice**.
    /// </summary>
    public string? ContextUtterancesNVoiceName { get; set; }

    /// <summary>
    /// Specifies the source provider associated with the chosen voice.
    ///
    /// - **`HUME_AI`**: Select voices from Hume's [Voice Library](https://app.hume.ai/tts/voice-library), containing a variety of preset, shared voices.
    /// - **`CUSTOM_VOICE`**: Select from voices you've personally generated and saved in your account.
    ///
    /// If no provider is explicitly set, the default provider is `CUSTOM_VOICE`. When using voices from Hume's **Voice Library**, you must explicitly set the provider to `HUME_AI`.
    ///
    /// Preset voices from Hume's **Voice Library** are accessible by all users. In contrast, your custom voices are private and accessible only via requests authenticated with your API key.
    /// </summary>
    public VoiceProvider? ContextUtterancesNVoiceProvider { get; set; }

    /// <summary>
    /// If enabled, enhances the provided description prompt to improve voice generation quality.
    /// </summary>
    public bool? ExpandDescription { get; set; }

    /// <summary>
    /// If enabled, additional generations will be made, and the best `num_generations` of them all will be returned.
    /// </summary>
    public bool? FilterGenerations { get; set; }

    /// <summary>
    /// Format for the output audio.
    /// </summary>
    public string? FormatType { get; set; }

    /// <summary>
    /// The set of timestamp types to include in the response. Only supported for Octave 2 requests.
    /// </summary>
    public TimestampType? IncludeTimestampTypesN { get; set; }

    /// <summary>
    /// Enables ultra-low latency streaming, significantly reducing the time until the first audio chunk is received. Recommended for real-time applications requiring immediate audio playback. For further details, see our documentation on [instant mode](/docs/text-to-speech-tts/overview#ultra-low-latency-streaming-instant-mode).
    /// - A [voice](/reference/text-to-speech-tts/synthesize-json-streaming#request.body.utterances.voice) must be specified when instant mode is enabled. Dynamic voice generation is not supported with this mode.
    /// - Instant mode is only supported for streaming endpoints (e.g., [/v0/tts/stream/json](/reference/text-to-speech-tts/synthesize-json-streaming), [/v0/tts/stream/file](/reference/text-to-speech-tts/synthesize-file-streaming)).
    /// - Ensure only a single generation is requested ([num_generations](/reference/text-to-speech-tts/synthesize-json-streaming#request.body.num_generations) must be `1` or omitted).
    /// </summary>
    public bool? InstantMode { get; set; }

    /// <summary>
    /// The TTS model to use for speech generations.
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// If enabled, consecutive utterances with the different voices will be generated with compounding context that takes into account the previous utterances.
    /// </summary>
    public bool? MultiSpeaker { get; set; }

    /// <summary>
    /// If enabled, no binary websocket messages will be sent to the client.
    /// </summary>
    public bool? NoBinary { get; set; }

    /// <summary>
    /// Number of audio generations to produce from the input utterances.
    ///
    /// Using `num_generations` enables faster processing than issuing multiple sequential requests. Additionally, specifying `num_generations` allows prosody continuation across all generations without repeating context, ensuring each generation sounds slightly different while maintaining contextual consistency.
    /// </summary>
    public int? NumGenerations { get; set; }

    /// <summary>
    /// Controls how audio output is segmented in the response.
    ///
    /// - When **enabled** (`true`), input utterances are automatically split into natural-sounding speech segments.
    ///
    /// - When **disabled** (`false`), the response maintains a strict one-to-one mapping between input utterances and output snippets.
    ///
    /// This setting affects how the `snippets` array is structured in the response, which may be important for applications that need to track the relationship between input text and generated audio segments. When setting to `false`, avoid including utterances with long `text`, as this can result in distorted output.
    /// </summary>
    public bool? SplitUtterances { get; set; }

    /// <summary>
    /// If enabled, the audio for all the chunks of a generation, once concatenated together, will constitute a single audio file. Otherwise, if disabled, each chunk's audio will be its own audio file, each with its own headers (if applicable).
    /// </summary>
    public bool? StripHeaders { get; set; }

    public FileParameter? UtterancesNAudio { get; set; }

    /// <summary>
    /// Natural language instructions describing how the synthesized speech should sound, including but not limited to tone, intonation, pacing, and accent.
    ///
    /// **This field behaves differently depending on whether a voice is specified**:
    /// - **Voice specified**: the description will serve as acting directions for delivery. Keep directions concise—100 characters or fewer—for best results. See our guide on [acting instructions](/docs/text-to-speech-tts/acting-instructions).
    /// - **Voice not specified**: the description will serve as a voice prompt for generating a voice. See our [prompting guide](/docs/text-to-speech-tts/prompting) for design tips.
    /// </summary>
    public string? UtterancesNDescription { get; set; }

    /// <summary>
    /// Speed multiplier for the synthesized speech. Extreme values below 0.75 and above 1.5 may sometimes cause instability to the generated output.
    /// </summary>
    public double? UtterancesNSpeed { get; set; }

    /// <summary>
    /// The input text to be synthesized into speech.
    /// </summary>
    public string? UtterancesNText { get; set; }

    /// <summary>
    /// Duration of trailing silence (in seconds) to add to this utterance
    /// </summary>
    public double? UtterancesNTrailingSilence { get; set; }

    /// <summary>
    /// The unique ID associated with the **Voice**.
    /// </summary>
    public string? UtterancesNVoiceId { get; set; }

    /// <summary>
    /// The name of a **Voice**.
    /// </summary>
    public string? UtterancesNVoiceName { get; set; }

    /// <summary>
    /// Specifies the source provider associated with the chosen voice.
    ///
    /// - **`HUME_AI`**: Select voices from Hume's [Voice Library](https://app.hume.ai/tts/voice-library), containing a variety of preset, shared voices.
    /// - **`CUSTOM_VOICE`**: Select from voices you've personally generated and saved in your account.
    ///
    /// If no provider is explicitly set, the default provider is `CUSTOM_VOICE`. When using voices from Hume's **Voice Library**, you must explicitly set the provider to `HUME_AI`.
    ///
    /// Preset voices from Hume's **Voice Library** are accessible by all users. In contrast, your custom voices are private and accessible only via requests authenticated with your API key.
    /// </summary>
    public VoiceProvider? UtterancesNVoiceProvider { get; set; }

    /// <summary>
    /// Selects the Octave model version used to synthesize speech for this request. If you omit this field, Hume automatically routes the request to the most appropriate model. Setting a specific version ensures stable and repeatable behavior across requests.
    ///
    /// Use `2` to opt into the latest Octave capabilities. When you specify version `2`, you must also provide a `voice`. Requests that set `version: 2` without a voice will be rejected.
    ///
    /// For a comparison of Octave versions, see the [Octave versions](/docs/text-to-speech-tts/overview#octave-versions) section in the TTS overview.
    /// </summary>
    public OctaveVersion? Version { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
