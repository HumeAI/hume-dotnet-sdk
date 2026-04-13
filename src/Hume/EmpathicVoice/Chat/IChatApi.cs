using global::System.Text.Json;
using Hume.Core.WebSockets;
using OneOf;

namespace Hume.EmpathicVoice;

public partial interface IChatApi : IAsyncDisposable, IDisposable
{
    public Event<Connected> Connected { get; }
    public Event<Closed> Closed { get; }
    public Event<Exception> ExceptionOccurred { get; }
    public Event<ReconnectionInfo> Reconnecting { get; }
    public Event<AssistantEnd> AssistantEnd { get; }
    public Event<AssistantMessage> AssistantMessage { get; }
    public Event<AssistantProsody> AssistantProsody { get; }
    public Event<AudioOutput> AudioOutput { get; }
    public Event<ChatMetadata> ChatMetadata { get; }
    public Event<WebSocketError> WebSocketError { get; }
    public Event<UserInterruption> UserInterruption { get; }
    public Event<UserMessage> UserMessage { get; }
    public Event<ToolCallMessage> ToolCallMessage { get; }
    public Event<ToolResponseMessage> ToolResponseMessage { get; }
    public Event<ToolErrorMessage> ToolErrorMessage { get; }
    public Event<SessionSettings> SessionSettings { get; }
    public Event<JsonElement> UnknownMessage { get; }
    public ConnectionStatus Status { get; }
    global::System.Threading.Tasks.Task ConnectAsync(CancellationToken cancellationToken = default);

    global::System.Threading.Tasks.Task Send(
        OneOf<
            AudioInput,
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
