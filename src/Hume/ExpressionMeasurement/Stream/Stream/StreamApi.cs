using System.ComponentModel;
using System.Text.Json;
using Hume.Core;
using Hume.Core.WebSockets;

namespace Hume.ExpressionMeasurement.Stream;

public partial class StreamApi : IAsyncDisposable, IDisposable, INotifyPropertyChanged
{
    private readonly StreamApi.Options _options;

    private readonly WebSocketClient _client;

    /// <summary>
    /// Event that is raised when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged
    {
        add => _client.PropertyChanged += value;
        remove => _client.PropertyChanged -= value;
    }

    /// <summary>
    /// Event handler for StreamModelPredictions.
    /// Use StreamModelPredictions.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<StreamModelPredictions> StreamModelPredictions = new();

    /// <summary>
    /// Event handler for StreamErrorMessage.
    /// Use StreamErrorMessage.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<StreamErrorMessage> StreamErrorMessage = new();

    /// <summary>
    /// Event handler for StreamWarningMessage.
    /// Use StreamWarningMessage.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<StreamWarningMessage> StreamWarningMessage = new();

    /// <summary>
    /// Default constructor
    /// </summary>
    public StreamApi() { }

    /// <summary>
    /// Constructor with options
    /// </summary>
    public StreamApi(StreamApi.Options options)
    {
        _options = options;
        var uri = new UriBuilder(_options.BaseUrl);
        uri.Path = $"{uri.Path.TrimEnd('/')}/models";
        _client = new WebSocketClient(uri.Uri, OnTextMessage);
    }

    /// <summary>
    /// Gets the current connection status of the WebSocket.
    /// </summary>
    public ConnectionStatus Status => _client.Status;

    /// <summary>
    /// Event that is raised when the WebSocket connection is established.
    /// </summary>
    public Event<Connected> Connected => _client.Connected;

    /// <summary>
    /// Event that is raised when the WebSocket connection is closed.
    /// </summary>
    public Event<Closed> Closed => _client.Closed;

    /// <summary>
    /// Event that is raised when an exception occurs during WebSocket operations.
    /// </summary>
    public Event<Exception> ExceptionOccurred => _client.ExceptionOccurred;

    /// <summary>
    /// Disposes of event subscriptions
    /// </summary>
    private void DisposeEvents()
    {
        StreamModelPredictions.Dispose();
        StreamErrorMessage.Dispose();
        StreamWarningMessage.Dispose();
    }

    /// <summary>
    /// Dispatches incoming WebSocket messages
    /// </summary>
    private async System.Threading.Tasks.Task OnTextMessage(System.IO.Stream stream)
    {
        var json = await JsonSerializer.DeserializeAsync<JsonDocument>(stream);
        if (json == null)
        {
            await ExceptionOccurred
                .RaiseEvent(new Exception("Invalid message - Not valid JSON"))
                .ConfigureAwait(false);
            return;
        }

        // deserialize the message to find the correct event
        {
            if (JsonUtils.TryDeserialize(json, out StreamModelPredictions? message))
            {
                await StreamModelPredictions.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out StreamErrorMessage? message))
            {
                await StreamErrorMessage.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out StreamWarningMessage? message))
            {
                await StreamWarningMessage.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        await ExceptionOccurred
            .RaiseEvent(new Exception($"Unknown message: {json.ToString()}"))
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Asynchronously establishes a WebSocket connection.
    /// </summary>
    public async System.Threading.Tasks.Task ConnectAsync()
    {
        await _client.ConnectAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Asynchronously closes the WebSocket connection.
    /// </summary>
    public async System.Threading.Tasks.Task CloseAsync()
    {
        await _client.CloseAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Asynchronously disposes the WebSocket client, closing any active connections and cleaning up resources.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await _client.DisposeAsync();
        DisposeEvents();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Synchronously disposes the WebSocket client, closing any active connections and cleaning up resources.
    /// </summary>
    public void Dispose()
    {
        _client.Dispose();
        DisposeEvents();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Sends a StreamModelsEndpointPayload message to the server
    /// </summary>
    public async System.Threading.Tasks.Task Send(StreamModelsEndpointPayload message)
    {
        await _client.SendInstant(JsonUtils.Serialize(message)).ConfigureAwait(false);
    }

    /// <summary>
    /// Options for the API client
    /// </summary>
    public class Options
    {
        /// <summary>
        /// The Websocket URL for the API connection.
        /// </summary>
        public string BaseUrl { get; set; } = "wss://api.hume.ai/v0/stream";
    }
}
