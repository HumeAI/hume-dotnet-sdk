using Hume;

namespace Hume.ExpressionMeasurement.Batch;

public partial interface IBatchClient
{
    /// <summary>
    /// Sort and filter jobs.
    /// </summary>
    WithRawResponseTask<IEnumerable<InferenceJob>> ListJobsAsync(
        BatchListJobsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Start a new measurement inference job.
    /// </summary>
    WithRawResponseTask<JobId> StartInferenceJobAsync(
        InferenceBaseRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Get the request details and state of a given job.
    /// </summary>
    WithRawResponseTask<InferenceJob> GetJobDetailsAsync(
        string id,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Get the JSON predictions of a completed inference job.
    /// </summary>
    WithRawResponseTask<IEnumerable<InferenceSourcePredictResult>> GetJobPredictionsAsync(
        string id,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Get the artifacts ZIP of a completed inference job.
    /// </summary>
    WithRawResponseTask<System.IO.Stream> GetJobArtifactsAsync(
        string id,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Start a new batch inference job.
    /// </summary>
    WithRawResponseTask<JobId> StartInferenceJobFromLocalFileAsync(
        BatchStartInferenceJobFromLocalFileRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
