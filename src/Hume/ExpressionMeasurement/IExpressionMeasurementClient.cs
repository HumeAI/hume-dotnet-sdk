using Hume.ExpressionMeasurement.Batch;
using Hume.ExpressionMeasurement.Stream;

namespace Hume.ExpressionMeasurement;

public partial interface IExpressionMeasurementClient
{
    public IBatchClient Batch { get; }
    public IStreamClient Stream { get; }
}
