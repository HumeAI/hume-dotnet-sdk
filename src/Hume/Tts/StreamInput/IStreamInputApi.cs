using Hume.Core.WebSockets;

namespace Hume.Tts;

public partial interface IStreamInputApi : IAsyncDisposable, IDisposable
{
    public Event<Connected> Connected { get; }
    public Event<Closed> Closed { get; }
    public Event<Exception> ExceptionOccurred { get; }
    public Event<ReconnectionInfo> Reconnecting { get; }
    public ConnectionStatus Status { get; }
    global::System.Threading.Tasks.Task ConnectAsync(CancellationToken cancellationToken = default);

    global::System.Threading.Tasks.Task Send(
        PublishTts message,
        CancellationToken cancellationToken = default
    );

    global::System.Threading.Tasks.Task CloseAsync(CancellationToken cancellationToken = default);
}
