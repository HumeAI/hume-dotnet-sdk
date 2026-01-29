using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

public partial interface IChatsClient
{
    /// <summary>
    /// Fetches a paginated list of **Chats**.
    /// </summary>
    System.Threading.Tasks.Task<Pager<ReturnChat>> ListChatsAsync(
        ChatsListChatsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Fetches a paginated list of **Chat** events.
    /// </summary>
    System.Threading.Tasks.Task<Pager<ReturnChatEvent>> ListChatEventsAsync(
        string id,
        ChatsListChatEventsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Fetches the audio of a previous **Chat**. For more details, see our guide on audio reconstruction [here](/docs/speech-to-speech-evi/faq#can-i-access-the-audio-of-previous-conversations-with-evi).
    /// </summary>
    WithRawResponseTask<ReturnChatAudioReconstruction> GetAudioAsync(
        string id,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
