using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// Collection of event_message specs to be returned from the server
/// </summary>
[Serializable]
public record ReturnEventMessageSpecs : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("on_inactivity_timeout")]
    public ReturnEventMessageSpec? OnInactivityTimeout { get; set; }

    [JsonPropertyName("on_max_duration_timeout")]
    public ReturnEventMessageSpec? OnMaxDurationTimeout { get; set; }

    [JsonPropertyName("on_new_chat")]
    public ReturnEventMessageSpec? OnNewChat { get; set; }

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
