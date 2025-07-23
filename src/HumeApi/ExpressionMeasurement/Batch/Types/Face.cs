using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Batch;

/// <summary>
/// The Facial Emotional Expression model analyzes human facial expressions in images and videos. Results will be provided per frame for video files.
///
/// Recommended input file types: `.png`, `.jpeg`, `.mp4`
/// </summary>
[Serializable]
public record Face : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Number of frames per second to process. Other frames will be omitted from the response. Set to `0` to process every frame.
    /// </summary>
    [JsonPropertyName("fps_pred")]
    public double? FpsPred { get; set; }

    /// <summary>
    /// Face detection probability threshold. Faces detected with a probability less than this threshold will be omitted from the response.
    /// </summary>
    [JsonPropertyName("prob_threshold")]
    public double? ProbThreshold { get; set; }

    /// <summary>
    /// Whether to return identifiers for faces across frames. If `true`, unique identifiers will be assigned to face bounding boxes to differentiate different faces. If `false`, all faces will be tagged with an `unknown` ID.
    /// </summary>
    [JsonPropertyName("identify_faces")]
    public bool? IdentifyFaces { get; set; }

    /// <summary>
    /// Minimum bounding box side length in pixels to treat as a face. Faces detected with a bounding box side length in pixels less than this threshold will be omitted from the response.
    /// </summary>
    [JsonPropertyName("min_face_size")]
    public ulong? MinFaceSize { get; set; }

    [JsonPropertyName("facs")]
    public Dictionary<string, object?>? Facs { get; set; }

    [JsonPropertyName("descriptions")]
    public Dictionary<string, object?>? Descriptions { get; set; }

    /// <summary>
    /// Whether to extract and save the detected faces in the artifacts zip created by each job.
    /// </summary>
    [JsonPropertyName("save_faces")]
    public bool? SaveFaces { get; set; }

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
