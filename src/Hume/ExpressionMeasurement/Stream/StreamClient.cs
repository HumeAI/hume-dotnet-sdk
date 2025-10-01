using Hume.Core;

namespace Hume.ExpressionMeasurement.Stream;

public partial class StreamClient
{
    private RawClient _client;

    internal StreamClient(RawClient client)
    {
        _client = client;
    }

    public StreamApi CreateStreamApi()
    {
        return new StreamApi(new StreamApi.Options());
    }

    public StreamApi CreateStreamApi(StreamApi.Options options)
    {
        return new StreamApi(options);
    }
}
