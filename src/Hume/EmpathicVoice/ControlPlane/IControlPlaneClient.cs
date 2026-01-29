using Hume;
using OneOf;

namespace Hume.EmpathicVoice;

public partial interface IControlPlaneClient
{
    /// <summary>
    /// Send a message to a specific chat.
    /// </summary>
    System.Threading.Tasks.Task SendAsync(
        string chatId,
        OneOf<
            SessionSettings,
            UserInput,
            AssistantInput,
            ToolResponseMessage,
            ToolErrorMessage,
            PauseAssistantMessage,
            ResumeAssistantMessage
        > request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
