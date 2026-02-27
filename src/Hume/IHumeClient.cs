using Hume.EmpathicVoice;
using Hume.ExpressionMeasurement;
using Hume.Tts;

namespace Hume;

public partial interface IHumeClient
{
    public IEmpathicVoiceClient EmpathicVoice { get; }
    public ITtsClient Tts { get; }
    public IExpressionMeasurementClient ExpressionMeasurement { get; }
}
