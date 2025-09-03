using Hume.Core;
using Hume.ExpressionMeasurement.Batch;

namespace Hume.ExpressionMeasurement;

public partial class ExpressionMeasurementClient
{
    private RawClient _client;

    internal ExpressionMeasurementClient(RawClient client)
    {
        _client = client;
        Batch = new BatchClient(_client);
    }

    public BatchClient Batch { get; }
}
