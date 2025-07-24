using System.Net.Http;
using System.Text.Json;
using System.Threading;
using global::System.Threading.Tasks;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.Tts;

public partial class TtsClient
{
    /// <summary>
    /// Synthesizes one or more input texts into speech using the specified voice. If no voice is provided, a novel voice will be generated dynamically. Optionally, additional context can be included to influence the speech's style and prosody.
    ///
    /// The response contains the generated audio file in the requested format.
    /// </summary>
    /// <example><code>
    /// await client.Tts.SynthesizeFileAsync(
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
    ///         Context = new PostedContextWithGenerationId
    ///         {
    ///             GenerationId = "09ad914d-8e7f-40f8-a279-e34f07f7dab2",
    ///         },
    ///         Format = new Format(new Format.Mp3(new FormatMp3())),
    ///         NumGenerations = 1,
    ///     }
    /// );
    /// </code></example>
    public async global::System.Threading.Tasks.Task<Stream> SynthesizeFileAsync(
        PostedTts request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Post,
                    Path = "v0/tts/file",
                    Body = request,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        {
            // return the stream if it is successful 
            if (response.StatusCode is >= 200 and < 400)
            {
                return await response.Raw.Content.ReadAsStreamAsync();
            }

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

    /// <summary>
    /// Streams synthesized speech using the specified voice. If no voice is provided, a novel voice will be generated dynamically. Optionally, additional context can be included to influence the speech's style and prosody.
    /// </summary>
    /// <example><code>
    /// await client.Tts.SynthesizeFileStreamingAsync(
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
    ///         Context = new PostedContextWithGenerationId
    ///         {
    ///             GenerationId = "09ad914d-8e7f-40f8-a279-e34f07f7dab2",
    ///         },
    ///         Format = new Format(new Format.Mp3(new FormatMp3())),
    ///         NumGenerations = 1,
    ///     }
    /// );
    /// </code></example>
    public async global::System.Threading.Tasks.Task<Stream> SynthesizeFileStreamingAsync(
        PostedTts request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Post,
                    Path = "v0/tts/stream/file",
                    Body = request,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        {
            // return the stream if it is successful 
            if (response.StatusCode is >= 200 and < 400)
            {
                return await response.Raw.Content.ReadAsStreamAsync();
            }

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

    /// <summary>
    /// Streams synthesized speech using the specified voice. If no voice is provided, a novel voice will be generated dynamically. Optionally, additional context can be included to influence the speech's style and prosody.
    ///
    /// The response is a stream of JSON objects including audio encoded in base64.
    /// </summary>
    /// <example><code>
    /// await client.Tts.SynthesizeJsonStreamingAsync(
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
    ///     }
    /// );
    /// </code></example>
    public async IAsyncEnumerable<SnippetAudioChunk> SynthesizeJsonStreamingAsync(
        PostedTts request,
        RequestOptions? options = null,
        [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Post,
                    Path = "v0/tts/stream/json",
                    Body = request,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        {
            if (response.StatusCode is >= 200 and < 400)
            {
                string? line;
                using var reader = new StreamReader(await response.Raw.Content.ReadAsStreamAsync());
                while (!string.IsNullOrEmpty(line = await reader.ReadLineAsync()))
                {
                    SnippetAudioChunk? chunk = null;
                    try
                    {
                        chunk = JsonUtils.Deserialize<SnippetAudioChunk>(line);
                    }
                    catch (JsonException)
                    {
                        // unable to map error response, throwing generic error
                        throw new HumeApiApiException(
                            $"Error with status code {response.StatusCode}",
                            response.StatusCode,
                            line
                        );
                    }
                    if (chunk is not null)
                    {
                        yield return chunk;
                    }
                }
                yield break;
            }

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