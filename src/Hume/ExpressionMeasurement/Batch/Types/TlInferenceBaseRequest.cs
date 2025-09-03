using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;
using OneOf;

namespace Hume.ExpressionMeasurement.Batch;

[Serializable]
public record TlInferenceBaseRequest : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("custom_model")]
    public required OneOf<CustomModelId, CustomModelVersionId> CustomModel { get; set; }

    /// <summary>
    /// URLs to the media files to be processed. Each must be a valid public URL to a media file (see recommended input filetypes) or an archive (`.zip`, `.tar.gz`, `.tar.bz2`, `.tar.xz`) of media files.
    ///
    /// If you wish to supply more than 100 URLs, consider providing them as an archive (`.zip`, `.tar.gz`, `.tar.bz2`, `.tar.xz`).
    /// </summary>
    [JsonPropertyName("urls")]
    public IEnumerable<string>? Urls { get; set; }

    /// <summary>
    /// If provided, a `POST` request will be made to the URL with the generated predictions on completion or the error message on failure.
    /// </summary>
    [JsonPropertyName("callback_url")]
    public string? CallbackUrl { get; set; }

    /// <summary>
    /// Whether to send an email notification to the user upon job completion/failure.
    /// </summary>
    [JsonPropertyName("notify")]
    public bool? Notify { get; set; }

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
