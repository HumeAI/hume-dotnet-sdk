using System.ComponentModel;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;

namespace Hume.Core.WebSockets;

/// <summary>
/// Abstract base class for asynchronous API implementations that use WebSocket connections.
/// Provides common functionality for connection management, message sending, and event handling.
/// </summary>
/// <typeparam name="T">The type of API options.</typeparam>
public abstract class AsyncApi<T> : IAsyncDisposable, IDisposable, INotifyPropertyChanged
{
    private ConnectionStatus _status = ConnectionStatus.Disconnected;
    private WebSocketConnection? _webSocket;

    /// <summary>
    /// Initializes a new instance of the AsyncApi class with the specified options.
    /// </summary>
    /// <param name="options">The API configuration options.</param>
    protected internal AsyncApi(T options)
    {
        ApiOptions = options;
    }

    /// <summary>
    /// Creates the WebSocket URI for the connection.
    /// </summary>
    /// <returns>The URI to connect to.</returns>
    protected abstract Uri CreateUri();

    /// <summary>
    /// Disposes any custom events specific to the derived class.
    /// </summary>
    protected abstract void DisposeEvents();

    /// <summary>
    /// Configures the WebSocket connection options before establishing the connection.
    /// Override this method to customize connection options.
    /// </summary>
    /// <param name="options">The WebSocket client options to configure.</param>
    protected virtual void SetConnectionOptions(ClientWebSocketOptions options) { }

    /// <summary>
    /// Handles incoming text messages from the WebSocket connection.
    /// </summary>
    /// <param name="stream">The stream containing the received text message.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected abstract Task OnTextMessage(Stream stream);

    /// <summary>
    /// Handles incoming binary messages from the WebSocket connection.
    ///
    /// Override this method to handle binary message content.
    /// (Default behavior is to do nothing)
    /// </summary>
    /// <param name="stream">The stream containing the received binary message.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected virtual Task OnBinaryMessage(Stream stream)
    {
        stream.Dispose();
        return Task.CompletedTask;
    }

    /// <summary>
    /// Gets the API configuration options.
    /// </summary>
    protected T ApiOptions { get; }

    /// <summary>
    /// Gets the current connection status of the WebSocket.
    /// </summary>
    public ConnectionStatus Status
    {
        get => _status;
        private set
        {
            if (_status != value)
            {
                _status = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Ensures the WebSocket is connected before sending.
    /// </summary>
    private void EnsureConnected()
    {
        this.Assert(
            Status == ConnectionStatus.Connected,
            $"Cannot send message when status is {Status}"
        );
    }

    /// <summary>
    /// Sends a text message instantly through the WebSocket connection.
    /// </summary>
    /// <param name="message">The text message to send.</param>
    /// <returns>A task representing the asynchronous send operation.</returns>
    /// <exception cref="Exception">Thrown when the connection is not in Connected status.</exception>
    protected internal Task SendInstant(string message)
    {
        EnsureConnected();
        return _webSocket!.SendInstant(message);
    }

    /// <summary>
    /// Sends a binary message instantly through the WebSocket connection.
    /// </summary>
    /// <param name="message">The binary message to send as a Memory&lt;byte&gt;.</param>
    /// <returns>A task representing the asynchronous send operation.</returns>
    /// <exception cref="Exception">Thrown when the connection is not in Connected status.</exception>
    protected internal Task SendInstant(Memory<byte> message)
    {
        EnsureConnected();
        return _webSocket!.SendInstant(message);
    }

    /// <summary>
    /// Sends a binary message instantly through the WebSocket connection.
    /// </summary>
    /// <param name="message">The binary message to send as an ArraySegment&lt;byte&gt;.</param>
    /// <returns>A task representing the asynchronous send operation.</returns>
    /// <exception cref="Exception">Thrown when the connection is not in Connected status.</exception>
    protected internal Task SendInstant(ArraySegment<byte> message)
    {
        EnsureConnected();
        return _webSocket!.SendInstant(message);
    }

    /// <summary>
    /// Sends a binary message instantly through the WebSocket connection.
    /// </summary>
    /// <param name="message">The binary message to send as a byte array.</param>
    /// <returns>A task representing the asynchronous send operation.</returns>
    /// <exception cref="Exception">Thrown when the connection is not in Connected status.</exception>
    protected internal Task SendInstant(byte[] message)
    {
        EnsureConnected();
        return _webSocket!.SendInstant(message);
    }

    /// <summary>
    /// Asynchronously disposes the AsyncApi instance, closing any active connections and cleaning up resources.
    /// </summary>
    /// <returns>A ValueTask representing the asynchronous dispose operation.</returns>
    public async ValueTask DisposeAsync()
    {
        if (_webSocket != null)
        {
            if (_webSocket.IsRunning)
            {
                await CloseAsync().ConfigureAwait(false);
            }

            _webSocket.Dispose();
            _webSocket = null;
        }

        DisposeEventsInternal();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Synchronously disposes the AsyncApi instance, closing any active connections and cleaning up resources.
    /// </summary>
    public void Dispose()
    {
        if (_webSocket != null)
        {
            if (_webSocket.IsRunning)
            {
                CloseAsync().Wait();
            }

            _webSocket.Dispose();
        }

        DisposeEventsInternal();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes all internal events and calls the derived class's DisposeEvents method.
    /// </summary>
    private void DisposeEventsInternal()
    {
        ExceptionOccurred.Dispose();
        Closed.Dispose();
        Connected.Dispose();
        DisposeEvents();
    }

    /// <summary>
    /// Asynchronously closes the WebSocket connection with normal closure status.
    /// </summary>
    /// <returns>A task representing the asynchronous close operation.</returns>
    public virtual async Task CloseAsync()
    {
        if (_webSocket != null)
        {
            Status = ConnectionStatus.Disconnecting;
            await _webSocket.StopOrFail(WebSocketCloseStatus.NormalClosure, "");
            Status = ConnectionStatus.Disconnected;
        }
    }

    /// <summary>
    /// Asynchronously establishes a WebSocket connection to the target URI.
    /// </summary>
    /// <returns>A task representing the asynchronous connect operation.</returns>
    /// <exception cref="Exception">Thrown when the connection status is not Disconnected or when connection fails.</exception>
    public virtual async Task ConnectAsync()
    {
        this.Assert(
            Status == ConnectionStatus.Disconnected,
            $"Connection status is currently {Status}"
        );

        _webSocket?.Dispose();

        Status = ConnectionStatus.Connecting;

        _webSocket = new WebSocketConnection(
            CreateUri(),
            () =>
            {
                var socket = new ClientWebSocket();
                SetConnectionOptions(socket.Options);
                return socket;
            }
        )
        {
            ExceptionOccurred = ExceptionOccurred.RaiseEvent,
            TextMessageReceived = OnTextMessage,
            BinaryMessageReceived = OnBinaryMessage,
            DisconnectionHappened = async d =>
            {
                await Closed
                    .RaiseEvent(
                        new Closed { Code = (int?)d.CloseStatus, Reason = d.CloseStatusDescription }
                    )
                    .ConfigureAwait(false);
            },
        };

        try
        {
            await _webSocket.StartOrFail().ConfigureAwait(false);
            Status = ConnectionStatus.Connected;
            await Connected.RaiseEvent(new Connected()).ConfigureAwait(false);
        }
        catch (Exception)
        {
            Status = ConnectionStatus.Disconnected;
            throw;
        }
    }

    /// <summary>
    /// Event that is raised when the WebSocket connection is successfully established.
    /// </summary>
    public readonly Event<Connected> Connected = new();

    /// <summary>
    /// Event that is raised when the WebSocket connection is closed.
    /// </summary>
    public readonly Event<Closed> Closed = new();

    /// <summary>
    /// Event that is raised when an exception occurs during WebSocket operations.
    /// </summary>
    public readonly Event<Exception> ExceptionOccurred = new();

    /// <summary>
    /// Event that is raised when a property value changes.
    /// Currently only raised for the Status property.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed. Automatically populated by the compiler.</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
