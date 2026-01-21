using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// Collection of event messages returned by the server.
///
/// Event messages are sent by the server when specific events occur during a chat session. These messages are used to configure behaviors for EVI, such as controlling how EVI starts a new conversation.
/// </summary>
[Serializable]
public record PostedEventMessageSpecs : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("on_inactivity_timeout")]
    public PostedEventMessageSpec? OnInactivityTimeout { get; set; }

    [JsonPropertyName("on_max_duration_timeout")]
    public PostedEventMessageSpec? OnMaxDurationTimeout { get; set; }

    [JsonPropertyName("on_new_chat")]
    public PostedEventMessageSpec? OnNewChat { get; set; }

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
