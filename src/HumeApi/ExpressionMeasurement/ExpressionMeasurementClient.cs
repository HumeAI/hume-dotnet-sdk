using HumeApi.Core;
using HumeApi.ExpressionMeasurement.Batch;
using HumeApi.ExpressionMeasurement.Stream;

namespace HumeApi.ExpressionMeasurement;

public partial class ExpressionMeasurementClient
{
    private RawClient _client;

    internal ExpressionMeasurementClient(RawClient client)
    {
        _client = client;
        Batch = new BatchClient(_client);
        Stream = new StreamClient(_client);
    }

    public BatchClient Batch { get; }

    public StreamClient Stream { get; }
}
