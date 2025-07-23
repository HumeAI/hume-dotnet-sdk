using System.Text.Json;
using System.Text.Json.Serialization;
using HumeApi;
using HumeApi.Core;
using OneOf;

namespace HumeApi.EmpathicVoice;

/// <summary>
/// Settings for this chat session.
/// </summary>
[Serializable]
public record SessionSettings : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Configuration details for the audio input used during the session. Ensures the audio is being correctly set up for processing.
    ///
    /// This optional field is only required when the audio input is encoded in PCM Linear 16 (16-bit, little-endian, signed PCM WAV data). For detailed instructions on how to configure session settings for PCM Linear 16 audio, please refer to the [Session Settings guide](/docs/empathic-voice-interface-evi/configuration/session-settings).
    /// </summary>
    [JsonPropertyName("audio")]
    public AudioConfiguration? Audio { get; set; }

    /// <summary>
    /// List of built-in tools to enable for the session.
    ///
    /// Tools are resources used by EVI to perform various tasks, such as searching the web or calling external APIs. Built-in tools, like web search, are natively integrated, while user-defined tools are created and invoked by the user. To learn more, see our [Tool Use Guide](/docs/empathic-voice-interface-evi/features/tool-use).
    ///
    /// Currently, the only built-in tool Hume provides is **Web Search**. When enabled, Web Search equips EVI with the ability to search the web for up-to-date information.
    /// </summary>
    [JsonPropertyName("builtin_tools")]
    public IEnumerable<BuiltinToolConfig>? BuiltinTools { get; set; }

    /// <summary>
    /// Allows developers to inject additional context into the conversation, which is appended to the end of user messages for the session.
    ///
    /// When included in a Session Settings message, the provided context can be used to remind the LLM of its role in every user message, prevent it from forgetting important details, or add new relevant information to the conversation.
    ///
    /// Set to `null` to disable context injection.
    /// </summary>
    [JsonPropertyName("context")]
    public Context? Context { get; set; }

    /// <summary>
    /// Unique identifier for the session. Used to manage conversational state, correlate frontend and backend data, and persist conversations across EVI sessions.
    ///
    /// If included, the response sent from Hume to your backend will include this ID. This allows you to correlate frontend users with their incoming messages.
    ///
    /// It is recommended to pass a `custom_session_id` if you are using a Custom Language Model. Please see our guide to [using a custom language model](/docs/empathic-voice-interface-evi/guides/custom-language-model) with EVI to learn more.
    /// </summary>
    [JsonPropertyName("custom_session_id")]
    public string? CustomSessionId { get; set; }

    /// <summary>
    /// Third party API key for the supplemental language model.
    ///
    /// When provided, EVI will use this key instead of Hume’s API key for the supplemental LLM. This allows you to bypass rate limits and utilize your own API key as needed.
    /// </summary>
    [JsonPropertyName("language_model_api_key")]
    public string? LanguageModelApiKey { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object?>? Metadata { get; set; }

    /// <summary>
    /// Instructions used to shape EVI’s behavior, responses, and style for the session.
    ///
    /// When included in a Session Settings message, the provided Prompt overrides the existing one specified in the EVI configuration. If no Prompt was defined in the configuration, this Prompt will be the one used for the session.
    ///
    /// You can use the Prompt to define a specific goal or role for EVI, specifying how it should act or what it should focus on during the conversation. For example, EVI can be instructed to act as a customer support representative, a fitness coach, or a travel advisor, each with its own set of behaviors and response styles.
    ///
    /// For help writing a system prompt, see our [Prompting Guide](/docs/empathic-voice-interface-evi/guides/prompting).
    /// </summary>
    [JsonPropertyName("system_prompt")]
    public string? SystemPrompt { get; set; }

    /// <summary>
    /// List of user-defined tools to enable for the session.
    ///
    /// Tools are resources used by EVI to perform various tasks, such as searching the web or calling external APIs. Built-in tools, like web search, are natively integrated, while user-defined tools are created and invoked by the user. To learn more, see our [Tool Use Guide](/docs/empathic-voice-interface-evi/features/tool-use).
    /// </summary>
    [JsonPropertyName("tools")]
    public IEnumerable<Tool>? Tools { get; set; }

    /// <summary>
    /// The type of message sent through the socket; must be `session_settings` for our server to correctly identify and process it as a Session Settings message.
    ///
    /// Session settings are temporary and apply only to the current Chat session. These settings can be adjusted dynamically based on the requirements of each session to ensure optimal performance and user experience.
    ///
    /// For more information, please refer to the [Session Settings guide](/docs/empathic-voice-interface-evi/configuration/session-settings).
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = "session_settings";

    /// <summary>
    /// This field allows you to assign values to dynamic variables referenced in your system prompt.
    ///
    /// Each key represents the variable name, and the corresponding value is the specific content you wish to assign to that variable within the session. While the values for variables can be strings, numbers, or booleans, the value will ultimately be converted to a string when injected into your system prompt.
    ///
    /// Using this field, you can personalize responses based on session-specific details. For more guidance, see our [guide on using dynamic variables](/docs/empathic-voice-interface-evi/features/dynamic-variables).
    /// </summary>
    [JsonPropertyName("variables")]
    public Dictionary<string, OneOf<string, double, bool>>? Variables { get; set; }

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
