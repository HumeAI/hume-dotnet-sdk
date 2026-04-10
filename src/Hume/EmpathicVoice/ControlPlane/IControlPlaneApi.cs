using Hume.Core.WebSockets;
using OneOf;

namespace Hume.EmpathicVoice;

public partial interface IControlPlaneApi : IAsyncDisposable, IDisposable
{
    public Event<Connected> Connected { get; }
    public Event<Closed> Closed { get; }
    public Event<Exception> ExceptionOccurred { get; }
    public Event<ReconnectionInfo> Reconnecting { get; }
    public ConnectionStatus Status { get; }
    global::System.Threading.Tasks.Task ConnectAsync(CancellationToken cancellationToken = default);

    global::System.Threading.Tasks.Task Send(
        OneOf<
            SessionSettings,
            UserInput,
            AssistantInput,
            ToolResponseMessage,
            ToolErrorMessage,
            PauseAssistantMessage,
            ResumeAssistantMessage
        > message,
        CancellationToken cancellationToken = default
    );

    global::System.Threading.Tasks.Task CloseAsync(CancellationToken cancellationToken = default);
}
