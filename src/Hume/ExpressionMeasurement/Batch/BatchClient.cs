using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Hume;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Batch;

public partial class BatchClient
{
    private RawClient _client;

    internal BatchClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Sort and filter jobs.
    /// </summary>
    /// <example><code>
    /// await client.ExpressionMeasurement.Batch.ListJobsAsync(new BatchListJobsRequest());
    /// </code></example>
    public async System.Threading.Tasks.Task<IEnumerable<InferenceJob>> ListJobsAsync(
        BatchListJobsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        _query["status"] = request.Status.Select(_value => _value.Stringify()).ToList();
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit.Value.ToString();
        }
        if (request.When != null)
        {
            _query["when"] = request.When.Value.Stringify();
        }
        if (request.TimestampMs != null)
        {
            _query["timestamp_ms"] = request.TimestampMs.Value.ToString();
        }
        if (request.SortBy != null)
        {
            _query["sort_by"] = request.SortBy.Value.Stringify();
        }
        if (request.Direction != null)
        {
            _query["direction"] = request.Direction.Value.Stringify();
        }
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Get,
                    Path = "v0/batch/jobs",
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
                return JsonUtils.Deserialize<IEnumerable<InferenceJob>>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new HumeClientException("Failed to deserialize response", e);
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
    public async System.Threading.Tasks.Task<JobId> StartInferenceJobAsync(
        InferenceBaseRequest request,
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
                return JsonUtils.Deserialize<JobId>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new HumeClientException("Failed to deserialize response", e);
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
    /// Get the request details and state of a given job.
    /// </summary>
    /// <example><code>
    /// await client.ExpressionMeasurement.Batch.GetJobDetailsAsync("job_id");
    /// </code></example>
    public async System.Threading.Tasks.Task<InferenceJob> GetJobDetailsAsync(
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
                return JsonUtils.Deserialize<InferenceJob>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new HumeClientException("Failed to deserialize response", e);
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
    /// Get the JSON predictions of a completed inference job.
    /// </summary>
    /// <example><code>
    /// await client.ExpressionMeasurement.Batch.GetJobPredictionsAsync("job_id");
    /// </code></example>
    public async System.Threading.Tasks.Task<
        IEnumerable<InferenceSourcePredictResult>
    > GetJobPredictionsAsync(
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
                return JsonUtils.Deserialize<IEnumerable<InferenceSourcePredictResult>>(
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
            throw new HumeClientApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    /// <summary>
    /// Get the artifacts ZIP of a completed inference job.
    /// </summary>
    public async System.Threading.Tasks.Task<System.IO.Stream> GetJobArtifactsAsync(
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
            return await response.Raw.Content.ReadAsStreamAsync();
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
    /// Start a new batch inference job.
    /// </summary>
    public async System.Threading.Tasks.Task<JobId> StartInferenceJobFromLocalFileAsync(
        BatchStartInferenceJobFromLocalFileRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var multipartFormRequest_ = new MultipartFormRequest
        {
            BaseUrl = _client.Options.BaseUrl,
            Method = HttpMethod.Post,
            Path = "v0/batch/jobs",
            Options = options,
        };
        multipartFormRequest_.AddJsonPart("json", request.Json);
        multipartFormRequest_.AddFileParameterParts("file", request.File);
        var response = await _client
            .SendRequestAsync(multipartFormRequest_, cancellationToken)
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<JobId>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new HumeClientException("Failed to deserialize response", e);
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
}
