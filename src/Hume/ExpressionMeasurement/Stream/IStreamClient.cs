namespace Hume.ExpressionMeasurement.Stream;

public partial interface IStreamClient
{
    IStreamApi CreateStreamApi();

    IStreamApi CreateStreamApi(StreamApi.Options options);
}
