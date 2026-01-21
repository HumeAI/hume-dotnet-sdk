using System.ComponentModel;
using System.Text.Json;
using Hume.Core;
using Hume.Core.WebSockets;
using OneOf;

namespace Hume.EmpathicVoice;

/// <summary>
/// Connects to an in-progress EVI chat session. The original chat must have been started with `allow_connection=true`. The connection can be used to send and receive the same messages as the original chat, with the exception that `audio_input` messages are not allowed.
/// </summary>
public partial class ControlPlaneApi : IAsyncDisposable, IDisposable, INotifyPropertyChanged
{
    private readonly ControlPlaneApi.Options _options;

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
    /// Event handler for AssistantEnd.
    /// Use AssistantEnd.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<AssistantEnd> AssistantEnd = new();

    /// <summary>
    /// Event handler for AssistantMessage.
    /// Use AssistantMessage.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<AssistantMessage> AssistantMessage = new();

    /// <summary>
    /// Event handler for AssistantProsody.
    /// Use AssistantProsody.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<AssistantProsody> AssistantProsody = new();

    /// <summary>
    /// Event handler for AudioOutput.
    /// Use AudioOutput.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<AudioOutput> AudioOutput = new();

    /// <summary>
    /// Event handler for ChatMetadata.
    /// Use ChatMetadata.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<ChatMetadata> ChatMetadata = new();

    /// <summary>
    /// Event handler for WebSocketError.
    /// Use WebSocketError.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<WebSocketError> WebSocketError = new();

    /// <summary>
    /// Event handler for UserInterruption.
    /// Use UserInterruption.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<UserInterruption> UserInterruption = new();

    /// <summary>
    /// Event handler for UserMessage.
    /// Use UserMessage.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<UserMessage> UserMessage = new();

    /// <summary>
    /// Event handler for ToolCallMessage.
    /// Use ToolCallMessage.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<ToolCallMessage> ToolCallMessage = new();

    /// <summary>
    /// Event handler for ToolResponseMessage.
    /// Use ToolResponseMessage.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<ToolResponseMessage> ToolResponseMessage = new();

    /// <summary>
    /// Event handler for ToolErrorMessage.
    /// Use ToolErrorMessage.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<ToolErrorMessage> ToolErrorMessage = new();

    /// <summary>
    /// Event handler for SessionSettings.
    /// Use SessionSettings.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<SessionSettings> SessionSettings = new();

    /// <summary>
    /// Constructor with options
    /// </summary>
    public ControlPlaneApi(ControlPlaneApi.Options options)
    {
        _options = options;
        var uri = new UriBuilder(_options.BaseUrl)
        {
            Query = new Query() { { "access_token", _options.AccessToken } },
        };
        uri.Path = $"{uri.Path.TrimEnd('/')}/chat/{Uri.EscapeDataString(_options.ChatId)}/connect";
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
        AssistantEnd.Dispose();
        AssistantMessage.Dispose();
        AssistantProsody.Dispose();
        AudioOutput.Dispose();
        ChatMetadata.Dispose();
        WebSocketError.Dispose();
        UserInterruption.Dispose();
        UserMessage.Dispose();
        ToolCallMessage.Dispose();
        ToolResponseMessage.Dispose();
        ToolErrorMessage.Dispose();
        SessionSettings.Dispose();
    }

    /// <summary>
    /// Dispatches incoming WebSocket messages
    /// </summary>
    private async System.Threading.Tasks.Task OnTextMessage(Stream stream)
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
            if (JsonUtils.TryDeserialize(json, out AssistantEnd? message))
            {
                await AssistantEnd.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out AssistantMessage? message))
            {
                await AssistantMessage.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out AssistantProsody? message))
            {
                await AssistantProsody.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out AudioOutput? message))
            {
                await AudioOutput.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out ChatMetadata? message))
            {
                await ChatMetadata.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out WebSocketError? message))
            {
                await WebSocketError.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out UserInterruption? message))
            {
                await UserInterruption.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out UserMessage? message))
            {
                await UserMessage.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out ToolCallMessage? message))
            {
                await ToolCallMessage.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out ToolResponseMessage? message))
            {
                await ToolResponseMessage.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out ToolErrorMessage? message))
            {
                await ToolErrorMessage.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out SessionSettings? message))
            {
                await SessionSettings.RaiseEvent(message!).ConfigureAwait(false);
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
    /// Sends a ControlPlanePublishEvent message to the server
    /// </summary>
    public async System.Threading.Tasks.Task Send(
        OneOf<
            SessionSettings,
            UserInput,
            AssistantInput,
            ToolResponseMessage,
            ToolErrorMessage,
            PauseAssistantMessage,
            ResumeAssistantMessage
        > message
    )
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
        public string BaseUrl { get; set; } = "evi";

        /// <summary>
        /// Access token used for authenticating the client. If not provided, an `api_key` must be provided to authenticate.
        ///
        /// The access token is generated using both an API key and a Secret key, which provides an additional layer of security compared to using just an API key.
        ///
        /// For more details, refer to the [Authentication Strategies Guide](/docs/introduction/api-key#authentication-strategies).
        /// </summary>
        public string? AccessToken { get; set; }

        /// <summary>
        /// The ID of the chat to connect to.
        /// </summary>
        public required string ChatId { get; set; }
    }
}
