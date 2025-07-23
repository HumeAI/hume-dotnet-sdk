using System.Net.Http;
using System.Text.Json;
using System.Threading;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.Tts;

public partial class TtsClient
{
    private RawClient _client;

    internal TtsClient(RawClient client)
    {
        _client = client;
        Voices = new VoicesClient(_client);
    }

    public VoicesClient Voices { get; }

    /// <summary>
    /// Synthesizes one or more input texts into speech using the specified voice. If no voice is provided, a novel voice will be generated dynamically. Optionally, additional context can be included to influence the speech's style and prosody.
    ///
    /// The response includes the base64-encoded audio and metadata in JSON format.
    /// </summary>
    /// <example><code>
    /// await client.Tts.SynthesizeJsonAsync(
    ///     new PostedTts
    ///     {
    ///         Utterances = new List&lt;PostedUtterance&gt;()
    ///         {
    ///             new PostedUtterance
    ///             {
    ///                 Text =
    ///                     "Beauty is no quality in things themselves: It exists merely in the mind which contemplates them.",
    ///                 Description =
    ///                     "Middle-aged masculine voice with a clear, rhythmic Scots lilt, rounded vowels, and a warm, steady tone with an articulate, academic quality.",
    ///             },
    ///         },
    ///         Context = new PostedContextWithUtterances
    ///         {
    ///             Utterances = new List&lt;PostedUtterance&gt;()
    ///             {
    ///                 new PostedUtterance
    ///                 {
    ///                     Text = "How can people see beauty so differently?",
    ///                     Description =
    ///                         "A curious student with a clear and respectful tone, seeking clarification on Hume's ideas with a straightforward question.",
    ///                 },
    ///             },
    ///         },
    ///         Format = new Format(new Format.Mp3(new FormatMp3())),
    ///         NumGenerations = 1,
    ///     }
    /// );
    /// </code></example>
    public async Task<ReturnTts> SynthesizeJsonAsync(
        PostedTts request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        if (request.AccessToken != null)
        {
            _query["access_token"] = request.AccessToken;
        }
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Post,
                    Path = "v0/tts",
                    Body = request,
                    Query = _query,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<ReturnTts>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new HumeApiException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                switch (response.StatusCode)
                {
                    case 422:
                        throw new UnprocessableEntityError(
                            JsonUtils.Deserialize<HttpValidationError>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new HumeApiApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }
}
