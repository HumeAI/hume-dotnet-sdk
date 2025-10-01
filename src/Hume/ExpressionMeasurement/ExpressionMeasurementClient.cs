using Hume.Core;
using Hume.ExpressionMeasurement.Batch;
using Hume.ExpressionMeasurement.Stream;

namespace Hume.ExpressionMeasurement;

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
