using System.IO;
using System.Net.WebSockets;
using System.Text.Json;
using Hume.Core;
using Hume.Core.Async;
using Hume.Core.Async.Events;
using Hume.Core.Async.Models;
using OneOf;

namespace Hume.EmpathicVoice;

/// <summary>
/// Chat with Empathic Voice Interface (EVI)
/// </summary>
public partial class ChatApi : AsyncApi<ChatApi.Options>
{
    /// <summary>
    /// Event handler for AssistantEnd.
    /// Use AssistantEnd.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<AssistantEnd> AssistantEnd = new();

    /// <summary>
    /// Event handler for AssistantMessage.
    /// Use AssistantMessage.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<AssistantMessage> AssistantMessage = new();

    /// <summary>
    /// Event handler for AssistantProsody.
    /// Use AssistantProsody.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<AssistantProsody> AssistantProsody = new();

    /// <summary>
    /// Event handler for AudioOutput.
    /// Use AudioOutput.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<AudioOutput> AudioOutput = new();

    /// <summary>
    /// Event handler for ChatMetadata.
    /// Use ChatMetadata.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<ChatMetadata> ChatMetadata = new();

    /// <summary>
    /// Event handler for WebSocketError.
    /// Use WebSocketError.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<WebSocketError> WebSocketError = new();

    /// <summary>
    /// Event handler for UserInterruption.
    /// Use UserInterruption.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<UserInterruption> UserInterruption = new();

    /// <summary>
    /// Event handler for UserMessage.
    /// Use UserMessage.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<UserMessage> UserMessage = new();

    /// <summary>
    /// Event handler for ToolCallMessage.
    /// Use ToolCallMessage.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<ToolCallMessage> ToolCallMessage = new();

    /// <summary>
    /// Event handler for ToolResponseMessage.
    /// Use ToolResponseMessage.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<ToolResponseMessage> ToolResponseMessage = new();

    /// <summary>
    /// Event handler for ToolErrorMessage.
    /// Use ToolErrorMessage.Subscribe(...) to receive messages.
    /// </summary>
    public readonly Event<ToolErrorMessage> ToolErrorMessage = new();

    /// <summary>
    /// Default constructor
    /// </summary>
    public ChatApi()
        : this(new ChatApi.Options()) { }

    /// <summary>
    /// Constructor with options
    /// </summary>
    public ChatApi(ChatApi.Options options)
        : base(options) { }

    /// <summary>
    /// Access token used for authenticating the client. If not provided, an `api_key` must be provided to authenticate.
    ///
    /// The access token is generated using both an API key and a Secret key, which provides an additional layer of security compared to using just an API key.
    ///
    /// For more details, refer to the [Authentication Strategies Guide](/docs/introduction/api-key#authentication-strategies).
    /// </summary>
    public string? AccessToken
    {
        get => ApiOptions.AccessToken;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.AccessToken),
                ApiOptions.AccessToken = value
            );
    }

    /// <summary>
    /// The unique identifier for an EVI configuration.
    ///
    /// Include this ID in your connection request to equip EVI with the Prompt, Language Model, Voice, and Tools associated with the specified configuration. If omitted, EVI will apply [default configuration settings](/docs/speech-to-speech-evi/configuration/build-a-configuration#default-configuration).
    ///
    /// For help obtaining this ID, see our [Configuration Guide](/docs/speech-to-speech-evi/configuration).
    /// </summary>
    public string? ConfigId
    {
        get => ApiOptions.ConfigId;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.ConfigId),
                ApiOptions.ConfigId = value
            );
    }

    /// <summary>
    /// The version number of the EVI configuration specified by the `config_id`.
    ///
    /// Configs, as well as Prompts and Tools, are versioned. This versioning system supports iterative development, allowing you to progressively refine configurations and revert to previous versions if needed.
    ///
    /// Include this parameter to apply a specific version of an EVI configuration. If omitted, the latest version will be applied.
    /// </summary>
    public int? ConfigVersion
    {
        get => ApiOptions.ConfigVersion;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.ConfigVersion),
                ApiOptions.ConfigVersion = value
            );
    }

    /// <summary>
    /// The maximum number of chat events to return from chat history. By default, the system returns up to 300 events (100 events per page × 3 pages). Set this parameter to a smaller value to limit the number of events returned.
    /// </summary>
    public int? EventLimit
    {
        get => ApiOptions.EventLimit;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.EventLimit),
                ApiOptions.EventLimit = value
            );
    }

    /// <summary>
    /// The unique identifier for a Chat Group. Use this field to preserve context from a previous Chat session.
    ///
    /// A Chat represents a single session from opening to closing a WebSocket connection. In contrast, a Chat Group is a series of resumed Chats that collectively represent a single conversation spanning multiple sessions. Each Chat includes a Chat Group ID, which is used to preserve the context of previous Chat sessions when starting a new one.
    ///
    /// Including the Chat Group ID in the `resumed_chat_group_id` query parameter is useful for seamlessly resuming a Chat after unexpected network disconnections and for picking up conversations exactly where you left off at a later time. This ensures preserved context across multiple sessions.
    ///
    /// There are three ways to obtain the Chat Group ID:
    ///
    /// - [Chat Metadata](/reference/speech-to-speech-evi/chat#receive.ChatMetadata): Upon establishing a WebSocket connection with EVI, the user receives a Chat Metadata message. This message contains a `chat_group_id`, which can be used to resume conversations within this chat group in future sessions.
    ///
    /// - [List Chats endpoint](/reference/speech-to-speech-evi/chats/list-chats): Use the GET `/v0/evi/chats` endpoint to obtain the Chat Group ID of individual Chat sessions. This endpoint lists all available Chat sessions and their associated Chat Group ID.
    ///
    /// - [List Chat Groups endpoint](/reference/speech-to-speech-evi/chat-groups/list-chat-groups): Use the GET `/v0/evi/chat_groups` endpoint to obtain the Chat Group IDs of all Chat Groups associated with an API key. This endpoint returns a list of all available chat groups.
    /// </summary>
    public string? ResumedChatGroupId
    {
        get => ApiOptions.ResumedChatGroupId;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.ResumedChatGroupId),
                ApiOptions.ResumedChatGroupId = value
            );
    }

    /// <summary>
    /// Sets number of audio channels for audio input.
    /// </summary>
    public int? SessionSettingsAudioChannels
    {
        get => ApiOptions.SessionSettingsAudioChannels;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.SessionSettingsAudioChannels),
                ApiOptions.SessionSettingsAudioChannels = value
            );
    }

    /// <summary>
    /// Sets encoding format of the audio input, such as `linear16`.
    /// </summary>
    public string? SessionSettingsAudioEncoding
    {
        get => ApiOptions.SessionSettingsAudioEncoding;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.SessionSettingsAudioEncoding),
                ApiOptions.SessionSettingsAudioEncoding = value
            );
    }

    /// <summary>
    /// Sets the sample rate for audio input. (Number of samples per second in the audio input, measured in Hertz.)
    /// </summary>
    public int? SessionSettingsAudioSampleRate
    {
        get => ApiOptions.SessionSettingsAudioSampleRate;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.SessionSettingsAudioSampleRate),
                ApiOptions.SessionSettingsAudioSampleRate = value
            );
    }

    /// <summary>
    /// The context to be injected into the conversation. Helps inform the LLM's response by providing relevant information about the ongoing conversation.
    ///
    /// This text will be appended to the end of [user_messages](/reference/speech-to-speech-evi/chat#receive.UserMessage.message.content) based on the chosen persistence level. For example, if you want to remind EVI of its role as a helpful weather assistant, the context you insert will be appended to the end of user messages as `{Context: You are a helpful weather assistant}`.
    /// </summary>
    public string? SessionSettingsContextText
    {
        get => ApiOptions.SessionSettingsContextText;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.SessionSettingsContextText),
                ApiOptions.SessionSettingsContextText = value
            );
    }

    /// <summary>
    /// The persistence level of the injected context. Specifies how long the injected context will remain active in the session.
    ///
    /// - **Temporary**: Context that is only applied to the following assistant response.
    ///
    /// - **Persistent**: Context that is applied to all subsequent assistant responses for the remainder of the Chat.
    /// </summary>
    public string? SessionSettingsContextType
    {
        get => ApiOptions.SessionSettingsContextType;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.SessionSettingsContextType),
                ApiOptions.SessionSettingsContextType = value
            );
    }

    /// <summary>
    /// Used to manage conversational state, correlate frontend and backend data, and persist conversations across EVI sessions.
    /// </summary>
    public string? SessionSettingsCustomSessionId
    {
        get => ApiOptions.SessionSettingsCustomSessionId;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.SessionSettingsCustomSessionId),
                ApiOptions.SessionSettingsCustomSessionId = value
            );
    }

    /// <summary>
    /// The maximum number of chat events to return from chat history. By default, the system returns up to 300 events (100 events per page × 3 pages). Set this parameter to a smaller value to limit the number of events returned.
    /// </summary>
    public int? SessionSettingsEventLimit
    {
        get => ApiOptions.SessionSettingsEventLimit;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.SessionSettingsEventLimit),
                ApiOptions.SessionSettingsEventLimit = value
            );
    }

    /// <summary>
    /// Third party API key for the supplemental language model.
    ///
    /// When provided, EVI will use this key instead of Hume's API key for the supplemental LLM. This allows you to bypass rate limits and utilize your own API key as needed.
    /// </summary>
    public string? SessionSettingsLanguageModelApiKey
    {
        get => ApiOptions.SessionSettingsLanguageModelApiKey;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(
                    ApiOptions.SessionSettingsLanguageModelApiKey
                ),
                ApiOptions.SessionSettingsLanguageModelApiKey = value
            );
    }

    /// <summary>
    /// Instructions used to shape EVI's behavior, responses, and style for the session.
    ///
    /// When included in a Session Settings message, the provided Prompt overrides the existing one specified in the EVI configuration. If no Prompt was defined in the configuration, this Prompt will be the one used for the session.
    ///
    /// You can use the Prompt to define a specific goal or role for EVI, specifying how it should act or what it should focus on during the conversation. For example, EVI can be instructed to act as a customer support representative, a fitness coach, or a travel advisor, each with its own set of behaviors and response styles.
    ///
    /// For help writing a system prompt, see our [Prompting Guide](/docs/speech-to-speech-evi/guides/prompting).
    /// </summary>
    public string? SessionSettingsSystemPrompt
    {
        get => ApiOptions.SessionSettingsSystemPrompt;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.SessionSettingsSystemPrompt),
                ApiOptions.SessionSettingsSystemPrompt = value
            );
    }

    /// <summary>
    /// This field allows you to assign values to dynamic variables referenced in your system prompt.
    ///
    /// Each key represents the variable name, and the corresponding value is the specific content you wish to assign to that variable within the session. While the values for variables can be strings, numbers, or booleans, the value will ultimately be converted to a string when injected into your system prompt.
    ///
    /// Using this field, you can personalize responses based on session-specific details. For more guidance, see our [guide on using dynamic variables](/docs/speech-to-speech-evi/features/dynamic-variables).
    /// </summary>
    public string? SessionSettingsVariables
    {
        get => ApiOptions.SessionSettingsVariables;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.SessionSettingsVariables),
                ApiOptions.SessionSettingsVariables = value
            );
    }

    /// <summary>
    /// The name or ID of the voice from the `Voice Library` to be used as the speaker for this EVI session. This will override the speaker set in the selected configuration.
    /// </summary>
    public string? SessionSettingsVoiceId
    {
        get => ApiOptions.SessionSettingsVoiceId;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.SessionSettingsVoiceId),
                ApiOptions.SessionSettingsVoiceId = value
            );
    }

    /// <summary>
    /// A flag to enable verbose transcription. Set this query parameter to `true` to have unfinalized user transcripts be sent to the client as interim UserMessage messages. The [interim](/reference/speech-to-speech-evi/chat#receive.UserMessage.interim) field on a [UserMessage](/reference/speech-to-speech-evi/chat#receive.UserMessage) denotes whether the message is "interim" or "final."
    /// </summary>
    public bool? VerboseTranscription
    {
        get => ApiOptions.VerboseTranscription;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.VerboseTranscription),
                ApiOptions.VerboseTranscription = value
            );
    }

    /// <summary>
    /// API key used for authenticating the client. If not provided, an `access_token` must be provided to authenticate.
    ///
    /// For more details, refer to the [Authentication Strategies Guide](/docs/introduction/api-key#authentication-strategies).
    /// </summary>
    public string? ApiKey
    {
        get => ApiOptions.ApiKey;
        set =>
            NotifyIfPropertyChanged(
                EqualityComparer<string>.Default.Equals(ApiOptions.ApiKey),
                ApiOptions.ApiKey = value
            );
    }

    /// <summary>
    /// Creates the Uri for the websocket connection from the BaseUrl and parameters
    /// </summary>
    protected override Uri CreateUri()
    {
        var uri = new UriBuilder(BaseUrl)
        {
            Query = new Query()
            {
                { "access_token", AccessToken },
                { "config_id", ConfigId },
                { "config_version", ConfigVersion },
                { "event_limit", EventLimit },
                { "resumed_chat_group_id", ResumedChatGroupId },
                { "session_settings[audio][channels]", SessionSettingsAudioChannels },
                { "session_settings[audio][encoding]", SessionSettingsAudioEncoding },
                { "session_settings[audio][sample_rate]", SessionSettingsAudioSampleRate },
                { "session_settings[context][text]", SessionSettingsContextText },
                { "session_settings[context][type]", SessionSettingsContextType },
                { "session_settings[custom_session_id]", SessionSettingsCustomSessionId },
                { "session_settings[event_limit]", SessionSettingsEventLimit },
                { "session_settings[language_model_api_key]", SessionSettingsLanguageModelApiKey },
                { "session_settings[system_prompt]", SessionSettingsSystemPrompt },
                { "session_settings[variables]", SessionSettingsVariables },
                { "session_settings[voice_id]", SessionSettingsVoiceId },
                { "verbose_transcription", VerboseTranscription },
                { "api_key", ApiKey },
            },
        };
        uri.Path = $"{uri.Path.TrimEnd('/')}/chat";
        return uri.Uri;
    }

    protected override void SetConnectionOptions(ClientWebSocketOptions options) { }

    /// <summary>
    /// Dispatches incoming WebSocket messages
    /// </summary>
    protected async override System.Threading.Tasks.Task OnTextMessage(Stream stream)
    {
        var json = await JsonSerializer.DeserializeAsync<JsonDocument>(stream);
        if (json == null)
        {
            await ExceptionOccurred
                .RaiseEvent(new Exception("Invalid message - Not valid JSON"))
                .ConfigureAwait(false);
            return;
        }

        // deserialize the message to find the correct event
        {
            if (JsonUtils.TryDeserialize(json, out AssistantEnd? message))
            {
                await AssistantEnd.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out AssistantMessage? message))
            {
                await AssistantMessage.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out AssistantProsody? message))
            {
                await AssistantProsody.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out AudioOutput? message))
            {
                await AudioOutput.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out ChatMetadata? message))
            {
                await ChatMetadata.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out WebSocketError? message))
            {
                await WebSocketError.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out UserInterruption? message))
            {
                await UserInterruption.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out UserMessage? message))
            {
                await UserMessage.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out ToolCallMessage? message))
            {
                await ToolCallMessage.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out ToolResponseMessage? message))
            {
                await ToolResponseMessage.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        {
            if (JsonUtils.TryDeserialize(json, out ToolErrorMessage? message))
            {
                await ToolErrorMessage.RaiseEvent(message!).ConfigureAwait(false);
                return;
            }
        }

        await ExceptionOccurred
            .RaiseEvent(new Exception($"Unknown message: {json.ToString()}"))
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Disposes of event subscriptions
    /// </summary>
    protected override void DisposeEvents()
    {
        AssistantEnd.Dispose();
        AssistantMessage.Dispose();
        AssistantProsody.Dispose();
        AudioOutput.Dispose();
        ChatMetadata.Dispose();
        WebSocketError.Dispose();
        UserInterruption.Dispose();
        UserMessage.Dispose();
        ToolCallMessage.Dispose();
        ToolResponseMessage.Dispose();
        ToolErrorMessage.Dispose();
    }

    /// <summary>
    /// Sends a PublishEvent message to the server
    /// </summary>
    public async System.Threading.Tasks.Task Send(
        OneOf<
            AudioInput,
            SessionSettings,
            UserInput,
            AssistantInput,
            ToolResponseMessage,
            ToolErrorMessage,
            PauseAssistantMessage,
            ResumeAssistantMessage
        > message
    )
    {
        await SendInstant(JsonUtils.Serialize(message)).ConfigureAwait(false);
    }

    /// <summary>
    /// Options for the API client
    /// </summary>
    public class Options : AsyncApiOptions
    {
        /// <summary>
        /// The Websocket URL for the API connection.
        /// </summary>
        override public string BaseUrl { get; set; } = "wss://api.hume.ai/v0/evi";

        /// <summary>
        /// Access token used for authenticating the client. If not provided, an `api_key` must be provided to authenticate.
        ///
        /// The access token is generated using both an API key and a Secret key, which provides an additional layer of security compared to using just an API key.
        ///
        /// For more details, refer to the [Authentication Strategies Guide](/docs/introduction/api-key#authentication-strategies).
        /// </summary>
        public string? AccessToken { get; set; }

        /// <summary>
        /// The unique identifier for an EVI configuration.
        ///
        /// Include this ID in your connection request to equip EVI with the Prompt, Language Model, Voice, and Tools associated with the specified configuration. If omitted, EVI will apply [default configuration settings](/docs/speech-to-speech-evi/configuration/build-a-configuration#default-configuration).
        ///
        /// For help obtaining this ID, see our [Configuration Guide](/docs/speech-to-speech-evi/configuration).
        /// </summary>
        public string? ConfigId { get; set; }

        /// <summary>
        /// The version number of the EVI configuration specified by the `config_id`.
        ///
        /// Configs, as well as Prompts and Tools, are versioned. This versioning system supports iterative development, allowing you to progressively refine configurations and revert to previous versions if needed.
        ///
        /// Include this parameter to apply a specific version of an EVI configuration. If omitted, the latest version will be applied.
        /// </summary>
        public int? ConfigVersion { get; set; }

        /// <summary>
        /// The maximum number of chat events to return from chat history. By default, the system returns up to 300 events (100 events per page × 3 pages). Set this parameter to a smaller value to limit the number of events returned.
        /// </summary>
        public int? EventLimit { get; set; }

        /// <summary>
        /// The unique identifier for a Chat Group. Use this field to preserve context from a previous Chat session.
        ///
        /// A Chat represents a single session from opening to closing a WebSocket connection. In contrast, a Chat Group is a series of resumed Chats that collectively represent a single conversation spanning multiple sessions. Each Chat includes a Chat Group ID, which is used to preserve the context of previous Chat sessions when starting a new one.
        ///
        /// Including the Chat Group ID in the `resumed_chat_group_id` query parameter is useful for seamlessly resuming a Chat after unexpected network disconnections and for picking up conversations exactly where you left off at a later time. This ensures preserved context across multiple sessions.
        ///
        /// There are three ways to obtain the Chat Group ID:
        ///
        /// - [Chat Metadata](/reference/speech-to-speech-evi/chat#receive.ChatMetadata): Upon establishing a WebSocket connection with EVI, the user receives a Chat Metadata message. This message contains a `chat_group_id`, which can be used to resume conversations within this chat group in future sessions.
        ///
        /// - [List Chats endpoint](/reference/speech-to-speech-evi/chats/list-chats): Use the GET `/v0/evi/chats` endpoint to obtain the Chat Group ID of individual Chat sessions. This endpoint lists all available Chat sessions and their associated Chat Group ID.
        ///
        /// - [List Chat Groups endpoint](/reference/speech-to-speech-evi/chat-groups/list-chat-groups): Use the GET `/v0/evi/chat_groups` endpoint to obtain the Chat Group IDs of all Chat Groups associated with an API key. This endpoint returns a list of all available chat groups.
        /// </summary>
        public string? ResumedChatGroupId { get; set; }

        /// <summary>
        /// Sets number of audio channels for audio input.
        /// </summary>
        public int? SessionSettingsAudioChannels { get; set; }

        /// <summary>
        /// Sets encoding format of the audio input, such as `linear16`.
        /// </summary>
        public string? SessionSettingsAudioEncoding { get; set; }

        /// <summary>
        /// Sets the sample rate for audio input. (Number of samples per second in the audio input, measured in Hertz.)
        /// </summary>
        public int? SessionSettingsAudioSampleRate { get; set; }

        /// <summary>
        /// The context to be injected into the conversation. Helps inform the LLM's response by providing relevant information about the ongoing conversation.
        ///
        /// This text will be appended to the end of [user_messages](/reference/speech-to-speech-evi/chat#receive.UserMessage.message.content) based on the chosen persistence level. For example, if you want to remind EVI of its role as a helpful weather assistant, the context you insert will be appended to the end of user messages as `{Context: You are a helpful weather assistant}`.
        /// </summary>
        public string? SessionSettingsContextText { get; set; }

        /// <summary>
        /// The persistence level of the injected context. Specifies how long the injected context will remain active in the session.
        ///
        /// - **Temporary**: Context that is only applied to the following assistant response.
        ///
        /// - **Persistent**: Context that is applied to all subsequent assistant responses for the remainder of the Chat.
        /// </summary>
        public string? SessionSettingsContextType { get; set; }

        /// <summary>
        /// Used to manage conversational state, correlate frontend and backend data, and persist conversations across EVI sessions.
        /// </summary>
        public string? SessionSettingsCustomSessionId { get; set; }

        /// <summary>
        /// The maximum number of chat events to return from chat history. By default, the system returns up to 300 events (100 events per page × 3 pages). Set this parameter to a smaller value to limit the number of events returned.
        /// </summary>
        public int? SessionSettingsEventLimit { get; set; }

        /// <summary>
        /// Third party API key for the supplemental language model.
        ///
        /// When provided, EVI will use this key instead of Hume's API key for the supplemental LLM. This allows you to bypass rate limits and utilize your own API key as needed.
        /// </summary>
        public string? SessionSettingsLanguageModelApiKey { get; set; }

        /// <summary>
        /// Instructions used to shape EVI's behavior, responses, and style for the session.
        ///
        /// When included in a Session Settings message, the provided Prompt overrides the existing one specified in the EVI configuration. If no Prompt was defined in the configuration, this Prompt will be the one used for the session.
        ///
        /// You can use the Prompt to define a specific goal or role for EVI, specifying how it should act or what it should focus on during the conversation. For example, EVI can be instructed to act as a customer support representative, a fitness coach, or a travel advisor, each with its own set of behaviors and response styles.
        ///
        /// For help writing a system prompt, see our [Prompting Guide](/docs/speech-to-speech-evi/guides/prompting).
        /// </summary>
        public string? SessionSettingsSystemPrompt { get; set; }

        /// <summary>
        /// This field allows you to assign values to dynamic variables referenced in your system prompt.
        ///
        /// Each key represents the variable name, and the corresponding value is the specific content you wish to assign to that variable within the session. While the values for variables can be strings, numbers, or booleans, the value will ultimately be converted to a string when injected into your system prompt.
        ///
        /// Using this field, you can personalize responses based on session-specific details. For more guidance, see our [guide on using dynamic variables](/docs/speech-to-speech-evi/features/dynamic-variables).
        /// </summary>
        public string? SessionSettingsVariables { get; set; }

        /// <summary>
        /// The name or ID of the voice from the `Voice Library` to be used as the speaker for this EVI session. This will override the speaker set in the selected configuration.
        /// </summary>
        public string? SessionSettingsVoiceId { get; set; }

        /// <summary>
        /// A flag to enable verbose transcription. Set this query parameter to `true` to have unfinalized user transcripts be sent to the client as interim UserMessage messages. The [interim](/reference/speech-to-speech-evi/chat#receive.UserMessage.interim) field on a [UserMessage](/reference/speech-to-speech-evi/chat#receive.UserMessage) denotes whether the message is "interim" or "final."
        /// </summary>
        public bool? VerboseTranscription { get; set; }

        /// <summary>
        /// API key used for authenticating the client. If not provided, an `access_token` must be provided to authenticate.
        ///
        /// For more details, refer to the [Authentication Strategies Guide](/docs/introduction/api-key#authentication-strategies).
        /// </summary>
        public string? ApiKey { get; set; }
    }
}
