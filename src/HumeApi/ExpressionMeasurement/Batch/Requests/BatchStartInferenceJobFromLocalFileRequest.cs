using HumeApi;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Batch;

[Serializable]
public record BatchStartInferenceJobFromLocalFileRequest
{
    /// <summary>
    /// Stringified JSON object containing the inference job configuration.
    /// </summary>
    public InferenceBaseRequest? Json { get; set; }

    public IEnumerable<FileParameter> File { get; set; } = new List<FileParameter>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
