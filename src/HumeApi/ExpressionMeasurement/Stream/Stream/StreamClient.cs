using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Stream;

public partial class StreamClient
{
    private RawClient _client;

    internal StreamClient(RawClient client)
    {
        _client = client;
    }
}
