using Hume;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Batch;

[Serializable]
public record BatchStartInferenceJobFromLocalFileRequest
{
    /// <summary>
    /// Stringified JSON object containing the inference job configuration.
    /// </summary>
    public InferenceBaseRequest? Json { get; set; }

    public required FileParameter File { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
