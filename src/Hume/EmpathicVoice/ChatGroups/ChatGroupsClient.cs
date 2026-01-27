using System.Text.Json;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

public partial class ChatGroupsClient : IChatGroupsClient
{
    private RawClient _client;

    internal ChatGroupsClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Fetches a paginated list of **Chat Groups**.
    /// </summary>
    private WithRawResponseTask<ReturnPagedChatGroups> ListChatGroupsInternalAsync(
        ChatGroupsListChatGroupsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnPagedChatGroups>(
            ListChatGroupsInternalAsyncCore(request, options, cancellationToken)
        );
    }

    private async System.Threading.Tasks.Task<
        WithRawResponse<ReturnPagedChatGroups>
    > ListChatGroupsInternalAsyncCore(
        ChatGroupsListChatGroupsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new Hume.Core.QueryStringBuilder.Builder(capacity: 4)
            .Add("page_number", request.PageNumber)
            .Add("page_size", request.PageSize)
            .Add("ascending_order", request.AscendingOrder)
            .Add("config_id", request.ConfigId)
            .MergeAdditional(options?.AdditionalQueryParameters)
            .Build();
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.Environment.Base,
                    Method = HttpMethod.Get,
                    Path = "v0/evi/chat_groups",
                    QueryString = _queryString,
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
                var responseData = JsonUtils.Deserialize<ReturnPagedChatGroups>(responseBody)!;
                return new WithRawResponse<ReturnPagedChatGroups>()
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
        WithRawResponse<ReturnChatGroupPagedChats>
    > GetChatGroupAsyncCore(
        string id,
        ChatGroupsGetChatGroupRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new Hume.Core.QueryStringBuilder.Builder(capacity: 4)
            .Add("status", request.Status)
            .Add("page_size", request.PageSize)
            .Add("page_number", request.PageNumber)
            .Add("ascending_order", request.AscendingOrder)
            .MergeAdditional(options?.AdditionalQueryParameters)
            .Build();
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.Environment.Base,
                    Method = HttpMethod.Get,
                    Path = string.Format(
                        "v0/evi/chat_groups/{0}",
                        ValueConvert.ToPathParameterString(id)
                    ),
                    QueryString = _queryString,
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
                var responseData = JsonUtils.Deserialize<ReturnChatGroupPagedChats>(responseBody)!;
                return new WithRawResponse<ReturnChatGroupPagedChats>()
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
        WithRawResponse<ReturnChatGroupPagedAudioReconstructions>
    > GetAudioAsyncCore(
        string id,
        ChatGroupsGetAudioRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new Hume.Core.QueryStringBuilder.Builder(capacity: 3)
            .Add("page_number", request.PageNumber)
            .Add("page_size", request.PageSize)
            .Add("ascending_order", request.AscendingOrder)
            .MergeAdditional(options?.AdditionalQueryParameters)
            .Build();
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.Environment.Base,
                    Method = HttpMethod.Get,
                    Path = string.Format(
                        "v0/evi/chat_groups/{0}/audio",
                        ValueConvert.ToPathParameterString(id)
                    ),
                    QueryString = _queryString,
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
                var responseData = JsonUtils.Deserialize<ReturnChatGroupPagedAudioReconstructions>(
                    responseBody
                )!;
                return new WithRawResponse<ReturnChatGroupPagedAudioReconstructions>()
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
    /// Fetches a paginated list of **Chat** events associated with a **Chat Group**.
    /// </summary>
    private WithRawResponseTask<ReturnChatGroupPagedEvents> ListChatGroupEventsInternalAsync(
        string id,
        ChatGroupsListChatGroupEventsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnChatGroupPagedEvents>(
            ListChatGroupEventsInternalAsyncCore(id, request, options, cancellationToken)
        );
    }

    private async System.Threading.Tasks.Task<
        WithRawResponse<ReturnChatGroupPagedEvents>
    > ListChatGroupEventsInternalAsyncCore(
        string id,
        ChatGroupsListChatGroupEventsRequest request,
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
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.Environment.Base,
                    Method = HttpMethod.Get,
                    Path = string.Format(
                        "v0/evi/chat_groups/{0}/events",
                        ValueConvert.ToPathParameterString(id)
                    ),
                    QueryString = _queryString,
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
                var responseData = JsonUtils.Deserialize<ReturnChatGroupPagedEvents>(responseBody)!;
                return new WithRawResponse<ReturnChatGroupPagedEvents>()
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
    /// Fetches a paginated list of **Chat Groups**.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.ChatGroups.ListChatGroupsAsync(
    ///     new ChatGroupsListChatGroupsRequest
    ///     {
    ///         PageNumber = 0,
    ///         PageSize = 1,
    ///         AscendingOrder = true,
    ///         ConfigId = "1b60e1a0-cc59-424a-8d2c-189d354db3f3",
    ///     }
    /// );
    /// </code></example>
    public async System.Threading.Tasks.Task<Pager<ReturnChatGroup>> ListChatGroupsAsync(
        ChatGroupsListChatGroupsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        request = request with { };
        var pager = await OffsetPager<
            ChatGroupsListChatGroupsRequest,
            RequestOptions?,
            ReturnPagedChatGroups,
            int?,
            object,
            ReturnChatGroup
        >
            .CreateInstanceAsync(
                request,
                options,
                async (request, options, cancellationToken) =>
                    await ListChatGroupsInternalAsync(request, options, cancellationToken),
                request => request.PageNumber ?? 0,
                (request, offset) =>
                {
                    request.PageNumber = offset;
                },
                null,
                response => response.ChatGroupsPage?.ToList(),
                null,
                cancellationToken
            )
            .ConfigureAwait(false);
        return pager;
    }

    /// <summary>
    /// Fetches a **ChatGroup** by ID, including a paginated list of **Chats** associated with the **ChatGroup**.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.ChatGroups.GetChatGroupAsync(
    ///     "697056f0-6c7e-487d-9bd8-9c19df79f05f",
    ///     new ChatGroupsGetChatGroupRequest
    ///     {
    ///         PageNumber = 0,
    ///         PageSize = 1,
    ///         AscendingOrder = true,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<ReturnChatGroupPagedChats> GetChatGroupAsync(
        string id,
        ChatGroupsGetChatGroupRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnChatGroupPagedChats>(
            GetChatGroupAsyncCore(id, request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Fetches a paginated list of audio for each **Chat** within the specified **Chat Group**. For more details, see our guide on audio reconstruction [here](/docs/speech-to-speech-evi/faq#can-i-access-the-audio-of-previous-conversations-with-evi).
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.ChatGroups.GetAudioAsync(
    ///     "369846cf-6ad5-404d-905e-a8acb5cdfc78",
    ///     new ChatGroupsGetAudioRequest
    ///     {
    ///         PageNumber = 0,
    ///         PageSize = 10,
    ///         AscendingOrder = true,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<ReturnChatGroupPagedAudioReconstructions> GetAudioAsync(
        string id,
        ChatGroupsGetAudioRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnChatGroupPagedAudioReconstructions>(
            GetAudioAsyncCore(id, request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Fetches a paginated list of **Chat** events associated with a **Chat Group**.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.ChatGroups.ListChatGroupEventsAsync(
    ///     "697056f0-6c7e-487d-9bd8-9c19df79f05f",
    ///     new ChatGroupsListChatGroupEventsRequest
    ///     {
    ///         PageNumber = 0,
    ///         PageSize = 3,
    ///         AscendingOrder = true,
    ///     }
    /// );
    /// </code></example>
    public async System.Threading.Tasks.Task<Pager<ReturnChatEvent>> ListChatGroupEventsAsync(
        string id,
        ChatGroupsListChatGroupEventsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        request = request with { };
        var pager = await OffsetPager<
            ChatGroupsListChatGroupEventsRequest,
            RequestOptions?,
            ReturnChatGroupPagedEvents,
            int?,
            object,
            ReturnChatEvent
        >
            .CreateInstanceAsync(
                request,
                options,
                async (request, options, cancellationToken) =>
                    await ListChatGroupEventsInternalAsync(id, request, options, cancellationToken)
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
}
