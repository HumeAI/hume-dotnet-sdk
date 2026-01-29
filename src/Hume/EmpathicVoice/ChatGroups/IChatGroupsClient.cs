using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

public partial interface IChatGroupsClient
{
    /// <summary>
    /// Fetches a paginated list of **Chat Groups**.
    /// </summary>
    System.Threading.Tasks.Task<Pager<ReturnChatGroup>> ListChatGroupsAsync(
        ChatGroupsListChatGroupsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Fetches a **ChatGroup** by ID, including a paginated list of **Chats** associated with the **ChatGroup**.
    /// </summary>
    WithRawResponseTask<ReturnChatGroupPagedChats> GetChatGroupAsync(
        string id,
        ChatGroupsGetChatGroupRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Fetches a paginated list of audio for each **Chat** within the specified **Chat Group**. For more details, see our guide on audio reconstruction [here](/docs/speech-to-speech-evi/faq#can-i-access-the-audio-of-previous-conversations-with-evi).
    /// </summary>
    WithRawResponseTask<ReturnChatGroupPagedAudioReconstructions> GetAudioAsync(
        string id,
        ChatGroupsGetAudioRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Fetches a paginated list of **Chat** events associated with a **Chat Group**.
    /// </summary>
    System.Threading.Tasks.Task<Pager<ReturnChatEvent>> ListChatGroupEventsAsync(
        string id,
        ChatGroupsListChatGroupEventsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
