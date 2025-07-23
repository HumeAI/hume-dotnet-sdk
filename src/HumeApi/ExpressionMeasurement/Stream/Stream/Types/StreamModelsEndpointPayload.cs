using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Stream;

/// <summary>
/// Models endpoint payload
/// </summary>
[Serializable]
public record StreamModelsEndpointPayload : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("data")]
    public string? Data { get; set; }

    /// <summary>
    /// Configuration used to specify which models should be used and with what settings.
    /// </summary>
    [JsonPropertyName("models")]
    public Config? Models { get; set; }

    /// <summary>
    /// Length in milliseconds of streaming sliding window.
    ///
    /// Extending the length of this window will prepend media context from past payloads into the current payload.
    ///
    /// For example, if on the first payload you send 500ms of data and on the second payload you send an additional 500ms of data, a window of at least 1000ms will allow the model to process all 1000ms of stream data.
    ///
    /// A window of 600ms would append the full 500ms of the second payload to the last 100ms of the first payload.
    ///
    /// Note: This feature is currently only supported for audio data and audio models. For other file types and models this parameter will be ignored.
    /// </summary>
    [JsonPropertyName("stream_window_ms")]
    public double? StreamWindowMs { get; set; }

    /// <summary>
    /// Whether to reset the streaming sliding window before processing the current payload.
    ///
    /// If this parameter is set to `true` then past context will be deleted before processing the current payload.
    ///
    /// Use reset_stream when one audio file is done being processed and you do not want context to leak across files.
    /// </summary>
    [JsonPropertyName("reset_stream")]
    public bool? ResetStream { get; set; }

    /// <summary>
    /// Set to `true` to enable the data parameter to be parsed as raw text rather than base64 encoded bytes.
    /// This parameter is useful if you want to send text to be processed by the language model, but it cannot be used with other file types like audio, image, or video.
    /// </summary>
    [JsonPropertyName("raw_text")]
    public bool? RawText { get; set; }

    /// <summary>
    /// Set to `true` to get details about the job.
    ///
    /// This parameter can be set in the same payload as data or it can be set without data and models configuration to get the job details between payloads.
    ///
    /// This parameter is useful to get the unique job ID.
    /// </summary>
    [JsonPropertyName("job_details")]
    public bool? JobDetails { get; set; }

    /// <summary>
    /// Pass an arbitrary string as the payload ID and get it back at the top level of the socket response.
    ///
    /// This can be useful if you have multiple requests running asynchronously and want to disambiguate responses as they are received.
    /// </summary>
    [JsonPropertyName("payload_id")]
    public string? PayloadId { get; set; }

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
