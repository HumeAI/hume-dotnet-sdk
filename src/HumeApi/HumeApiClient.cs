using HumeApi.Core;
using HumeApi.EmpathicVoice;
using HumeApi.ExpressionMeasurement;
using HumeApi.Tts;

namespace HumeApi;

public partial class HumeApiClient
{
    private readonly RawClient _client;

    public HumeApiClient(string? apiKey = null, ClientOptions? clientOptions = null)
    {
        var defaultHeaders = new Headers(
            new Dictionary<string, string>()
            {
                { "X-Hume-Api-Key", apiKey },
                { "X-Fern-Language", "C#" },
                { "X-Fern-SDK-Name", "HumeApi" },
                { "X-Fern-SDK-Version", Version.Current },
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
        Tts = new TtsClient(_client);
        EmpathicVoice = new EmpathicVoiceClient(_client);
        ExpressionMeasurement = new ExpressionMeasurementClient(_client);
    }

    public TtsClient Tts { get; }

    public EmpathicVoiceClient EmpathicVoice { get; }

    public ExpressionMeasurementClient ExpressionMeasurement { get; }
}
