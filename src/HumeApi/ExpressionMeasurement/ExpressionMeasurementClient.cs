using HumeApi.Core;
using HumeApi.ExpressionMeasurement.Batch;

namespace HumeApi.ExpressionMeasurement;

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
