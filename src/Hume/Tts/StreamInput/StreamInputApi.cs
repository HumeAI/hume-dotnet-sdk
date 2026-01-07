using System.ComponentModel;
using System.Net.WebSockets;
using System.Text.Json;
using Hume.Core;
using Hume.Core.WebSockets;

namespace Hume.Tts;

/// <summary>
/// Generate emotionally expressive speech.
/// </summary>
public partial class StreamInputApi : IAsyncDisposable, IDisposable, INotifyPropertyChanged
{
    private readonly Options _options;
    private readonly WebSocketClient _client;

    /// <summary>
    /// Event handler for SnippetAudioChunk.
    /// Use SnippetAudioChunk.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<SnippetAudioChunk> SnippetAudioChunk = new();

    /// <summary>
    /// Event handler for TimestampMessage.
    /// Use TimestampMessage.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<TimestampMessage> TimestampMessage = new();

    /// <summary>
    /// Default constructor
    /// </summary>
    public StreamInputApi()
        : this(new StreamInputApi.Options()) { }

    /// <summary>
    /// Constructor with options
    /// </summary>
    public StreamInputApi(StreamInputApi.Options options)
    {
        _options = options;

        var uriBuilder = new UriBuilder(_options.BaseUrl)
        {
            Query = new Query()
            {
                { "access_token", _options.AccessToken },
                { "context_generation_id", _options.ContextGenerationId },
                { "format_type", _options.FormatType },
                { "include_timestamp_types", _options.IncludeTimestampTypes },
                { "instant_mode", _options.InstantMode },
                { "no_binary", _options.NoBinary },
                { "strip_headers", _options.StripHeaders },
                { "version", _options.Version },
                { "api_key", _options.ApiKey },
            },
        };
        uriBuilder.Path = $"{uriBuilder.Path.TrimEnd('/')}/stream/input";

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
    private async Task OnTextMessage(Stream stream)
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
            if (JsonUtils.TryDeserialize(json, out SnippetAudioChunk? message))
            {
                await SnippetAudioChunk.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out TimestampMessage? message))
            {
                await TimestampMessage.RaiseEvent(message!).ConfigureAwait(false);
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
        SnippetAudioChunk.Dispose();
        TimestampMessage.Dispose();
    }

    /// <summary>
    /// Asynchronously disposes the StreamInputApi instance, closing any active connections and cleaning up resources.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await _client.DisposeAsync();
        DisposeEvents();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Synchronously disposes the StreamInputApi instance, closing any active connections and cleaning up resources.
    /// </summary>
    public void Dispose()
    {
        _client.Dispose();
        DisposeEvents();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Sends a PublishTts message to the server
    /// </summary>
    public async System.Threading.Tasks.Task Send(PublishTts message)
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
        public string BaseUrl { get; set; } = "wss://api.hume.ai/v0/tts";

        /// <summary>
        /// Access token used for authenticating the client. If not provided, an `api_key` must be provided to authenticate.
        ///
        /// The access token is generated using both an API key and a Secret key, which provides an additional layer of security compared to using just an API key.
        ///
        /// For more details, refer to the [Authentication Strategies Guide](/docs/introduction/api-key#authentication-strategies).
        /// </summary>
        public string? AccessToken { get; set; }

        /// <summary>
        /// The ID of a prior TTS generation to use as context for generating consistent speech style and prosody across multiple requests. Including context may increase audio generation times.
        /// </summary>
        public string? ContextGenerationId { get; set; }

        /// <summary>
        /// The format to be used for audio generation.
        /// </summary>
        public AudioFormatType? FormatType { get; set; }

        /// <summary>
        /// The set of timestamp types to include in the response.
        /// </summary>
        public TimestampType? IncludeTimestampTypes { get; set; }

        /// <summary>
        /// Enables ultra-low latency streaming, significantly reducing the time until the first audio chunk is received. Recommended for real-time applications requiring immediate audio playback. For further details, see our documentation on [instant mode](/docs/text-to-speech-tts/overview#ultra-low-latency-streaming-instant-mode).
        /// </summary>
        public bool? InstantMode { get; set; }

        /// <summary>
        /// If enabled, no binary websocket messages will be sent to the client.
        /// </summary>
        public bool? NoBinary { get; set; }

        /// <summary>
        /// If enabled, the audio for all the chunks of a generation, once concatenated together, will constitute a single audio file. Otherwise, if disabled, each chunk's audio will be its own audio file, each with its own headers (if applicable).
        /// </summary>
        public bool? StripHeaders { get; set; }

        /// <summary>
        /// The version of the Octave Model to use. 1 for the legacy model, 2 for the new model.
        /// </summary>
        public OctaveVersion? Version { get; set; }

        /// <summary>
        /// API key used for authenticating the client. If not provided, an `access_token` must be provided to authenticate.
        ///
        /// For more details, refer to the [Authentication Strategies Guide](/docs/introduction/api-key#authentication-strategies).
        /// </summary>
        public string? ApiKey { get; set; }
    }
}
