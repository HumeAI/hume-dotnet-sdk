using Hume.Core;
using Hume.EmpathicVoice;
using Hume.ExpressionMeasurement;
using Hume.Tts;

namespace Hume;

public partial class HumeClient
{
    private readonly RawClient _client;

    public HumeClient(string? apiKey, ClientOptions? clientOptions = null)
    {
        var defaultHeaders = new Headers(
            new Dictionary<string, string>()
            {
                { "X-Hume-Api-Key", apiKey ?? "" },
                { "X-Fern-Language", "C#" },
                { "X-Fern-SDK-Name", "Hume" },
                { "X-Fern-SDK-Version", Version.Current },
                { "User-Agent", "Hume/0.2.3" },
            }
        );
        clientOptions ??= new ClientOptions();
        foreach (var header in defaultHeaders)
        {
            if (!clientOptions.Headers.ContainsKey(header.Key))
            {
                clientOptions.Headers[header.Key] = header.Value;
            }
        }
        _client = new RawClient(clientOptions);
        EmpathicVoice = new EmpathicVoiceClient(_client);
        Tts = new TtsClient(_client);
        ExpressionMeasurement = new ExpressionMeasurementClient(_client);
    }

    public EmpathicVoiceClient EmpathicVoice { get; }

    public TtsClient Tts { get; }

    public ExpressionMeasurementClient ExpressionMeasurement { get; }
}
