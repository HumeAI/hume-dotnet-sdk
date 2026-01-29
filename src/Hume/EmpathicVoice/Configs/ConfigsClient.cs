using System.Text.Json;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

public partial class ConfigsClient : IConfigsClient
{
    private RawClient _client;

    internal ConfigsClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Fetches a paginated list of **Configs**.
    ///
    /// For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
    /// </summary>
    private WithRawResponseTask<ReturnPagedConfigs> ListConfigsInternalAsync(
        ConfigsListConfigsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnPagedConfigs>(
            ListConfigsInternalAsyncCore(request, options, cancellationToken)
        );
    }

    private async System.Threading.Tasks.Task<
        WithRawResponse<ReturnPagedConfigs>
    > ListConfigsInternalAsyncCore(
        ConfigsListConfigsRequest request,
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
                    Path = "v0/evi/configs",
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
                var responseData = JsonUtils.Deserialize<ReturnPagedConfigs>(responseBody)!;
                return new WithRawResponse<ReturnPagedConfigs>()
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

    private async System.Threading.Tasks.Task<WithRawResponse<ReturnConfig>> CreateConfigAsyncCore(
        PostedConfig request,
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
                    Path = "v0/evi/configs",
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
                var responseData = JsonUtils.Deserialize<ReturnConfig>(responseBody)!;
                return new WithRawResponse<ReturnConfig>()
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
    /// Fetches a list of a **Config's** versions.
    ///
    /// For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
    /// </summary>
    private WithRawResponseTask<ReturnPagedConfigs> ListConfigVersionsInternalAsync(
        string id,
        ConfigsListConfigVersionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnPagedConfigs>(
            ListConfigVersionsInternalAsyncCore(id, request, options, cancellationToken)
        );
    }

    private async System.Threading.Tasks.Task<
        WithRawResponse<ReturnPagedConfigs>
    > ListConfigVersionsInternalAsyncCore(
        string id,
        ConfigsListConfigVersionsRequest request,
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
                        "v0/evi/configs/{0}",
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
                var responseData = JsonUtils.Deserialize<ReturnPagedConfigs>(responseBody)!;
                return new WithRawResponse<ReturnPagedConfigs>()
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
        WithRawResponse<ReturnConfig>
    > CreateConfigVersionAsyncCore(
        string id,
        PostedConfigVersion request,
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
                    Path = string.Format(
                        "v0/evi/configs/{0}",
                        ValueConvert.ToPathParameterString(id)
                    ),
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
                var responseData = JsonUtils.Deserialize<ReturnConfig>(responseBody)!;
                return new WithRawResponse<ReturnConfig>()
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

    private async System.Threading.Tasks.Task<WithRawResponse<string>> UpdateConfigNameAsyncCore(
        string id,
        PostedConfigName request,
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
                    Method = HttpMethodExtensions.Patch,
                    Path = string.Format(
                        "v0/evi/configs/{0}",
                        ValueConvert.ToPathParameterString(id)
                    ),
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
        WithRawResponse<ReturnConfig>
    > GetConfigVersionAsyncCore(
        string id,
        int version,
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
                        "v0/evi/configs/{0}/version/{1}",
                        ValueConvert.ToPathParameterString(id),
                        ValueConvert.ToPathParameterString(version)
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
                var responseData = JsonUtils.Deserialize<ReturnConfig>(responseBody)!;
                return new WithRawResponse<ReturnConfig>()
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
        WithRawResponse<ReturnConfig>
    > UpdateConfigDescriptionAsyncCore(
        string id,
        int version,
        PostedConfigVersionDescription request,
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
                    Method = HttpMethodExtensions.Patch,
                    Path = string.Format(
                        "v0/evi/configs/{0}/version/{1}",
                        ValueConvert.ToPathParameterString(id),
                        ValueConvert.ToPathParameterString(version)
                    ),
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
                var responseData = JsonUtils.Deserialize<ReturnConfig>(responseBody)!;
                return new WithRawResponse<ReturnConfig>()
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
    /// Fetches a paginated list of **Configs**.
    ///
    /// For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Configs.ListConfigsAsync(
    ///     new ConfigsListConfigsRequest { PageNumber = 0, PageSize = 1 }
    /// );
    /// </code></example>
    public async System.Threading.Tasks.Task<Pager<ReturnConfig>> ListConfigsAsync(
        ConfigsListConfigsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        request = request with { };
        var pager = await OffsetPager<
            ConfigsListConfigsRequest,
            RequestOptions?,
            ReturnPagedConfigs,
            int,
            object,
            ReturnConfig
        >
            .CreateInstanceAsync(
                request,
                options,
                async (request, options, cancellationToken) =>
                    await ListConfigsInternalAsync(request, options, cancellationToken),
                request => request.PageNumber ?? 0,
                (request, offset) =>
                {
                    request.PageNumber = offset;
                },
                null,
                response => response.ConfigsPage?.ToList(),
                null,
                cancellationToken
            )
            .ConfigureAwait(false);
        return pager;
    }

    /// <summary>
    /// Creates a **Config** which can be applied to EVI.
    ///
    /// For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Configs.CreateConfigAsync(
    ///     new PostedConfig
    ///     {
    ///         Name = "Weather Assistant Config",
    ///         Prompt = new PostedConfigPromptSpec
    ///         {
    ///             Id = "af699d45-2985-42cc-91b9-af9e5da3bac5",
    ///             Version = 0,
    ///         },
    ///         EviVersion = "3",
    ///         Voice = new VoiceName
    ///         {
    ///             Provider = Hume.EmpathicVoice.VoiceProvider.HumeAi,
    ///             Name = "Ava Song",
    ///         },
    ///         LanguageModel = new PostedLanguageModel
    ///         {
    ///             ModelProvider = ModelProviderEnum.Anthropic,
    ///             ModelResource = LanguageModelType.Claude37SonnetLatest,
    ///             Temperature = 1f,
    ///         },
    ///         EventMessages = new PostedEventMessageSpecs
    ///         {
    ///             OnNewChat = new PostedEventMessageSpec { Enabled = false, Text = "" },
    ///             OnInactivityTimeout = new PostedEventMessageSpec { Enabled = false, Text = "" },
    ///             OnMaxDurationTimeout = new PostedEventMessageSpec { Enabled = false, Text = "" },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<ReturnConfig> CreateConfigAsync(
        PostedConfig request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnConfig>(
            CreateConfigAsyncCore(request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Fetches a list of a **Config's** versions.
    ///
    /// For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Configs.ListConfigVersionsAsync(
    ///     "1b60e1a0-cc59-424a-8d2c-189d354db3f3",
    ///     new ConfigsListConfigVersionsRequest()
    /// );
    /// </code></example>
    public async System.Threading.Tasks.Task<Pager<ReturnConfig>> ListConfigVersionsAsync(
        string id,
        ConfigsListConfigVersionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        request = request with { };
        var pager = await OffsetPager<
            ConfigsListConfigVersionsRequest,
            RequestOptions?,
            ReturnPagedConfigs,
            int,
            object,
            ReturnConfig
        >
            .CreateInstanceAsync(
                request,
                options,
                async (request, options, cancellationToken) =>
                    await ListConfigVersionsInternalAsync(id, request, options, cancellationToken)
                        .ConfigureAwait(false),
                request => request.PageNumber ?? 0,
                (request, offset) =>
                {
                    request.PageNumber = offset;
                },
                null,
                response => response.ConfigsPage?.ToList(),
                null,
                cancellationToken
            )
            .ConfigureAwait(false);
        return pager;
    }

    /// <summary>
    /// Updates a **Config** by creating a new version of the **Config**.
    ///
    /// For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Configs.CreateConfigVersionAsync(
    ///     "1b60e1a0-cc59-424a-8d2c-189d354db3f3",
    ///     new PostedConfigVersion
    ///     {
    ///         VersionDescription = "This is an updated version of the Weather Assistant Config.",
    ///         EviVersion = "3",
    ///         Prompt = new PostedConfigPromptSpec
    ///         {
    ///             Id = "af699d45-2985-42cc-91b9-af9e5da3bac5",
    ///             Version = 0,
    ///         },
    ///         Voice = new VoiceName
    ///         {
    ///             Provider = Hume.EmpathicVoice.VoiceProvider.HumeAi,
    ///             Name = "Ava Song",
    ///         },
    ///         LanguageModel = new PostedLanguageModel
    ///         {
    ///             ModelProvider = ModelProviderEnum.Anthropic,
    ///             ModelResource = LanguageModelType.Claude37SonnetLatest,
    ///             Temperature = 1f,
    ///         },
    ///         EllmModel = new PostedEllmModel { AllowShortResponses = true },
    ///         EventMessages = new PostedEventMessageSpecs
    ///         {
    ///             OnNewChat = new PostedEventMessageSpec { Enabled = false, Text = "" },
    ///             OnInactivityTimeout = new PostedEventMessageSpec { Enabled = false, Text = "" },
    ///             OnMaxDurationTimeout = new PostedEventMessageSpec { Enabled = false, Text = "" },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<ReturnConfig> CreateConfigVersionAsync(
        string id,
        PostedConfigVersion request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnConfig>(
            CreateConfigVersionAsyncCore(id, request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Deletes a **Config** and its versions.
    ///
    /// For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Configs.DeleteConfigAsync("1b60e1a0-cc59-424a-8d2c-189d354db3f3");
    /// </code></example>
    public async System.Threading.Tasks.Task DeleteConfigAsync(
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
                    Method = HttpMethod.Delete,
                    Path = string.Format(
                        "v0/evi/configs/{0}",
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
    /// Updates the name of a **Config**.
    ///
    /// For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Configs.UpdateConfigNameAsync(
    ///     "1b60e1a0-cc59-424a-8d2c-189d354db3f3",
    ///     new PostedConfigName { Name = "Updated Weather Assistant Config Name" }
    /// );
    /// </code></example>
    public WithRawResponseTask<string> UpdateConfigNameAsync(
        string id,
        PostedConfigName request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<string>(
            UpdateConfigNameAsyncCore(id, request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Fetches a specified version of a **Config**.
    ///
    /// For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Configs.GetConfigVersionAsync("1b60e1a0-cc59-424a-8d2c-189d354db3f3", 1);
    /// </code></example>
    public WithRawResponseTask<ReturnConfig> GetConfigVersionAsync(
        string id,
        int version,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnConfig>(
            GetConfigVersionAsyncCore(id, version, options, cancellationToken)
        );
    }

    /// <summary>
    /// Deletes a specified version of a **Config**.
    ///
    /// For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Configs.DeleteConfigVersionAsync(
    ///     "1b60e1a0-cc59-424a-8d2c-189d354db3f3",
    ///     1
    /// );
    /// </code></example>
    public async System.Threading.Tasks.Task DeleteConfigVersionAsync(
        string id,
        int version,
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
                    Method = HttpMethod.Delete,
                    Path = string.Format(
                        "v0/evi/configs/{0}/version/{1}",
                        ValueConvert.ToPathParameterString(id),
                        ValueConvert.ToPathParameterString(version)
                    ),
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

    /// <summary>
    /// Updates the description of a **Config**.
    ///
    /// For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Configs.UpdateConfigDescriptionAsync(
    ///     "1b60e1a0-cc59-424a-8d2c-189d354db3f3",
    ///     1,
    ///     new PostedConfigVersionDescription
    ///     {
    ///         VersionDescription = "This is an updated version_description.",
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<ReturnConfig> UpdateConfigDescriptionAsync(
        string id,
        int version,
        PostedConfigVersionDescription request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ReturnConfig>(
            UpdateConfigDescriptionAsyncCore(id, version, request, options, cancellationToken)
        );
    }
}
