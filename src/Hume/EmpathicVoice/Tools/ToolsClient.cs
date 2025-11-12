using System.Text.Json;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

public partial class ToolsClient
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
    private async System.Threading.Tasks.Task<ReturnPagedUserDefinedTools> ListToolsInternalAsync(
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
                return JsonUtils.Deserialize<ReturnPagedUserDefinedTools>(responseBody)!;
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
    /// Fetches a list of a **Tool's** versions.
    ///
    /// Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
    /// </summary>
    private async System.Threading.Tasks.Task<ReturnPagedUserDefinedTools> ListToolVersionsInternalAsync(
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
                return JsonUtils.Deserialize<ReturnPagedUserDefinedTools>(responseBody)!;
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
                ListToolsInternalAsync,
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
    public async System.Threading.Tasks.Task<ReturnUserDefinedTool?> CreateToolAsync(
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
                return JsonUtils.Deserialize<ReturnUserDefinedTool?>(responseBody)!;
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
                (request, options, cancellationToken) =>
                    ListToolVersionsInternalAsync(id, request, options, cancellationToken),
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
    public async System.Threading.Tasks.Task<ReturnUserDefinedTool?> CreateToolVersionAsync(
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
                return JsonUtils.Deserialize<ReturnUserDefinedTool?>(responseBody)!;
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
    public async System.Threading.Tasks.Task<string> UpdateToolNameAsync(
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
    /// Fetches a specified version of a **Tool**.
    ///
    /// Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.Tools.GetToolVersionAsync("00183a3f-79ba-413d-9f3b-609864268bea", 1);
    /// </code></example>
    public async System.Threading.Tasks.Task<ReturnUserDefinedTool?> GetToolVersionAsync(
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
                return JsonUtils.Deserialize<ReturnUserDefinedTool?>(responseBody)!;
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
    public async System.Threading.Tasks.Task<ReturnUserDefinedTool?> UpdateToolDescriptionAsync(
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
                return JsonUtils.Deserialize<ReturnUserDefinedTool?>(responseBody)!;
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
