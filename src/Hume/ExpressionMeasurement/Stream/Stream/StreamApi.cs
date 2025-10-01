using System.Net.WebSockets;
using System.Text.Json;
using Hume.Core;
using Hume.Core.Async;
using Hume.Core.Async.Events;
using Hume.Core.Async.Models;

namespace Hume.ExpressionMeasurement.Stream;

public partial class StreamApi : AsyncApi<StreamApi.Options>
{
    public readonly Event<StreamModelPredictions> StreamModelPredictions = new();

    public readonly Event<StreamErrorMessage> StreamErrorMessage = new();

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

    protected override Uri CreateUri()
    {
        return new UriBuilder(BaseUrl.TrimEnd('/') + "/models").Uri;
    }

    protected override void SetConnectionOptions(ClientWebSocketOptions options) { }

    protected override async System.Threading.Tasks.Task OnTextMessage(System.IO.Stream stream)
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

        try
        {
            var message = json.Deserialize<StreamModelPredictions>();
            if (message != null)
            {
                await StreamModelPredictions.RaiseEvent(message).ConfigureAwait(false);
                return;
            }
        }
        catch (Exception)
        {
            // message is not StreamModelPredictions, continue
        }

        try
        {
            var message = json.Deserialize<StreamErrorMessage>();
            if (message != null)
            {
                await StreamErrorMessage.RaiseEvent(message).ConfigureAwait(false);
                return;
            }
        }
        catch (Exception)
        {
            // message is not StreamErrorMessage, continue
        }

        try
        {
            var message = json.Deserialize<StreamWarningMessage>();
            if (message != null)
            {
                await StreamWarningMessage.RaiseEvent(message).ConfigureAwait(false);
                return;
            }
        }
        catch (Exception)
        {
            // message is not StreamWarningMessage, continue
        }

        await ExceptionOccurred
            .RaiseEvent(new Exception($"Unknown message: {json.ToString()}"))
            .ConfigureAwait(false);
    }

    protected override void DisposeEvents()
    {
        StreamModelPredictions.Dispose();
        StreamErrorMessage.Dispose();
        StreamWarningMessage.Dispose();
    }

    public async System.Threading.Tasks.Task Send(StreamModelsEndpointPayload message)
    {
        await SendInstant(JsonUtils.Serialize(message)).ConfigureAwait(false);
    }

    public class Options : AsyncApiOptions
    {
        /// <summary>
        /// The Websocket URL for the API connection.
        /// </summary>
        override public string BaseUrl { get; set; } = "wss://api.hume.ai/v0/stream";
    }
}
