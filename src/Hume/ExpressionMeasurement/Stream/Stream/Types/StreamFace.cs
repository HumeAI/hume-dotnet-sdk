using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Stream;

/// <summary>
/// Configuration for the facial expression emotion model.
///
/// Note: Using the `reset_stream` parameter does not have any effect on face identification. A single face identifier cache is maintained over a full session whether `reset_stream` is used or not.
/// </summary>
[Serializable]
public record StreamFace : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Configuration for FACS predictions. If missing or null, no FACS predictions will be generated.
    /// </summary>
    [JsonPropertyName("facs")]
    public Dictionary<string, object?>? Facs { get; set; }

    /// <summary>
    /// Configuration for Descriptions predictions. If missing or null, no Descriptions predictions will be generated.
    /// </summary>
    [JsonPropertyName("descriptions")]
    public Dictionary<string, object?>? Descriptions { get; set; }

    /// <summary>
    /// Whether to return identifiers for faces across frames. If true, unique identifiers will be assigned to face bounding boxes to differentiate different faces. If false, all faces will be tagged with an "unknown" ID.
    /// </summary>
    [JsonPropertyName("identify_faces")]
    public bool? IdentifyFaces { get; set; }

    /// <summary>
    /// Number of frames per second to process. Other frames will be omitted from the response.
    /// </summary>
    [JsonPropertyName("fps_pred")]
    public double? FpsPred { get; set; }

    /// <summary>
    /// Face detection probability threshold. Faces detected with a probability less than this threshold will be omitted from the response.
    /// </summary>
    [JsonPropertyName("prob_threshold")]
    public double? ProbThreshold { get; set; }

    /// <summary>
    /// Minimum bounding box side length in pixels to treat as a face. Faces detected with a bounding box side length in pixels less than this threshold will be omitted from the response.
    /// </summary>
    [JsonPropertyName("min_face_size")]
    public double? MinFaceSize { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
