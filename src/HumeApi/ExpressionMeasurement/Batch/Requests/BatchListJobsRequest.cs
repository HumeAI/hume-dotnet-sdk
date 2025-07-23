using System.Text.Json.Serialization;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Batch;

[Serializable]
public record BatchListJobsRequest
{
    /// <summary>
    /// The maximum number of jobs to include in the response.
    /// </summary>
    [JsonIgnore]
    public int? Limit { get; set; }

    /// <summary>
    /// Include only jobs of this status in the response. There are four possible statuses:
    ///
    /// - `QUEUED`: The job has been received and is waiting to be processed.
    ///
    /// - `IN_PROGRESS`: The job is currently being processed.
    ///
    /// - `COMPLETED`: The job has finished processing.
    ///
    /// - `FAILED`: The job encountered an error and could not be completed successfully.
    /// </summary>
    [JsonIgnore]
    public IEnumerable<Status> Status { get; set; } = new List<Status>();

    /// <summary>
    /// Specify whether to include jobs created before or after a given `timestamp_ms`.
    /// </summary>
    [JsonIgnore]
    public When? When { get; set; }

    /// <summary>
    /// Provide a timestamp in milliseconds to filter jobs.
    ///
    ///  When combined with the `when` parameter, you can filter jobs before or after the given timestamp. Defaults to the current Unix timestamp if one is not provided.
    /// </summary>
    [JsonIgnore]
    public long? TimestampMs { get; set; }

    /// <summary>
    /// Specify which timestamp to sort the jobs by.
    ///
    /// - `created`: Sort jobs by the time of creation, indicated by `created_timestamp_ms`.
    ///
    /// - `started`: Sort jobs by the time processing started, indicated by `started_timestamp_ms`.
    ///
    /// - `ended`: Sort jobs by the time processing ended, indicated by `ended_timestamp_ms`.
    /// </summary>
    [JsonIgnore]
    public SortBy? SortBy { get; set; }

    /// <summary>
    /// Specify the order in which to sort the jobs. Defaults to descending order.
    ///
    /// - `asc`: Sort in ascending order (chronological, with the oldest records first).
    ///
    /// - `desc`: Sort in descending order (reverse-chronological, with the newest records first).
    /// </summary>
    [JsonIgnore]
    public Direction? Direction { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
