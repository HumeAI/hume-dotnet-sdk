using System.Text.Json;
using Hume;
using Hume.Core;

namespace Hume.Tts;

public partial class VoicesClient : IVoicesClient
{
    private RawClient _client;

    internal VoicesClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Lists voices you have saved in your account, or voices from the [Voice Library](https://app.hume.ai/tts/voice-library).
    /// </summary>
    private WithRawResponseTask<ReturnPagedVoices> ListInternalAsync(
        VoicesListRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnPagedVoices>(
            ListInternalAsyncCore(request, options, cancellationToken)
        );
    }

    private async System.Threading.Tasks.Task<
        WithRawResponse<ReturnPagedVoices>
    > ListInternalAsyncCore(
        VoicesListRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new Hume.Core.QueryStringBuilder.Builder(capacity: 5)
            .Add("provider", request.Provider)
            .Add("page_number", request.PageNumber)
            .Add("page_size", request.PageSize)
            .Add("ascending_order", request.AscendingOrder)
            .Add("filter_tag", request.FilterTag)
            .MergeAdditional(options?.AdditionalQueryParameters)
            .Build();
        var _headers = await new Hume.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.Environment.Base,
                    Method = HttpMethod.Get,
                    Path = "v0/tts/voices",
                    QueryString = _queryString,
                    Headers = _headers,
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
                var responseData = JsonUtils.Deserialize<ReturnPagedVoices>(responseBody)!;
                return new WithRawResponse<ReturnPagedVoices>()
                {
                    Data = responseData,
                    RawResponse = new RawResponse()
                    {
                        StatusCode = response.Raw.StatusCode,
                        Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                        Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                    },
                };
            }
            catch (JsonException e)
            {
                throw new HumeClientApiException(
                    "Failed to deserialize response",
                    response.StatusCode,
                    responseBody,
                    e
                );
            }
        }
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                switch (response.StatusCode)
                {
                    case 400:
                        throw new BadRequestError(
                            JsonUtils.Deserialize<ErrorResponse>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new HumeClientApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async System.Threading.Tasks.Task<WithRawResponse<ReturnVoice>> CreateAsyncCore(
        PostedVoice request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _headers = await new Hume.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.Environment.Base,
                    Method = HttpMethod.Post,
                    Path = "v0/tts/voices",
                    Body = request,
                    Headers = _headers,
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
                var responseData = JsonUtils.Deserialize<ReturnVoice>(responseBody)!;
                return new WithRawResponse<ReturnVoice>()
                {
                    Data = responseData,
                    RawResponse = new RawResponse()
                    {
                        StatusCode = response.Raw.StatusCode,
                        Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                        Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                    },
                };
            }
            catch (JsonException e)
            {
                throw new HumeClientApiException(
                    "Failed to deserialize response",
                    response.StatusCode,
                    responseBody,
                    e
                );
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
            throw new HumeClientApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    /// <summary>
    /// Lists voices you have saved in your account, or voices from the [Voice Library](https://app.hume.ai/tts/voice-library).
    /// </summary>
    /// <example><code>
    /// await client.Tts.Voices.ListAsync(
    ///     new VoicesListRequest { Provider = Hume.Tts.VoiceProvider.CustomVoice }
    /// );
    /// </code></example>
    public async System.Threading.Tasks.Task<Pager<ReturnVoice>> ListAsync(
        VoicesListRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        request = request with { };
        var pager = await OffsetPager<
            VoicesListRequest,
            RequestOptions?,
            ReturnPagedVoices,
            int,
            object,
            ReturnVoice
        >
            .CreateInstanceAsync(
                request,
                options,
                async (request, options, cancellationToken) =>
                    await ListInternalAsync(request, options, cancellationToken),
                request => request.PageNumber ?? 0,
                (request, offset) =>
                {
                    request.PageNumber = offset;
                },
                null,
                response => response.VoicesPage?.ToList(),
                null,
                cancellationToken
            )
            .ConfigureAwait(false);
        return pager;
    }

    /// <summary>
    /// Saves a new custom voice to your account using the specified TTS generation ID.
    ///
    /// Once saved, this voice can be reused in subsequent TTS requests, ensuring consistent speech style and prosody. For more details on voice creation, see the [Voices Guide](/docs/text-to-speech-tts/voices).
    /// </summary>
    /// <example><code>
    /// await client.Tts.Voices.CreateAsync(
    ///     new PostedVoice { GenerationId = "795c949a-1510-4a80-9646-7d0863b023ab", Name = "David Hume" }
    /// );
    /// </code></example>
    public WithRawResponseTask<ReturnVoice> CreateAsync(
        PostedVoice request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnVoice>(
            CreateAsyncCore(request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Deletes a previously generated custom voice.
    /// </summary>
    /// <example><code>
    /// await client.Tts.Voices.DeleteAsync(new VoicesDeleteRequest { Name = "David Hume" });
    /// </code></example>
    public async System.Threading.Tasks.Task DeleteAsync(
        VoicesDeleteRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new Hume.Core.QueryStringBuilder.Builder(capacity: 1)
            .Add("name", request.Name)
            .MergeAdditional(options?.AdditionalQueryParameters)
            .Build();
        var _headers = await new Hume.Core.HeadersBuilder.Builder()
            .Add(_client.Options.Headers)
            .Add(_client.Options.AdditionalHeaders)
            .Add(options?.AdditionalHeaders)
            .BuildAsync()
            .ConfigureAwait(false);
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.Environment.Base,
                    Method = HttpMethod.Delete,
                    Path = "v0/tts/voices",
                    QueryString = _queryString,
                    Headers = _headers,
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            return;
        }
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                switch (response.StatusCode)
                {
                    case 400:
                        throw new BadRequestError(
                            JsonUtils.Deserialize<ErrorResponse>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new HumeClientApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }
}
