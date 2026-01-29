using System.Text.Json;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

public partial class ChatsClient : IChatsClient
{
    private RawClient _client;

    internal ChatsClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Fetches a paginated list of **Chats**.
    /// </summary>
    private WithRawResponseTask<ReturnPagedChats> ListChatsInternalAsync(
        ChatsListChatsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnPagedChats>(
            ListChatsInternalAsyncCore(request, options, cancellationToken)
        );
    }

    private async System.Threading.Tasks.Task<
        WithRawResponse<ReturnPagedChats>
    > ListChatsInternalAsyncCore(
        ChatsListChatsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new Hume.Core.QueryStringBuilder.Builder(capacity: 5)
            .Add("page_number", request.PageNumber)
            .Add("page_size", request.PageSize)
            .Add("ascending_order", request.AscendingOrder)
            .Add("config_id", request.ConfigId)
            .Add("status", request.Status)
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
                    Path = "v0/evi/chats",
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
                var responseData = JsonUtils.Deserialize<ReturnPagedChats>(responseBody)!;
                return new WithRawResponse<ReturnPagedChats>()
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

    /// <summary>
    /// Fetches a paginated list of **Chat** events.
    /// </summary>
    private WithRawResponseTask<ReturnChatPagedEvents> ListChatEventsInternalAsync(
        string id,
        ChatsListChatEventsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnChatPagedEvents>(
            ListChatEventsInternalAsyncCore(id, request, options, cancellationToken)
        );
    }

    private async System.Threading.Tasks.Task<
        WithRawResponse<ReturnChatPagedEvents>
    > ListChatEventsInternalAsyncCore(
        string id,
        ChatsListChatEventsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new Hume.Core.QueryStringBuilder.Builder(capacity: 3)
            .Add("page_size", request.PageSize)
            .Add("page_number", request.PageNumber)
            .Add("ascending_order", request.AscendingOrder)
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
                    Path = string.Format(
                        "v0/evi/chats/{0}",
                        ValueConvert.ToPathParameterString(id)
                    ),
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
                var responseData = JsonUtils.Deserialize<ReturnChatPagedEvents>(responseBody)!;
                return new WithRawResponse<ReturnChatPagedEvents>()
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

    private async System.Threading.Tasks.Task<
        WithRawResponse<ReturnChatAudioReconstruction>
    > GetAudioAsyncCore(
        string id,
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
                    Method = HttpMethod.Get,
                    Path = string.Format(
                        "v0/evi/chats/{0}/audio",
                        ValueConvert.ToPathParameterString(id)
                    ),
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
                var responseData = JsonUtils.Deserialize<ReturnChatAudioReconstruction>(
                    responseBody
                )!;
                return new WithRawResponse<ReturnChatAudioReconstruction>()
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

    /// <summary>
    /// Fetches a paginated list of **Chats**.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Chats.ListChatsAsync(
    ///     new ChatsListChatsRequest
    ///     {
    ///         PageNumber = 0,
    ///         PageSize = 1,
    ///         AscendingOrder = true,
    ///     }
    /// );
    /// </code></example>
    public async System.Threading.Tasks.Task<Pager<ReturnChat>> ListChatsAsync(
        ChatsListChatsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        request = request with { };
        var pager = await OffsetPager<
            ChatsListChatsRequest,
            RequestOptions?,
            ReturnPagedChats,
            int,
            object,
            ReturnChat
        >
            .CreateInstanceAsync(
                request,
                options,
                async (request, options, cancellationToken) =>
                    await ListChatsInternalAsync(request, options, cancellationToken),
                request => request.PageNumber ?? 0,
                (request, offset) =>
                {
                    request.PageNumber = offset;
                },
                null,
                response => response.ChatsPage?.ToList(),
                null,
                cancellationToken
            )
            .ConfigureAwait(false);
        return pager;
    }

    /// <summary>
    /// Fetches a paginated list of **Chat** events.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Chats.ListChatEventsAsync(
    ///     "470a49f6-1dec-4afe-8b61-035d3b2d63b0",
    ///     new ChatsListChatEventsRequest
    ///     {
    ///         PageNumber = 0,
    ///         PageSize = 3,
    ///         AscendingOrder = true,
    ///     }
    /// );
    /// </code></example>
    public async System.Threading.Tasks.Task<Pager<ReturnChatEvent>> ListChatEventsAsync(
        string id,
        ChatsListChatEventsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        request = request with { };
        var pager = await OffsetPager<
            ChatsListChatEventsRequest,
            RequestOptions?,
            ReturnChatPagedEvents,
            int,
            object,
            ReturnChatEvent
        >
            .CreateInstanceAsync(
                request,
                options,
                async (request, options, cancellationToken) =>
                    await ListChatEventsInternalAsync(id, request, options, cancellationToken)
                        .ConfigureAwait(false),
                request => request.PageNumber ?? 0,
                (request, offset) =>
                {
                    request.PageNumber = offset;
                },
                null,
                response => response.EventsPage?.ToList(),
                null,
                cancellationToken
            )
            .ConfigureAwait(false);
        return pager;
    }

    /// <summary>
    /// Fetches the audio of a previous **Chat**. For more details, see our guide on audio reconstruction [here](/docs/speech-to-speech-evi/faq#can-i-access-the-audio-of-previous-conversations-with-evi).
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Chats.GetAudioAsync("470a49f6-1dec-4afe-8b61-035d3b2d63b0");
    /// </code></example>
    public WithRawResponseTask<ReturnChatAudioReconstruction> GetAudioAsync(
        string id,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnChatAudioReconstruction>(
            GetAudioAsyncCore(id, options, cancellationToken)
        );
    }
}
