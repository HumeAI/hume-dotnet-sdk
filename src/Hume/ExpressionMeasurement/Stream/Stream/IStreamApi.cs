using global::System.Text.Json;
using Hume.Core.WebSockets;

namespace Hume.ExpressionMeasurement.Stream;

public partial interface IStreamApi : IAsyncDisposable, IDisposable
{
    public Event<Connected> Connected { get; }
    public Event<Closed> Closed { get; }
    public Event<Exception> ExceptionOccurred { get; }
    public Event<ReconnectionInfo> Reconnecting { get; }
    public Event<StreamModelPredictions> StreamModelPredictions { get; }
    public Event<StreamErrorMessage> StreamErrorMessage { get; }
    public Event<StreamWarningMessage> StreamWarningMessage { get; }
    public Event<JsonElement> UnknownMessage { get; }
    public ConnectionStatus Status { get; }
    global::System.Threading.Tasks.Task ConnectAsync(CancellationToken cancellationToken = default);

    global::System.Threading.Tasks.Task Send(
        StreamModelsEndpointPayload message,
        CancellationToken cancellationToken = default
    );

    global::System.Threading.Tasks.Task CloseAsync(CancellationToken cancellationToken = default);
}
