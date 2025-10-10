using System.Net.WebSockets;
using System.Text.Json;
using Hume.Core;
using Hume.Core.Async;
using Hume.Core.Async.Events;
using Hume.Core.Async.Models;

namespace Hume.ExpressionMeasurement.Stream;

public partial class StreamApi : AsyncApi<StreamApi.Options>
{
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
        : base(options) { }

    /// <summary>
    /// Creates the Uri for the websocket connection from the BaseUrl and parameters
    /// </summary>
    protected override Uri CreateUri()
    {
        var uri = new UriBuilder(BaseUrl);
        uri.Path = $"{uri.Path.TrimEnd('/')}/models";
        return uri.Uri;
    }

    protected override void SetConnectionOptions(ClientWebSocketOptions options) { }

    /// <summary>
    /// Dispatches incoming WebSocket messages
    /// </summary>
    protected async override System.Threading.Tasks.Task OnTextMessage(System.IO.Stream stream)
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
    /// Disposes of event subscriptions
    /// </summary>
    protected override void DisposeEvents()
    {
        StreamModelPredictions.Dispose();
        StreamErrorMessage.Dispose();
        StreamWarningMessage.Dispose();
    }

    /// <summary>
    /// Sends a StreamModelsEndpointPayload message to the server
    /// </summary>
    public async System.Threading.Tasks.Task Send(StreamModelsEndpointPayload message)
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
        override public string BaseUrl { get; set; } = "wss://api.hume.ai/v0/stream";
    }
}
