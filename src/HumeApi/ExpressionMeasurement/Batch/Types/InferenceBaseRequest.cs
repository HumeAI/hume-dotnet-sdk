using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Batch;

[Serializable]
public record InferenceBaseRequest : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Specify the models to use for inference.
    ///
    /// If this field is not explicitly set, then all models will run by default.
    /// </summary>
    [JsonPropertyName("models")]
    public Models? Models { get; set; }

    [JsonPropertyName("transcription")]
    public Transcription? Transcription { get; set; }

    /// <summary>
    /// URLs to the media files to be processed. Each must be a valid public URL to a media file (see recommended input filetypes) or an archive (`.zip`, `.tar.gz`, `.tar.bz2`, `.tar.xz`) of media files.
    ///
    /// If you wish to supply more than 100 URLs, consider providing them as an archive (`.zip`, `.tar.gz`, `.tar.bz2`, `.tar.xz`).
    /// </summary>
    [JsonPropertyName("urls")]
    public IEnumerable<string>? Urls { get; set; }

    /// <summary>
    /// Text supplied directly to our Emotional Language and NER models for analysis.
    /// </summary>
    [JsonPropertyName("text")]
    public IEnumerable<string>? Text { get; set; }

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
