using System.Net.WebSockets;
using System.Text.Json;
using Hume.Core;
using Hume.Core.Async;
using Hume.Core.Async.Events;
using Hume.Core.Async.Models;

namespace Hume.Tts;

/// <summary>
/// Generate emotionally expressive speech.
/// </summary>
public partial class StreamInputApi : AsyncApi<StreamInputApi.Options>
{
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
        : base(options) { }

    /// <summary>
    /// Access token used for authenticating the client. If not provided, an `api_key` must be provided to authenticate.
    ///
    /// The access token is generated using both an API key and a Secret key, which provides an additional layer of security compared to using just an API key.
    ///
    /// For more details, refer to the [Authentication Strategies Guide](/docs/introduction/api-key#authentication-strategies).
    /// </summary>
    public string? AccessToken
    {
        get => ApiOptions.AccessToken;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.AccessToken),
                ApiOptions.AccessToken = value
            );
    }

    /// <summary>
    /// The ID of a prior TTS generation to use as context for generating consistent speech style and prosody across multiple requests. Including context may increase audio generation times.
    /// </summary>
    public string? ContextGenerationId
    {
        get => ApiOptions.ContextGenerationId;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.ContextGenerationId),
                ApiOptions.ContextGenerationId = value
            );
    }

    /// <summary>
    /// The format to be used for audio generation.
    /// </summary>
    public AudioFormatType? FormatType
    {
        get => ApiOptions.FormatType;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.FormatType),
                ApiOptions.FormatType = value
            );
    }

    /// <summary>
    /// The set of timestamp types to include in the response.
    /// </summary>
    public TimestampType? IncludeTimestampTypes
    {
        get => ApiOptions.IncludeTimestampTypes;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.IncludeTimestampTypes),
                ApiOptions.IncludeTimestampTypes = value
            );
    }

    /// <summary>
    /// Enables ultra-low latency streaming, significantly reducing the time until the first audio chunk is received. Recommended for real-time applications requiring immediate audio playback. For further details, see our documentation on [instant mode](/docs/text-to-speech-tts/overview#ultra-low-latency-streaming-instant-mode).
    /// </summary>
    public bool? InstantMode
    {
        get => ApiOptions.InstantMode;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.InstantMode),
                ApiOptions.InstantMode = value
            );
    }

    /// <summary>
    /// If enabled, no binary websocket messages will be sent to the client.
    /// </summary>
    public bool? NoBinary
    {
        get => ApiOptions.NoBinary;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.NoBinary),
                ApiOptions.NoBinary = value
            );
    }

    /// <summary>
    /// If enabled, the audio for all the chunks of a generation, once concatenated together, will constitute a single audio file. Otherwise, if disabled, each chunk's audio will be its own audio file, each with its own headers (if applicable).
    /// </summary>
    public bool? StripHeaders
    {
        get => ApiOptions.StripHeaders;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.StripHeaders),
                ApiOptions.StripHeaders = value
            );
    }

    /// <summary>
    /// The version of the Octave Model to use. 1 for the legacy model, 2 for the new model.
    /// </summary>
    public OctaveVersion? Version
    {
        get => ApiOptions.Version;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.Version),
                ApiOptions.Version = value
            );
    }

    /// <summary>
    /// API key used for authenticating the client. If not provided, an `access_token` must be provided to authenticate.
    ///
    /// For more details, refer to the [Authentication Strategies Guide](/docs/introduction/api-key#authentication-strategies).
    /// </summary>
    public string? ApiKey
    {
        get => ApiOptions.ApiKey;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.ApiKey),
                ApiOptions.ApiKey = value
            );
    }

    /// <summary>
    /// Creates the Uri for the websocket connection from the BaseUrl and parameters
    /// </summary>
    protected override Uri CreateUri()
    {
        var uri = new UriBuilder(BaseUrl)
        {
            Query = new Query()
            {
                { "access_token", AccessToken },
                { "context_generation_id", ContextGenerationId },
                { "format_type", FormatType },
                { "include_timestamp_types", IncludeTimestampTypes },
                { "instant_mode", InstantMode },
                { "no_binary", NoBinary },
                { "strip_headers", StripHeaders },
                { "version", Version },
                { "api_key", ApiKey },
            },
        };
        uri.Path = $"{uri.Path.TrimEnd('/')}/stream/input";
        return uri.Uri;
    }

    protected override void SetConnectionOptions(ClientWebSocketOptions options) { }

    /// <summary>
    /// Dispatches incoming WebSocket messages
    /// </summary>
    protected async override System.Threading.Tasks.Task OnTextMessage(Stream stream)
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
    /// Disposes of event subscriptions
    /// </summary>
    protected override void DisposeEvents()
    {
        SnippetAudioChunk.Dispose();
        TimestampMessage.Dispose();
    }

    /// <summary>
    /// Sends a PublishTts message to the server
    /// </summary>
    public async System.Threading.Tasks.Task Send(PublishTts message)
    {
        await SendInstant(JsonUtils.Serialize(message)).ConfigureAwait(false);
    }

    /// <summary>
    /// Options for the API client
    /// </summary>
    public class Options : AsyncApiOptions
    {
        /// <summary>
        /// The Websocket URL for the API connection.
        /// </summary>
        override public string BaseUrl { get; set; } = "wss://api.hume.ai/v0/tts";

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
