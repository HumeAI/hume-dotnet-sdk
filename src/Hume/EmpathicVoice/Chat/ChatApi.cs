using global::System.ComponentModel;
using global::System.Text.Json;
using Hume.Core;
using Hume.Core.WebSockets;
using OneOf;

namespace Hume.EmpathicVoice;

/// <summary>
/// Chat with Empathic Voice Interface (EVI)
/// </summary>
public partial class ChatApi : IChatApi, IAsyncDisposable, IDisposable, INotifyPropertyChanged
{
    private readonly ChatApi.Options _options;

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
    /// Default constructor
    /// </summary>
    public ChatApi() { }

    /// <summary>
    /// Constructor with options
    /// </summary>
    public ChatApi(ChatApi.Options options)
    {
        _options = options;
        var uri = new UriBuilder(_options.BaseUrl)
        {
            Query = new Hume.Core.QueryStringBuilder.Builder(capacity: 9)
                .Add("access_token", _options.AccessToken)
                .Add("allow_connection", _options.AllowConnection)
                .Add("config_id", _options.ConfigId)
                .Add("config_version", _options.ConfigVersion)
                .Add("event_limit", _options.EventLimit)
                .Add("resumed_chat_group_id", _options.ResumedChatGroupId)
                .Add("verbose_transcription", _options.VerboseTranscription)
                .Add("api_key", _options.ApiKey)
                .AddDeepObject("session_settings", _options.SessionSettings)
                .Build(),
        };
        uri.Path = $"{uri.Path.TrimEnd('/')}/chat";
        _client = new WebSocketClient(uri.Uri, OnTextMessage);
        _client.HttpInvoker = _options.HttpInvoker;
        _client.IsReconnectionEnabled = _options.IsReconnectionEnabled;
        _client.ReconnectTimeout = _options.ReconnectTimeout;
        _client.ErrorReconnectTimeout = _options.ErrorReconnectTimeout;
        _client.LostReconnectTimeout = _options.LostReconnectTimeout;
        _client.Backoff = _options.ReconnectBackoff;
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
    /// Event raised when the WebSocket connection is re-established after a disconnect.
    /// </summary>
    public Event<ReconnectionInfo> Reconnecting => _client.Reconnecting;

    /// <summary>
    /// Event handler for AssistantEnd.
    /// Use AssistantEnd.Subscribe(...) to receive messages.
    /// </summary>
    public Event<AssistantEnd> AssistantEnd { get; } = new();

    /// <summary>
    /// Event handler for AssistantMessage.
    /// Use AssistantMessage.Subscribe(...) to receive messages.
    /// </summary>
    public Event<AssistantMessage> AssistantMessage { get; } = new();

    /// <summary>
    /// Event handler for AssistantProsody.
    /// Use AssistantProsody.Subscribe(...) to receive messages.
    /// </summary>
    public Event<AssistantProsody> AssistantProsody { get; } = new();

    /// <summary>
    /// Event handler for AudioOutput.
    /// Use AudioOutput.Subscribe(...) to receive messages.
    /// </summary>
    public Event<AudioOutput> AudioOutput { get; } = new();

    /// <summary>
    /// Event handler for ChatMetadata.
    /// Use ChatMetadata.Subscribe(...) to receive messages.
    /// </summary>
    public Event<ChatMetadata> ChatMetadata { get; } = new();

    /// <summary>
    /// Event handler for WebSocketError.
    /// Use WebSocketError.Subscribe(...) to receive messages.
    /// </summary>
    public Event<WebSocketError> WebSocketError { get; } = new();

    /// <summary>
    /// Event handler for UserInterruption.
    /// Use UserInterruption.Subscribe(...) to receive messages.
    /// </summary>
    public Event<UserInterruption> UserInterruption { get; } = new();

    /// <summary>
    /// Event handler for UserMessage.
    /// Use UserMessage.Subscribe(...) to receive messages.
    /// </summary>
    public Event<UserMessage> UserMessage { get; } = new();

    /// <summary>
    /// Event handler for ToolCallMessage.
    /// Use ToolCallMessage.Subscribe(...) to receive messages.
    /// </summary>
    public Event<ToolCallMessage> ToolCallMessage { get; } = new();

    /// <summary>
    /// Event handler for ToolResponseMessage.
    /// Use ToolResponseMessage.Subscribe(...) to receive messages.
    /// </summary>
    public Event<ToolResponseMessage> ToolResponseMessage { get; } = new();

    /// <summary>
    /// Event handler for ToolErrorMessage.
    /// Use ToolErrorMessage.Subscribe(...) to receive messages.
    /// </summary>
    public Event<ToolErrorMessage> ToolErrorMessage { get; } = new();

    /// <summary>
    /// Event handler for SessionSettings.
    /// Use SessionSettings.Subscribe(...) to receive messages.
    /// </summary>
    public Event<SessionSettings> SessionSettings { get; } = new();

    /// <summary>
    /// Event handler for unknown/unrecognized message types.
    /// Use UnknownMessage.Subscribe(...) to handle messages from newer server versions.
    /// </summary>
    public Event<JsonElement> UnknownMessage { get; } = new();

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
        UnknownMessage.Dispose();
    }

    /// <summary>
    /// Dispatches incoming WebSocket messages
    /// </summary>
    private async global::System.Threading.Tasks.Task OnTextMessage(Stream stream)
    {
        using var json = await JsonSerializer.DeserializeAsync<JsonDocument>(stream);
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

        await UnknownMessage.RaiseEvent(json.RootElement.Clone()).ConfigureAwait(false);
    }

    /// <summary>
    /// Serializes and sends a JSON message to the server
    /// </summary>
    private async global::System.Threading.Tasks.Task SendJsonAsync(
        object message,
        CancellationToken cancellationToken = default
    )
    {
        await _client
            .SendInstant(JsonUtils.Serialize(message), cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Injects a fake text message for testing. Dispatches through the normal message handling pipeline.
    /// </summary>
    internal async global::System.Threading.Tasks.Task InjectTestMessage(string rawJson)
    {
        using var stream = new MemoryStream(global::System.Text.Encoding.UTF8.GetBytes(rawJson));
        await OnTextMessage(stream).ConfigureAwait(false);
    }

    /// <summary>
    /// Asynchronously establishes a WebSocket connection.
    /// </summary>
    public async global::System.Threading.Tasks.Task ConnectAsync(
        CancellationToken cancellationToken = default
    )
    {
#if NET6_0_OR_GREATER
        _client.DeflateOptions = _options.EnableCompression
            ? new System.Net.WebSockets.WebSocketDeflateOptions()
            : null;
#endif
        await _client.ConnectAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Asynchronously closes the WebSocket connection.
    /// </summary>
    public async global::System.Threading.Tasks.Task CloseAsync(
        CancellationToken cancellationToken = default
    )
    {
        await _client.CloseAsync(cancellationToken).ConfigureAwait(false);
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
    /// Sends a PublishEvent message to the server
    /// </summary>
    public async global::System.Threading.Tasks.Task Send(
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
    )
    {
        await SendJsonAsync(message, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Options for the API client
    /// </summary>
    public class Options
    {
        /// <summary>
        /// The Websocket URL for the API connection.
        /// </summary>
        public string BaseUrl { get; set; } = "wss://api.hume.ai/v0/evi";

        /// <summary>
        /// Enable per-message deflate compression (RFC 7692). When true, the client sets <c>ClientWebSocketOptions.DangerousDeflateOptions</c> before connecting. Compression is negotiated during the handshake; if the server does not support it, the connection proceeds uncompressed. Default: <c>false</c>.
        /// <para><b>Security warning:</b> do not enable compression when transmitting data containing secrets — compressed encrypted payloads are vulnerable to CRIME/BREACH side-channel attacks. See <see href="https://learn.microsoft.com/dotnet/api/system.net.websockets.clientwebsocketoptions.dangerousdeflateoptions">ClientWebSocketOptions.DangerousDeflateOptions</see> for details.</para>
        /// </summary>
        public bool EnableCompression { get; set; } = false;

        /// <summary>
        /// Optional HTTP/2 handler for multiplexed WebSocket connections (.NET 7+).
        /// </summary>
        public System.Net.Http.HttpMessageInvoker? HttpInvoker { get; set; }

        /// <summary>
        /// Access token used for authenticating the client. If not provided, an `api_key` must be provided to authenticate.
        ///
        /// The access token is generated using both an API key and a Secret key, which provides an additional layer of security compared to using just an API key.
        ///
        /// For more details, refer to the [Authentication Strategies Guide](/docs/introduction/api-key#authentication-strategies).
        /// </summary>
        public string? AccessToken { get; set; }

        /// <summary>
        /// Allows external connections to this chat via the /connect endpoint.
        /// </summary>
        public bool? AllowConnection { get; set; }

        /// <summary>
        /// The unique identifier for an EVI configuration.
        ///
        /// Include this ID in your connection request to equip EVI with the Prompt, Language Model, Voice, and Tools associated with the specified configuration. If omitted, EVI will apply [default configuration settings](/docs/empathic-voice-interface-evi/configuration#default-configuration).
        ///
        /// For help obtaining this ID, see our [Configuration Guide](/docs/empathic-voice-interface-evi/configuration).
        /// </summary>
        public string? ConfigId { get; set; }

        /// <summary>
        /// The version number of the EVI configuration specified by the `config_id`.
        ///
        /// Configs, as well as Prompts and Tools, are versioned. This versioning system supports iterative development, allowing you to progressively refine configurations and revert to previous versions if needed.
        ///
        /// Include this parameter to apply a specific version of an EVI configuration. If omitted, the latest version will be applied.
        /// </summary>
        public int? ConfigVersion { get; set; }

        /// <summary>
        /// The maximum number of chat events to return from chat history. By default, the system returns up to 300 events (100 events per page × 3 pages). Set this parameter to a smaller value to limit the number of events returned.
        /// </summary>
        public int? EventLimit { get; set; }

        /// <summary>
        /// The unique identifier for a Chat Group. Use this field to preserve context from a previous Chat session.
        ///
        /// A Chat represents a single session from opening to closing a WebSocket connection. In contrast, a Chat Group is a series of resumed Chats that collectively represent a single conversation spanning multiple sessions. Each Chat includes a Chat Group ID, which is used to preserve the context of previous Chat sessions when starting a new one.
        ///
        /// Including the Chat Group ID in the `resumed_chat_group_id` query parameter is useful for seamlessly resuming a Chat after unexpected network disconnections and for picking up conversations exactly where you left off at a later time. This ensures preserved context across multiple sessions.
        ///
        /// There are three ways to obtain the Chat Group ID:
        ///
        /// - [Chat Metadata](/reference/empathic-voice-interface-evi/chat/chat#receive.Chat%20Metadata.type): Upon establishing a WebSocket connection with EVI, the user receives a Chat Metadata message. This message contains a `chat_group_id`, which can be used to resume conversations within this chat group in future sessions.
        ///
        /// - [List Chats endpoint](/reference/empathic-voice-interface-evi/chats/list-chats): Use the GET `/v0/evi/chats` endpoint to obtain the Chat Group ID of individual Chat sessions. This endpoint lists all available Chat sessions and their associated Chat Group ID.
        ///
        /// - [List Chat Groups endpoint](/reference/empathic-voice-interface-evi/chat-groups/list-chat-groups): Use the GET `/v0/evi/chat_groups` endpoint to obtain the Chat Group IDs of all Chat Groups associated with an API key. This endpoint returns a list of all available chat groups.
        /// </summary>
        public string? ResumedChatGroupId { get; set; }

        /// <summary>
        /// A flag to enable verbose transcription. Set this query parameter to `"true"` to have unfinalized user transcripts be sent to the client as interim `UserMessage` messages.
        /// </summary>
        public bool? VerboseTranscription { get; set; }

        public string? ApiKey { get; set; }

        public ConnectSessionSettings? SessionSettings { get; set; }

        /// <summary>
        /// Enable or disable automatic reconnection. Default: false.
        /// </summary>
        public bool IsReconnectionEnabled { get; set; } = false;

        /// <summary>
        /// Time to wait before reconnecting if no message comes from the server. Set null to disable. Default: 1 minute.
        /// </summary>
        public TimeSpan? ReconnectTimeout { get; set; } = TimeSpan.FromMinutes(1);

        /// <summary>
        /// Time to wait before reconnecting if the last reconnection attempt failed. Set null to disable. Default: 1 minute.
        /// </summary>
        public TimeSpan? ErrorReconnectTimeout { get; set; } = TimeSpan.FromMinutes(1);

        /// <summary>
        /// Time to wait before reconnecting if the connection is lost with a transient error. Set null to disable (reconnect immediately). Default: null.
        /// </summary>
        public TimeSpan? LostReconnectTimeout { get; set; }

        /// <summary>
        /// Backoff strategy for reconnection delays. Controls interval growth, jitter, and max attempts. Set to null to use fixed-interval reconnection (legacy behavior). Default: exponential backoff, 1s→60s, unlimited attempts, with jitter.
        /// </summary>
        public ReconnectStrategy? ReconnectBackoff { get; set; } = new ReconnectStrategy();
    }
}
