using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

public partial class ChatGroupsClient
{
    private RawClient _client;

    internal ChatGroupsClient(RawClient client)
    {
        _client = client;
    }

    private async System.Threading.Tasks.Task<ReturnPagedChatGroups> ListChatGroupsInternalAsync(
        ChatGroupsListChatGroupsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        if (request.PageNumber != null)
        {
            _query["page_number"] = request.PageNumber.Value.ToString();
        }
        if (request.PageSize != null)
        {
            _query["page_size"] = request.PageSize.Value.ToString();
        }
        if (request.AscendingOrder != null)
        {
            _query["ascending_order"] = JsonUtils.Serialize(request.AscendingOrder.Value);
        }
        if (request.ConfigId != null)
        {
            _query["config_id"] = request.ConfigId;
        }
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.Environment.Base,
                    Method = HttpMethod.Get,
                    Path = "v0/evi/chat_groups",
                    Query = _query,
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
                return JsonUtils.Deserialize<ReturnPagedChatGroups>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new HumeClientException("Failed to deserialize response", e);
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

    private async System.Threading.Tasks.Task<ReturnChatGroupPagedEvents> ListChatGroupEventsInternalAsync(
        string id,
        ChatGroupsListChatGroupEventsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        if (request.PageSize != null)
        {
            _query["page_size"] = request.PageSize.Value.ToString();
        }
        if (request.PageNumber != null)
        {
            _query["page_number"] = request.PageNumber.Value.ToString();
        }
        if (request.AscendingOrder != null)
        {
            _query["ascending_order"] = JsonUtils.Serialize(request.AscendingOrder.Value);
        }
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
                    Query = _query,
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
                return JsonUtils.Deserialize<ReturnChatGroupPagedEvents>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new HumeClientException("Failed to deserialize response", e);
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
                ListChatGroupsInternalAsync,
                request => request?.PageNumber ?? 0,
                (request, offset) =>
                {
                    request.PageNumber = offset;
                },
                null,
                response => response?.ChatGroupsPage?.ToList(),
                null,
                cancellationToken
            )
            .ConfigureAwait(false);
        return pager;
    }

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
    public async System.Threading.Tasks.Task<ReturnChatGroupPagedChats> GetChatGroupAsync(
        string id,
        ChatGroupsGetChatGroupRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        if (request.Status != null)
        {
            _query["status"] = request.Status;
        }
        if (request.PageSize != null)
        {
            _query["page_size"] = request.PageSize.Value.ToString();
        }
        if (request.PageNumber != null)
        {
            _query["page_number"] = request.PageNumber.Value.ToString();
        }
        if (request.AscendingOrder != null)
        {
            _query["ascending_order"] = JsonUtils.Serialize(request.AscendingOrder.Value);
        }
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
                    Query = _query,
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
                return JsonUtils.Deserialize<ReturnChatGroupPagedChats>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new HumeClientException("Failed to deserialize response", e);
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
    public async System.Threading.Tasks.Task<ReturnChatGroupPagedAudioReconstructions> GetAudioAsync(
        string id,
        ChatGroupsGetAudioRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        if (request.PageNumber != null)
        {
            _query["page_number"] = request.PageNumber.Value.ToString();
        }
        if (request.PageSize != null)
        {
            _query["page_size"] = request.PageSize.Value.ToString();
        }
        if (request.AscendingOrder != null)
        {
            _query["ascending_order"] = JsonUtils.Serialize(request.AscendingOrder.Value);
        }
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
                    Query = _query,
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
                return JsonUtils.Deserialize<ReturnChatGroupPagedAudioReconstructions>(
                    responseBody
                )!;
            }
            catch (JsonException e)
            {
                throw new HumeClientException("Failed to deserialize response", e);
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
                (request, options, cancellationToken) =>
                    ListChatGroupEventsInternalAsync(id, request, options, cancellationToken),
                request => request?.PageNumber ?? 0,
                (request, offset) =>
                {
                    request.PageNumber = offset;
                },
                null,
                response => response?.EventsPage?.ToList(),
                null,
                cancellationToken
            )
            .ConfigureAwait(false);
        return pager;
    }
}
