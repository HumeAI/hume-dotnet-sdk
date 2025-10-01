using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

public partial class ConfigsClient
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
    private async System.Threading.Tasks.Task<ReturnPagedConfigs> ListConfigsInternalAsync(
        ConfigsListConfigsRequest request,
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
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Get,
                    Path = "v0/evi/configs",
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
                return JsonUtils.Deserialize<ReturnPagedConfigs>(responseBody)!;
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

    /// <summary>
    /// Fetches a list of a **Config's** versions.
    ///
    /// For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
    /// </summary>
    private async System.Threading.Tasks.Task<ReturnPagedConfigs> ListConfigVersionsInternalAsync(
        string id,
        ConfigsListConfigVersionsRequest request,
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
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Get,
                    Path = string.Format(
                        "v0/evi/configs/{0}",
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
                return JsonUtils.Deserialize<ReturnPagedConfigs>(responseBody)!;
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
        if (request is not null)
        {
            request = request with { };
        }
        var pager = await OffsetPager<
            ConfigsListConfigsRequest,
            RequestOptions?,
            ReturnPagedConfigs,
            int?,
            object,
            ReturnConfig
        >
            .CreateInstanceAsync(
                request,
                options,
                ListConfigsInternalAsync,
                request => request?.PageNumber ?? 0,
                (request, offset) =>
                {
                    request.PageNumber = offset;
                },
                null,
                response => response?.ConfigsPage?.ToList(),
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
    public async System.Threading.Tasks.Task<ReturnConfig> CreateConfigAsync(
        PostedConfig request,
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
                    Path = "v0/evi/configs",
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
                return JsonUtils.Deserialize<ReturnConfig>(responseBody)!;
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
        if (request is not null)
        {
            request = request with { };
        }
        var pager = await OffsetPager<
            ConfigsListConfigVersionsRequest,
            RequestOptions?,
            ReturnPagedConfigs,
            int?,
            object,
            ReturnConfig
        >
            .CreateInstanceAsync(
                request,
                options,
                (request, options, cancellationToken) =>
                    ListConfigVersionsInternalAsync(id, request, options, cancellationToken),
                request => request?.PageNumber ?? 0,
                (request, offset) =>
                {
                    request.PageNumber = offset;
                },
                null,
                response => response?.ConfigsPage?.ToList(),
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
    public async System.Threading.Tasks.Task<ReturnConfig> CreateConfigVersionAsync(
        string id,
        PostedConfigVersion request,
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
                    Path = string.Format(
                        "v0/evi/configs/{0}",
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
                return JsonUtils.Deserialize<ReturnConfig>(responseBody)!;
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
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Delete,
                    Path = string.Format(
                        "v0/evi/configs/{0}",
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
    public async System.Threading.Tasks.Task<string> UpdateConfigNameAsync(
        string id,
        PostedConfigName request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethodExtensions.Patch,
                    Path = string.Format(
                        "v0/evi/configs/{0}",
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
            return responseBody;
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
    /// Fetches a specified version of a **Config**.
    ///
    /// For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Configs.GetConfigVersionAsync("1b60e1a0-cc59-424a-8d2c-189d354db3f3", 1);
    /// </code></example>
    public async System.Threading.Tasks.Task<ReturnConfig> GetConfigVersionAsync(
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
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Get,
                    Path = string.Format(
                        "v0/evi/configs/{0}/version/{1}",
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
                return JsonUtils.Deserialize<ReturnConfig>(responseBody)!;
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
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Delete,
                    Path = string.Format(
                        "v0/evi/configs/{0}/version/{1}",
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
    public async System.Threading.Tasks.Task<ReturnConfig> UpdateConfigDescriptionAsync(
        string id,
        int version,
        PostedConfigVersionDescription request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethodExtensions.Patch,
                    Path = string.Format(
                        "v0/evi/configs/{0}/version/{1}",
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
                return JsonUtils.Deserialize<ReturnConfig>(responseBody)!;
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
}
