using Hume.Core;

namespace Hume.ExpressionMeasurement.Stream;

public partial class StreamClient : IStreamClient
{
    private readonly RawClient _client;

    internal StreamClient(RawClient client)
    {
        _client = client;
    }

    public IStreamApi CreateStreamApi()
    {
        return new StreamApi(
            new StreamApi.Options { BaseUrl = _client.Options.Environment.Stream }
        );
    }

    public IStreamApi CreateStreamApi(StreamApi.Options options)
    {
        return new StreamApi(options);
    }
}
