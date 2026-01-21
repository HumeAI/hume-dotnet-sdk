using Hume.EmpathicVoice;
using Hume.ExpressionMeasurement;
using Hume.Tts;

namespace Hume;

public partial interface IHumeClient
{
    public TtsClient Tts { get; }
    public EmpathicVoiceClient EmpathicVoice { get; }
    public ExpressionMeasurementClient ExpressionMeasurement { get; }
}
