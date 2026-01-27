using System.Text.Json;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

public partial class PromptsClient : IPromptsClient
{
    private RawClient _client;

    internal PromptsClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Fetches a paginated list of **Prompts**.
    ///
    /// See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
    /// </summary>
    private WithRawResponseTask<ReturnPagedPrompts> ListPromptsInternalAsync(
        PromptsListPromptsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnPagedPrompts>(
            ListPromptsInternalAsyncCore(request, options, cancellationToken)
        );
    }

    private async System.Threading.Tasks.Task<
        WithRawResponse<ReturnPagedPrompts>
    > ListPromptsInternalAsyncCore(
        PromptsListPromptsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new Hume.Core.QueryStringBuilder.Builder(capacity: 4)
            .Add("page_number", request.PageNumber)
            .Add("page_size", request.PageSize)
            .Add("restrict_to_most_recent", request.RestrictToMostRecent)
            .Add("name", request.Name)
            .MergeAdditional(options?.AdditionalQueryParameters)
            .Build();
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.Environment.Base,
                    Method = HttpMethod.Get,
                    Path = "v0/evi/prompts",
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
                var responseData = JsonUtils.Deserialize<ReturnPagedPrompts>(responseBody)!;
                return new WithRawResponse<ReturnPagedPrompts>()
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

    private async System.Threading.Tasks.Task<WithRawResponse<ReturnPrompt?>> CreatePromptAsyncCore(
        PostedPrompt request,
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
                    Path = "v0/evi/prompts",
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
                var responseData = JsonUtils.Deserialize<ReturnPrompt?>(responseBody)!;
                return new WithRawResponse<ReturnPrompt?>()
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
        WithRawResponse<ReturnPagedPrompts>
    > ListPromptVersionsAsyncCore(
        string id,
        PromptsListPromptVersionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new Hume.Core.QueryStringBuilder.Builder(capacity: 3)
            .Add("page_number", request.PageNumber)
            .Add("page_size", request.PageSize)
            .Add("restrict_to_most_recent", request.RestrictToMostRecent)
            .MergeAdditional(options?.AdditionalQueryParameters)
            .Build();
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.Environment.Base,
                    Method = HttpMethod.Get,
                    Path = string.Format(
                        "v0/evi/prompts/{0}",
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
                var responseData = JsonUtils.Deserialize<ReturnPagedPrompts>(responseBody)!;
                return new WithRawResponse<ReturnPagedPrompts>()
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
        WithRawResponse<ReturnPrompt?>
    > CreatePromptVersionAsyncCore(
        string id,
        PostedPromptVersion request,
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
                        "v0/evi/prompts/{0}",
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
                var responseData = JsonUtils.Deserialize<ReturnPrompt?>(responseBody)!;
                return new WithRawResponse<ReturnPrompt?>()
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

    private async System.Threading.Tasks.Task<WithRawResponse<string>> UpdatePromptNameAsyncCore(
        string id,
        PostedPromptName request,
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
                        "v0/evi/prompts/{0}",
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
        WithRawResponse<ReturnPrompt?>
    > GetPromptVersionAsyncCore(
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
                        "v0/evi/prompts/{0}/version/{1}",
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
                var responseData = JsonUtils.Deserialize<ReturnPrompt?>(responseBody)!;
                return new WithRawResponse<ReturnPrompt?>()
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
        WithRawResponse<ReturnPrompt?>
    > UpdatePromptDescriptionAsyncCore(
        string id,
        int version,
        PostedPromptVersionDescription request,
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
                        "v0/evi/prompts/{0}/version/{1}",
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
                var responseData = JsonUtils.Deserialize<ReturnPrompt?>(responseBody)!;
                return new WithRawResponse<ReturnPrompt?>()
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
    /// Fetches a paginated list of **Prompts**.
    ///
    /// See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Prompts.ListPromptsAsync(
    ///     new PromptsListPromptsRequest { PageNumber = 0, PageSize = 2 }
    /// );
    /// </code></example>
    public async System.Threading.Tasks.Task<Pager<ReturnPrompt>> ListPromptsAsync(
        PromptsListPromptsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        request = request with { };
        var pager = await OffsetPager<
            PromptsListPromptsRequest,
            RequestOptions?,
            ReturnPagedPrompts,
            int?,
            object,
            ReturnPrompt
        >
            .CreateInstanceAsync(
                request,
                options,
                async (request, options, cancellationToken) =>
                    await ListPromptsInternalAsync(request, options, cancellationToken),
                request => request.PageNumber ?? 0,
                (request, offset) =>
                {
                    request.PageNumber = offset;
                },
                null,
                response => response.PromptsPage?.ToList(),
                null,
                cancellationToken
            )
            .ConfigureAwait(false);
        return pager;
    }

    /// <summary>
    /// Creates a **Prompt** that can be added to an [EVI configuration](/reference/speech-to-speech-evi/configs/create-config).
    ///
    /// See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Prompts.CreatePromptAsync(
    ///     new PostedPrompt
    ///     {
    ///         Name = "Weather Assistant Prompt",
    ///         Text =
    ///             "&lt;role&gt;You are an AI weather assistant providing users with accurate and up-to-date weather information. Respond to user queries concisely and clearly. Use simple language and avoid technical jargon. Provide temperature, precipitation, wind conditions, and any weather alerts. Include helpful tips if severe weather is expected.&lt;/role&gt;",
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<ReturnPrompt?> CreatePromptAsync(
        PostedPrompt request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnPrompt?>(
            CreatePromptAsyncCore(request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Fetches a list of a **Prompt's** versions.
    ///
    /// See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Prompts.ListPromptVersionsAsync(
    ///     "af699d45-2985-42cc-91b9-af9e5da3bac5",
    ///     new PromptsListPromptVersionsRequest()
    /// );
    /// </code></example>
    public WithRawResponseTask<ReturnPagedPrompts> ListPromptVersionsAsync(
        string id,
        PromptsListPromptVersionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnPagedPrompts>(
            ListPromptVersionsAsyncCore(id, request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Updates a **Prompt** by creating a new version of the **Prompt**.
    ///
    /// See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Prompts.CreatePromptVersionAsync(
    ///     "af699d45-2985-42cc-91b9-af9e5da3bac5",
    ///     new PostedPromptVersion
    ///     {
    ///         Text =
    ///             "&lt;role&gt;You are an updated version of an AI weather assistant providing users with accurate and up-to-date weather information. Respond to user queries concisely and clearly. Use simple language and avoid technical jargon. Provide temperature, precipitation, wind conditions, and any weather alerts. Include helpful tips if severe weather is expected.&lt;/role&gt;",
    ///         VersionDescription = "This is an updated version of the Weather Assistant Prompt.",
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<ReturnPrompt?> CreatePromptVersionAsync(
        string id,
        PostedPromptVersion request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnPrompt?>(
            CreatePromptVersionAsyncCore(id, request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Deletes a **Prompt** and its versions.
    ///
    /// See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Prompts.DeletePromptAsync("af699d45-2985-42cc-91b9-af9e5da3bac5");
    /// </code></example>
    public async System.Threading.Tasks.Task DeletePromptAsync(
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
                        "v0/evi/prompts/{0}",
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
    /// Updates the name of a **Prompt**.
    ///
    /// See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Prompts.UpdatePromptNameAsync(
    ///     "af699d45-2985-42cc-91b9-af9e5da3bac5",
    ///     new PostedPromptName { Name = "Updated Weather Assistant Prompt Name" }
    /// );
    /// </code></example>
    public WithRawResponseTask<string> UpdatePromptNameAsync(
        string id,
        PostedPromptName request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<string>(
            UpdatePromptNameAsyncCore(id, request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Fetches a specified version of a **Prompt**.
    ///
    /// See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Prompts.GetPromptVersionAsync("af699d45-2985-42cc-91b9-af9e5da3bac5", 0);
    /// </code></example>
    public WithRawResponseTask<ReturnPrompt?> GetPromptVersionAsync(
        string id,
        int version,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnPrompt?>(
            GetPromptVersionAsyncCore(id, version, options, cancellationToken)
        );
    }

    /// <summary>
    /// Deletes a specified version of a **Prompt**.
    ///
    /// See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Prompts.DeletePromptVersionAsync(
    ///     "af699d45-2985-42cc-91b9-af9e5da3bac5",
    ///     1
    /// );
    /// </code></example>
    public async System.Threading.Tasks.Task DeletePromptVersionAsync(
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
                        "v0/evi/prompts/{0}/version/{1}",
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
    /// Updates the description of a **Prompt**.
    ///
    /// See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Prompts.UpdatePromptDescriptionAsync(
    ///     "af699d45-2985-42cc-91b9-af9e5da3bac5",
    ///     1,
    ///     new PostedPromptVersionDescription
    ///     {
    ///         VersionDescription = "This is an updated version_description.",
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<ReturnPrompt?> UpdatePromptDescriptionAsync(
        string id,
        int version,
        PostedPromptVersionDescription request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnPrompt?>(
            UpdatePromptDescriptionAsyncCore(id, version, request, options, cancellationToken)
        );
    }
}
