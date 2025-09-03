using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Batch;

/// <summary>
/// A bounding box around a face.
/// </summary>
[Serializable]
public record BoundingBox : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// x-coordinate of bounding box top left corner.
    /// </summary>
    [JsonPropertyName("x")]
    public required double X { get; set; }

    /// <summary>
    /// y-coordinate of bounding box top left corner.
    /// </summary>
    [JsonPropertyName("y")]
    public required double Y { get; set; }

    /// <summary>
    /// Bounding box width.
    /// </summary>
    [JsonPropertyName("w")]
    public required double W { get; set; }

    /// <summary>
    /// Bounding box height.
    /// </summary>
    [JsonPropertyName("h")]
    public required double H { get; set; }

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
