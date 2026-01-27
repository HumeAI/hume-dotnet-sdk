using Hume;
using Hume.Core;

namespace Hume.Tts;

public partial interface IVoicesClient
{
    /// <summary>
    /// Lists voices you have saved in your account, or voices from the [Voice Library](https://app.hume.ai/tts/voice-library).
    /// </summary>
    System.Threading.Tasks.Task<Pager<ReturnVoice>> ListAsync(
        VoicesListRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Saves a new custom voice to your account using the specified TTS generation ID.
    ///
    /// Once saved, this voice can be reused in subsequent TTS requests, ensuring consistent speech style and prosody. For more details on voice creation, see the [Voices Guide](/docs/text-to-speech-tts/voices).
    /// </summary>
    WithRawResponseTask<ReturnVoice> CreateAsync(
        PostedVoice request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Deletes a previously generated custom voice.
    /// </summary>
    System.Threading.Tasks.Task DeleteAsync(
        VoicesDeleteRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
