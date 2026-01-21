using Hume;
using OneOf;

namespace Hume.Tts;

public partial interface ITtsClient
{
    public VoicesClient Voices { get; }

    /// <summary>
    /// Synthesizes one or more input texts into speech using the specified voice. If no voice is provided, a novel voice will be generated dynamically. Optionally, additional context can be included to influence the speech's style and prosody.
    ///
    /// The response includes the base64-encoded audio and metadata in JSON format.
    /// </summary>
    WithRawResponseTask<ReturnTts> SynthesizeJsonAsync(
        PostedTts request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Synthesizes one or more input texts into speech using the specified voice. If no voice is provided, a novel voice will be generated dynamically. Optionally, additional context can be included to influence the speech's style and prosody.
    ///
    /// The response contains the generated audio file in the requested format.
    /// </summary>
    WithRawResponseTask<System.IO.Stream> SynthesizeFileAsync(
        PostedTts request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Streams synthesized speech using the specified voice. If no voice is provided, a novel voice will be generated dynamically. Optionally, additional context can be included to influence the speech's style and prosody.
    /// </summary>
    WithRawResponseTask<System.IO.Stream> SynthesizeFileStreamingAsync(
        PostedTts request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Streams synthesized speech using the specified voice. If no voice is provided, a novel voice will be generated dynamically. Optionally, additional context can be included to influence the speech's style and prosody.
    ///
    /// The response is a stream of JSON objects including audio encoded in base64.
    /// </summary>
    IAsyncEnumerable<OneOf<SnippetAudioChunk, TimestampMessage>> SynthesizeJsonStreamingAsync(
        PostedTts request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<System.IO.Stream> ConvertVoiceFileAsync(
        ConvertVoiceFileRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    IAsyncEnumerable<OneOf<SnippetAudioChunk, TimestampMessage>> ConvertVoiceJsonAsync(
        ConvertVoiceJsonRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
