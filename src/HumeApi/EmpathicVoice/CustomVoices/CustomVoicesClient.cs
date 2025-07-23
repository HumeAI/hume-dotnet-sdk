using System.Net.Http;
using System.Text.Json;
using System.Threading;
using global::System.Threading.Tasks;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

public partial class CustomVoicesClient
{
    private RawClient _client;

    internal CustomVoicesClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Fetches a paginated list of **Custom Voices**.
    ///
    /// Refer to our [voices guide](/docs/empathic-voice-interface-evi/configuration/voices) for details on creating a custom voice.
    /// </summary>
    private async Task<ReturnPagedCustomVoices> ListCustomVoicesInternalAsync(
        CustomVoicesListCustomVoicesRequest request,
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
                    Path = "v0/evi/custom_voices",
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
                return JsonUtils.Deserialize<ReturnPagedCustomVoices>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new HumeApiException("Failed to deserialize response", e);
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
            throw new HumeApiApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    /// <summary>
    /// Fetches a paginated list of **Custom Voices**.
    ///
    /// Refer to our [voices guide](/docs/empathic-voice-interface-evi/configuration/voices) for details on creating a custom voice.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.CustomVoices.ListCustomVoicesAsync(
    ///     new CustomVoicesListCustomVoicesRequest()
    /// );
    /// </code></example>
    public async Task<Pager<ReturnCustomVoice>> ListCustomVoicesAsync(
        CustomVoicesListCustomVoicesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        if (request is not null)
        {
            request = request with { };
        }
        var pager = await OffsetPager<
            CustomVoicesListCustomVoicesRequest,
            RequestOptions?,
            ReturnPagedCustomVoices,
            int?,
            object,
            ReturnCustomVoice
        >
            .CreateInstanceAsync(
                request,
                options,
                ListCustomVoicesInternalAsync,
                request => request?.PageNumber ?? 0,
                (request, offset) =>
                {
                    request.PageNumber = offset;
                },
                null,
                response => response?.CustomVoicesPage?.ToList(),
                null,
                cancellationToken
            )
            .ConfigureAwait(false);
        return pager;
    }

    /// <summary>
    /// Creates a **Custom Voice** that can be added to an [EVI configuration](/reference/empathic-voice-interface-evi/configs/create-config).
    ///
    /// Refer to our [voices guide](/docs/empathic-voice-interface-evi/configuration/voices) for details on creating a custom voice.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.CustomVoices.CreateCustomVoiceAsync(
    ///     new PostedCustomVoice
    ///     {
    ///         Name = "name",
    ///         BaseVoice = PostedCustomVoiceBaseVoice.Ito,
    ///         ParameterModel = "20241004-11parameter",
    ///     }
    /// );
    /// </code></example>
    public async Task<ReturnCustomVoice> CreateCustomVoiceAsync(
        PostedCustomVoice request,
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
                    Path = "v0/evi/custom_voices",
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
                return JsonUtils.Deserialize<ReturnCustomVoice>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new HumeApiException("Failed to deserialize response", e);
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
            throw new HumeApiApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    /// <summary>
    /// Fetches a specific **Custom Voice** by ID.
    ///
    /// Refer to our [voices guide](/docs/empathic-voice-interface-evi/configuration/voices) for details on creating a custom voice.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.CustomVoices.GetCustomVoiceAsync("id");
    /// </code></example>
    public async Task<ReturnCustomVoice> GetCustomVoiceAsync(
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
                    Method = HttpMethod.Get,
                    Path = string.Format(
                        "v0/evi/custom_voices/{0}",
                        ValueConvert.ToPathParameterString(id)
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
                return JsonUtils.Deserialize<ReturnCustomVoice>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new HumeApiException("Failed to deserialize response", e);
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
            throw new HumeApiApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    /// <summary>
    /// Updates a **Custom Voice** by creating a new version of the **Custom Voice**.
    ///
    /// Refer to our [voices guide](/docs/empathic-voice-interface-evi/configuration/voices) for details on creating a custom voice.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.CustomVoices.CreateCustomVoiceVersionAsync(
    ///     "id",
    ///     new PostedCustomVoice
    ///     {
    ///         Name = "name",
    ///         BaseVoice = PostedCustomVoiceBaseVoice.Ito,
    ///         ParameterModel = "20241004-11parameter",
    ///     }
    /// );
    /// </code></example>
    public async Task<ReturnCustomVoice> CreateCustomVoiceVersionAsync(
        string id,
        PostedCustomVoice request,
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
                        "v0/evi/custom_voices/{0}",
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
                return JsonUtils.Deserialize<ReturnCustomVoice>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new HumeApiException("Failed to deserialize response", e);
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
            throw new HumeApiApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    /// <summary>
    /// Deletes a **Custom Voice** and its versions.
    ///
    /// Refer to our [voices guide](/docs/empathic-voice-interface-evi/configuration/voices) for details on creating a custom voice.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.CustomVoices.DeleteCustomVoiceAsync("id");
    /// </code></example>
    public async global::System.Threading.Tasks.Task DeleteCustomVoiceAsync(
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
                        "v0/evi/custom_voices/{0}",
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
            throw new HumeApiApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    /// <summary>
    /// Updates the name of a **Custom Voice**.
    ///
    /// Refer to our [voices guide](/docs/empathic-voice-interface-evi/configuration/voices) for details on creating a custom voice.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.CustomVoices.UpdateCustomVoiceNameAsync(
    ///     "id",
    ///     new PostedCustomVoiceName { Name = "name" }
    /// );
    /// </code></example>
    public async Task<string> UpdateCustomVoiceNameAsync(
        string id,
        PostedCustomVoiceName request,
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
                        "v0/evi/custom_voices/{0}",
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
            throw new HumeApiApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }
}
