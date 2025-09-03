using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

[Serializable]
public record BuiltinToolConfig : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("name")]
    public required BuiltInTool Name { get; set; }

    /// <summary>
    /// Optional text passed to the supplemental LLM if the tool call fails. The LLM then uses this text to generate a response back to the user, ensuring continuity in the conversation.
    /// </summary>
    [JsonPropertyName("fallback_content")]
    public string? FallbackContent { get; set; }

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
