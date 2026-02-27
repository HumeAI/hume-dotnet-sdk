using Hume.Core;
using Hume.EmpathicVoice;
using Hume.ExpressionMeasurement;
using Hume.Tts;

namespace Hume;

public partial class HumeClient : IHumeClient
{
    private readonly RawClient _client;

    public HumeClient(string? apiKey = null, ClientOptions? clientOptions = null)
    {
        clientOptions ??= new ClientOptions();
        var platformHeaders = new Headers(
            new Dictionary<string, string>()
            {
                { "X-Fern-Language", "C#" },
                { "X-Fern-SDK-Name", "Hume" },
                { "X-Fern-SDK-Version", Version.Current },
                { "User-Agent", "Hume/0.2.5" },
            }
        );
        foreach (var header in platformHeaders)
        {
            if (!clientOptions.Headers.ContainsKey(header.Key))
            {
                clientOptions.Headers[header.Key] = header.Value;
            }
        }
        var clientOptionsWithAuth = clientOptions.Clone();
        var authHeaders = new Headers(
            new Dictionary<string, string>() { { "X-Hume-Api-Key", apiKey ?? "" } }
        );
        foreach (var header in authHeaders)
        {
            clientOptionsWithAuth.Headers[header.Key] = header.Value;
        }
        _client = new RawClient(clientOptionsWithAuth);
        Tts = new TtsClient(_client);
        EmpathicVoice = new EmpathicVoiceClient(_client);
        ExpressionMeasurement = new ExpressionMeasurementClient(_client);
    }

    public ITtsClient Tts { get; }

    public IEmpathicVoiceClient EmpathicVoice { get; }

    public IExpressionMeasurementClient ExpressionMeasurement { get; }
}
