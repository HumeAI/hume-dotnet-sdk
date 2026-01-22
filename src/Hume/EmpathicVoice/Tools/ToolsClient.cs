using System.Text.Json;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

public partial class ToolsClient : IToolsClient
{
    private RawClient _client;

    internal ToolsClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Fetches a paginated list of **Tools**.
    ///
    /// Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
    /// </summary>
    private WithRawResponseTask<ReturnPagedUserDefinedTools> ListToolsInternalAsync(
        ToolsListToolsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnPagedUserDefinedTools>(
            ListToolsInternalAsyncCore(request, options, cancellationToken)
        );
    }

    private async System.Threading.Tasks.Task<
        WithRawResponse<ReturnPagedUserDefinedTools>
    > ListToolsInternalAsyncCore(
        ToolsListToolsRequest request,
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
        if (request.RestrictToMostRecent != null)
        {
            _query["restrict_to_most_recent"] = JsonUtils.Serialize(
                request.RestrictToMostRecent.Value
            );
        }
        if (request.Name != null)
        {
            _query["name"] = request.Name;
        }
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.Environment.Base,
                    Method = HttpMethod.Get,
                    Path = "v0/evi/tools",
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
                var responseData = JsonUtils.Deserialize<ReturnPagedUserDefinedTools>(
                    responseBody
                )!;
                return new WithRawResponse<ReturnPagedUserDefinedTools>()
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
        WithRawResponse<ReturnUserDefinedTool?>
    > CreateToolAsyncCore(
        PostedUserDefinedTool request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.Environment.Base,
                    Method = HttpMethod.Post,
                    Path = "v0/evi/tools",
                    Body = request,
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
                var responseData = JsonUtils.Deserialize<ReturnUserDefinedTool?>(responseBody)!;
                return new WithRawResponse<ReturnUserDefinedTool?>()
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
    /// Fetches a list of a **Tool's** versions.
    ///
    /// Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
    /// </summary>
    private WithRawResponseTask<ReturnPagedUserDefinedTools> ListToolVersionsInternalAsync(
        string id,
        ToolsListToolVersionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnPagedUserDefinedTools>(
            ListToolVersionsInternalAsyncCore(id, request, options, cancellationToken)
        );
    }

    private async System.Threading.Tasks.Task<
        WithRawResponse<ReturnPagedUserDefinedTools>
    > ListToolVersionsInternalAsyncCore(
        string id,
        ToolsListToolVersionsRequest request,
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
        if (request.RestrictToMostRecent != null)
        {
            _query["restrict_to_most_recent"] = JsonUtils.Serialize(
                request.RestrictToMostRecent.Value
            );
        }
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.Environment.Base,
                    Method = HttpMethod.Get,
                    Path = string.Format(
                        "v0/evi/tools/{0}",
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
                var responseData = JsonUtils.Deserialize<ReturnPagedUserDefinedTools>(
                    responseBody
                )!;
                return new WithRawResponse<ReturnPagedUserDefinedTools>()
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
        WithRawResponse<ReturnUserDefinedTool?>
    > CreateToolVersionAsyncCore(
        string id,
        PostedUserDefinedToolVersion request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.Environment.Base,
                    Method = HttpMethod.Post,
                    Path = string.Format(
                        "v0/evi/tools/{0}",
                        ValueConvert.ToPathParameterString(id)
                    ),
                    Body = request,
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
                var responseData = JsonUtils.Deserialize<ReturnUserDefinedTool?>(responseBody)!;
                return new WithRawResponse<ReturnUserDefinedTool?>()
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

    private async System.Threading.Tasks.Task<WithRawResponse<string>> UpdateToolNameAsyncCore(
        string id,
        PostedUserDefinedToolName request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.Environment.Base,
                    Method = HttpMethodExtensions.Patch,
                    Path = string.Format(
                        "v0/evi/tools/{0}",
                        ValueConvert.ToPathParameterString(id)
                    ),
                    Body = request,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            return new WithRawResponse<string>()
            {
                Data = responseBody,
                RawResponse = new RawResponse()
                {
                    StatusCode = response.Raw.StatusCode,
                    Url = response.Raw.RequestMessage?.RequestUri ?? new Uri("about:blank"),
                    Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                },
            };
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
        WithRawResponse<ReturnUserDefinedTool?>
    > GetToolVersionAsyncCore(
        string id,
        int version,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.Environment.Base,
                    Method = HttpMethod.Get,
                    Path = string.Format(
                        "v0/evi/tools/{0}/version/{1}",
                        ValueConvert.ToPathParameterString(id),
                        ValueConvert.ToPathParameterString(version)
                    ),
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
                var responseData = JsonUtils.Deserialize<ReturnUserDefinedTool?>(responseBody)!;
                return new WithRawResponse<ReturnUserDefinedTool?>()
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
        WithRawResponse<ReturnUserDefinedTool?>
    > UpdateToolDescriptionAsyncCore(
        string id,
        int version,
        PostedUserDefinedToolVersionDescription request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.Environment.Base,
                    Method = HttpMethodExtensions.Patch,
                    Path = string.Format(
                        "v0/evi/tools/{0}/version/{1}",
                        ValueConvert.ToPathParameterString(id),
                        ValueConvert.ToPathParameterString(version)
                    ),
                    Body = request,
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
                var responseData = JsonUtils.Deserialize<ReturnUserDefinedTool?>(responseBody)!;
                return new WithRawResponse<ReturnUserDefinedTool?>()
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
    /// Fetches a paginated list of **Tools**.
    ///
    /// Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Tools.ListToolsAsync(
    ///     new ToolsListToolsRequest { PageNumber = 0, PageSize = 2 }
    /// );
    /// </code></example>
    public async System.Threading.Tasks.Task<Pager<ReturnUserDefinedTool>> ListToolsAsync(
        ToolsListToolsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        request = request with { };
        var pager = await OffsetPager<
            ToolsListToolsRequest,
            RequestOptions?,
            ReturnPagedUserDefinedTools,
            int?,
            object,
            ReturnUserDefinedTool
        >
            .CreateInstanceAsync(
                request,
                options,
                async (request, options, cancellationToken) =>
                    await ListToolsInternalAsync(request, options, cancellationToken),
                request => request.PageNumber ?? 0,
                (request, offset) =>
                {
                    request.PageNumber = offset;
                },
                null,
                response => response.ToolsPage?.ToList(),
                null,
                cancellationToken
            )
            .ConfigureAwait(false);
        return pager;
    }

    /// <summary>
    /// Creates a **Tool** that can be added to an [EVI configuration](/reference/speech-to-speech-evi/configs/create-config).
    ///
    /// Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Tools.CreateToolAsync(
    ///     new PostedUserDefinedTool
    ///     {
    ///         Name = "get_current_weather",
    ///         Parameters =
    ///             "{ \"type\": \"object\", \"properties\": { \"location\": { \"type\": \"string\", \"description\": \"The city and state, e.g. San Francisco, CA\" }, \"format\": { \"type\": \"string\", \"enum\": [\"celsius\", \"fahrenheit\"], \"description\": \"The temperature unit to use. Infer this from the users location.\" } }, \"required\": [\"location\", \"format\"] }",
    ///         VersionDescription =
    ///             "Fetches current weather and uses celsius or fahrenheit based on location of user.",
    ///         Description = "This tool is for getting the current weather.",
    ///         FallbackContent = "Unable to fetch current weather.",
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<ReturnUserDefinedTool?> CreateToolAsync(
        PostedUserDefinedTool request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnUserDefinedTool?>(
            CreateToolAsyncCore(request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Fetches a list of a **Tool's** versions.
    ///
    /// Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Tools.ListToolVersionsAsync(
    ///     "00183a3f-79ba-413d-9f3b-609864268bea",
    ///     new ToolsListToolVersionsRequest()
    /// );
    /// </code></example>
    public async System.Threading.Tasks.Task<Pager<ReturnUserDefinedTool>> ListToolVersionsAsync(
        string id,
        ToolsListToolVersionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        request = request with { };
        var pager = await OffsetPager<
            ToolsListToolVersionsRequest,
            RequestOptions?,
            ReturnPagedUserDefinedTools,
            int?,
            object,
            ReturnUserDefinedTool
        >
            .CreateInstanceAsync(
                request,
                options,
                async (request, options, cancellationToken) =>
                    await ListToolVersionsInternalAsync(id, request, options, cancellationToken)
                        .ConfigureAwait(false),
                request => request.PageNumber ?? 0,
                (request, offset) =>
                {
                    request.PageNumber = offset;
                },
                null,
                response => response.ToolsPage?.ToList(),
                null,
                cancellationToken
            )
            .ConfigureAwait(false);
        return pager;
    }

    /// <summary>
    /// Updates a **Tool** by creating a new version of the **Tool**.
    ///
    /// Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Tools.CreateToolVersionAsync(
    ///     "00183a3f-79ba-413d-9f3b-609864268bea",
    ///     new PostedUserDefinedToolVersion
    ///     {
    ///         Parameters =
    ///             "{ \"type\": \"object\", \"properties\": { \"location\": { \"type\": \"string\", \"description\": \"The city and state, e.g. San Francisco, CA\" }, \"format\": { \"type\": \"string\", \"enum\": [\"celsius\", \"fahrenheit\", \"kelvin\"], \"description\": \"The temperature unit to use. Infer this from the users location.\" } }, \"required\": [\"location\", \"format\"] }",
    ///         VersionDescription =
    ///             "Fetches current weather and uses celsius, fahrenheit, or kelvin based on location of user.",
    ///         FallbackContent = "Unable to fetch current weather.",
    ///         Description = "This tool is for getting the current weather.",
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<ReturnUserDefinedTool?> CreateToolVersionAsync(
        string id,
        PostedUserDefinedToolVersion request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnUserDefinedTool?>(
            CreateToolVersionAsyncCore(id, request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Deletes a **Tool** and its versions.
    ///
    /// Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Tools.DeleteToolAsync("00183a3f-79ba-413d-9f3b-609864268bea");
    /// </code></example>
    public async System.Threading.Tasks.Task DeleteToolAsync(
        string id,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.Environment.Base,
                    Method = HttpMethod.Delete,
                    Path = string.Format(
                        "v0/evi/tools/{0}",
                        ValueConvert.ToPathParameterString(id)
                    ),
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

    /// <summary>
    /// Updates the name of a **Tool**.
    ///
    /// Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Tools.UpdateToolNameAsync(
    ///     "00183a3f-79ba-413d-9f3b-609864268bea",
    ///     new PostedUserDefinedToolName { Name = "get_current_temperature" }
    /// );
    /// </code></example>
    public WithRawResponseTask<string> UpdateToolNameAsync(
        string id,
        PostedUserDefinedToolName request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<string>(
            UpdateToolNameAsyncCore(id, request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Fetches a specified version of a **Tool**.
    ///
    /// Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Tools.GetToolVersionAsync("00183a3f-79ba-413d-9f3b-609864268bea", 1);
    /// </code></example>
    public WithRawResponseTask<ReturnUserDefinedTool?> GetToolVersionAsync(
        string id,
        int version,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnUserDefinedTool?>(
            GetToolVersionAsyncCore(id, version, options, cancellationToken)
        );
    }

    /// <summary>
    /// Deletes a specified version of a **Tool**.
    ///
    /// Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Tools.DeleteToolVersionAsync("00183a3f-79ba-413d-9f3b-609864268bea", 1);
    /// </code></example>
    public async System.Threading.Tasks.Task DeleteToolVersionAsync(
        string id,
        int version,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.Environment.Base,
                    Method = HttpMethod.Delete,
                    Path = string.Format(
                        "v0/evi/tools/{0}/version/{1}",
                        ValueConvert.ToPathParameterString(id),
                        ValueConvert.ToPathParameterString(version)
                    ),
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

    /// <summary>
    /// Updates the description of a specified **Tool** version.
    ///
    /// Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Tools.UpdateToolDescriptionAsync(
    ///     "00183a3f-79ba-413d-9f3b-609864268bea",
    ///     1,
    ///     new PostedUserDefinedToolVersionDescription
    ///     {
    ///         VersionDescription =
    ///             "Fetches current temperature, precipitation, wind speed, AQI, and other weather conditions. Uses Celsius, Fahrenheit, or kelvin depending on user's region.",
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<ReturnUserDefinedTool?> UpdateToolDescriptionAsync(
        string id,
        int version,
        PostedUserDefinedToolVersionDescription request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnUserDefinedTool?>(
            UpdateToolDescriptionAsyncCore(id, version, request, options, cancellationToken)
        );
    }
}
