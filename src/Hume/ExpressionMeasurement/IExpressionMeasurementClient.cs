using Hume.ExpressionMeasurement.Batch;
using Hume.ExpressionMeasurement.Stream;

namespace Hume.ExpressionMeasurement;

public partial interface IExpressionMeasurementClient
{
    public BatchClient Batch { get; }
    public StreamClient Stream { get; }
}
