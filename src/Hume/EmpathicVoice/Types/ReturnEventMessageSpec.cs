using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// A specific event message configuration to be returned from the server
/// </summary>
[Serializable]
public record ReturnEventMessageSpec : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Boolean indicating if this event message is enabled.
    ///
    /// If set to `true`, a message will be sent when the circumstances for the specific event are met.
    /// </summary>
    [JsonPropertyName("enabled")]
    public required bool Enabled { get; set; }

    /// <summary>
    /// Text to use as the event message when the corresponding event occurs. If no text is specified, EVI will generate an appropriate message based on its current context and the system prompt.
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

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
