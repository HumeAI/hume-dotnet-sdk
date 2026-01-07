using System.ComponentModel;
using System.Net.WebSockets;
using System.Text.Json;
using Hume.Core;
using Hume.Core.WebSockets;

namespace Hume.ExpressionMeasurement.Stream;

public partial class StreamApi : IAsyncDisposable, IDisposable, INotifyPropertyChanged
{
    private readonly Options _options;
    private readonly WebSocketClient _client;

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
    public StreamApi()
        : this(new StreamApi.Options()) { }

    /// <summary>
    /// Constructor with options
    /// </summary>
    public StreamApi(StreamApi.Options options)
    {
        _options = options;

        var uriBuilder = new UriBuilder(_options.BaseUrl);
        uriBuilder.Path = $"{uriBuilder.Path.TrimEnd('/')}/models";

        _client = new WebSocketClient(uriBuilder.Uri, OnTextMessage);
    }

    /// <summary>
    /// Gets the current connection status of the WebSocket.
    /// </summary>
    public ConnectionStatus Status => _client.Status;

    /// <summary>
    /// Event that is raised when the WebSocket connection is successfully established.
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
    /// Event that is raised when a property value changes.
    /// Currently only raised for the Status property.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged
    {
        add => _client.PropertyChanged += value;
        remove => _client.PropertyChanged -= value;
    }

    /// <summary>
    /// Dispatches incoming WebSocket messages
    /// </summary>
    private async Task OnTextMessage(System.IO.Stream stream)
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
    /// Asynchronously establishes a WebSocket connection to the target URI.
    /// </summary>
    public Task ConnectAsync() => _client.ConnectAsync();

    /// <summary>
    /// Asynchronously closes the WebSocket connection with normal closure status.
    /// </summary>
    public Task CloseAsync() => _client.CloseAsync();

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
    /// Asynchronously disposes the StreamApi instance, closing any active connections and cleaning up resources.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await _client.DisposeAsync();
        DisposeEvents();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Synchronously disposes the StreamApi instance, closing any active connections and cleaning up resources.
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
