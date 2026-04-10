using Hume.EmpathicVoice;
using Hume.ExpressionMeasurement;
using Hume.Tts;

namespace Hume;

public partial interface IHumeClient
{
    public ITtsClient Tts { get; }
    public IEmpathicVoiceClient EmpathicVoice { get; }
    public IExpressionMeasurementClient ExpressionMeasurement { get; }
}
