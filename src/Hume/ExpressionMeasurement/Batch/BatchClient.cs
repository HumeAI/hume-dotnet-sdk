using System.Text.Json;
using Hume;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Batch;

public partial class BatchClient : IBatchClient
{
    private RawClient _client;

    internal BatchClient(RawClient client)
    {
        _client = client;
    }

    private async System.Threading.Tasks.Task<
        WithRawResponse<IEnumerable<InferenceJob>>
    > ListJobsAsyncCore(
        BatchListJobsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _queryString = new Hume.Core.QueryStringBuilder.Builder(capacity: 6)
            .Add("limit", request.Limit)
            .Add("status", request.Status)
            .Add("when", request.When)
            .Add("timestamp_ms", request.TimestampMs)
            .Add("sort_by", request.SortBy)
            .Add("direction", request.Direction)
            .MergeAdditional(options?.AdditionalQueryParameters)
            .Build();
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.Environment.Base,
                    Method = HttpMethod.Get,
                    Path = "v0/batch/jobs",
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
                var responseData = JsonUtils.Deserialize<IEnumerable<InferenceJob>>(responseBody)!;
                return new WithRawResponse<IEnumerable<InferenceJob>>()
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
            throw new HumeClientApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async System.Threading.Tasks.Task<WithRawResponse<JobId>> StartInferenceJobAsyncCore(
        InferenceBaseRequest request,
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
                    Path = "v0/batch/jobs",
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
                var responseData = JsonUtils.Deserialize<JobId>(responseBody)!;
                return new WithRawResponse<JobId>()
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
            throw new HumeClientApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async System.Threading.Tasks.Task<WithRawResponse<InferenceJob>> GetJobDetailsAsyncCore(
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
                    Method = HttpMethod.Get,
                    Path = string.Format(
                        "v0/batch/jobs/{0}",
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
                var responseData = JsonUtils.Deserialize<InferenceJob>(responseBody)!;
                return new WithRawResponse<InferenceJob>()
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
            throw new HumeClientApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async System.Threading.Tasks.Task<
        WithRawResponse<IEnumerable<InferenceSourcePredictResult>>
    > GetJobPredictionsAsyncCore(
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
                    Method = HttpMethod.Get,
                    Path = string.Format(
                        "v0/batch/jobs/{0}/predictions",
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
                var responseData = JsonUtils.Deserialize<IEnumerable<InferenceSourcePredictResult>>(
                    responseBody
                )!;
                return new WithRawResponse<IEnumerable<InferenceSourcePredictResult>>()
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
            throw new HumeClientApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async System.Threading.Tasks.Task<
        WithRawResponse<System.IO.Stream>
    > GetJobArtifactsAsyncCore(
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
                    Method = HttpMethod.Get,
                    Path = string.Format(
                        "v0/batch/jobs/{0}/artifacts",
                        ValueConvert.ToPathParameterString(id)
                    ),
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var stream = await response.Raw.Content.ReadAsStreamAsync();
            return new WithRawResponse<System.IO.Stream>()
            {
                Data = stream,
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
            throw new HumeClientApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    private async System.Threading.Tasks.Task<
        WithRawResponse<JobId>
    > StartInferenceJobFromLocalFileAsyncCore(
        BatchStartInferenceJobFromLocalFileRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var multipartFormRequest_ = new MultipartFormRequest
        {
            BaseUrl = _client.Options.Environment.Base,
            Method = HttpMethod.Post,
            Path = "v0/batch/jobs",
            Options = options,
        };
        multipartFormRequest_.AddJsonPart("json", request.Json);
        multipartFormRequest_.AddFileParameterPart("file", request.File);
        var response = await _client
            .SendRequestAsync(multipartFormRequest_, cancellationToken)
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                var responseData = JsonUtils.Deserialize<JobId>(responseBody)!;
                return new WithRawResponse<JobId>()
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
            throw new HumeClientApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    /// <summary>
    /// Sort and filter jobs.
    /// </summary>
    /// <example><code>
    /// await client.ExpressionMeasurement.Batch.ListJobsAsync(new BatchListJobsRequest());
    /// </code></example>
    public WithRawResponseTask<IEnumerable<InferenceJob>> ListJobsAsync(
        BatchListJobsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<IEnumerable<InferenceJob>>(
            ListJobsAsyncCore(request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Start a new measurement inference job.
    /// </summary>
    /// <example><code>
    /// await client.ExpressionMeasurement.Batch.StartInferenceJobAsync(
    ///     new InferenceBaseRequest
    ///     {
    ///         Urls = new List&lt;string&gt;() { "https://hume-tutorials.s3.amazonaws.com/faces.zip" },
    ///         Notify = true,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<JobId> StartInferenceJobAsync(
        InferenceBaseRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<JobId>(
            StartInferenceJobAsyncCore(request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Get the request details and state of a given job.
    /// </summary>
    /// <example><code>
    /// await client.ExpressionMeasurement.Batch.GetJobDetailsAsync("job_id");
    /// </code></example>
    public WithRawResponseTask<InferenceJob> GetJobDetailsAsync(
        string id,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<InferenceJob>(
            GetJobDetailsAsyncCore(id, options, cancellationToken)
        );
    }

    /// <summary>
    /// Get the JSON predictions of a completed inference job.
    /// </summary>
    /// <example><code>
    /// await client.ExpressionMeasurement.Batch.GetJobPredictionsAsync("job_id");
    /// </code></example>
    public WithRawResponseTask<IEnumerable<InferenceSourcePredictResult>> GetJobPredictionsAsync(
        string id,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<IEnumerable<InferenceSourcePredictResult>>(
            GetJobPredictionsAsyncCore(id, options, cancellationToken)
        );
    }

    /// <summary>
    /// Get the artifacts ZIP of a completed inference job.
    /// </summary>
    public WithRawResponseTask<System.IO.Stream> GetJobArtifactsAsync(
        string id,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<System.IO.Stream>(
            GetJobArtifactsAsyncCore(id, options, cancellationToken)
        );
    }

    /// <summary>
    /// Start a new batch inference job.
    /// </summary>
    /// <example><code>
    /// await client.ExpressionMeasurement.Batch.StartInferenceJobFromLocalFileAsync(
    ///     new BatchStartInferenceJobFromLocalFileRequest()
    /// );
    /// </code></example>
    public WithRawResponseTask<JobId> StartInferenceJobFromLocalFileAsync(
        BatchStartInferenceJobFromLocalFileRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<JobId>(
            StartInferenceJobFromLocalFileAsyncCore(request, options, cancellationToken)
        );
    }
}
