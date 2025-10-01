using System.Text.Json;
using System.Text.Json.Serialization;
using Hume;
using Hume.Core;
using OneOf;

namespace Hume.EmpathicVoice;

[Serializable]
public record ChatMessage : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Transcript of the message.
    /// </summary>
    [JsonPropertyName("content")]
    public string? Content { get; set; }

    /// <summary>
    /// Role of who is providing the message.
    /// </summary>
    [JsonPropertyName("role")]
    public required Role Role { get; set; }

    /// <summary>
    /// Function call name and arguments.
    /// </summary>
    [JsonPropertyName("tool_call")]
    public ToolCallMessage? ToolCall { get; set; }

    /// <summary>
    /// Function call response from client.
    /// </summary>
    [JsonPropertyName("tool_result")]
    public OneOf<ToolResponseMessage, ToolErrorMessage>? ToolResult { get; set; }

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
