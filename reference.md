# Reference
## EmpathicVoice ControlPlane
<details><summary><code>client.EmpathicVoice.ControlPlane.<a href="/src/Hume/EmpathicVoice/ControlPlane/ControlPlaneClient.cs">SendAsync</a>(chatId, OneOf&lt;SessionSettings, UserInput, AssistantInput, ToolResponseMessage, ToolErrorMessage, PauseAssistantMessage, ResumeAssistantMessage&gt; { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Send a message to a specific chat.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.ControlPlane.SendAsync(
    "chat_id",
    new SessionSettings { Type = "session_settings" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**chatId:** `string` 
    
</dd>
</dl>

<dl>
<dd>

**request:** `OneOf<SessionSettings, UserInput, AssistantInput, ToolResponseMessage, ToolErrorMessage, PauseAssistantMessage, ResumeAssistantMessage>` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## EmpathicVoice ChatGroups
<details><summary><code>client.EmpathicVoice.ChatGroups.<a href="/src/Hume/EmpathicVoice/ChatGroups/ChatGroupsClient.cs">ListChatGroupsAsync</a>(ChatGroupsListChatGroupsRequest { ... }) -> Pager&lt;ReturnChatGroup&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Fetches a paginated list of **Chat Groups**.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.ChatGroups.ListChatGroupsAsync(
    new ChatGroupsListChatGroupsRequest
    {
        PageNumber = 0,
        PageSize = 1,
        AscendingOrder = true,
        ConfigId = "1b60e1a0-cc59-424a-8d2c-189d354db3f3",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ChatGroupsListChatGroupsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.ChatGroups.<a href="/src/Hume/EmpathicVoice/ChatGroups/ChatGroupsClient.cs">GetChatGroupAsync</a>(id, ChatGroupsGetChatGroupRequest { ... }) -> WithRawResponseTask&lt;ReturnChatGroupPagedChats&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Fetches a **ChatGroup** by ID, including a paginated list of **Chats** associated with the **ChatGroup**.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.ChatGroups.GetChatGroupAsync(
    "697056f0-6c7e-487d-9bd8-9c19df79f05f",
    new ChatGroupsGetChatGroupRequest
    {
        PageNumber = 0,
        PageSize = 1,
        AscendingOrder = true,
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Chat Group. Formatted as a UUID.
    
</dd>
</dl>

<dl>
<dd>

**request:** `ChatGroupsGetChatGroupRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.ChatGroups.<a href="/src/Hume/EmpathicVoice/ChatGroups/ChatGroupsClient.cs">GetAudioAsync</a>(id, ChatGroupsGetAudioRequest { ... }) -> WithRawResponseTask&lt;ReturnChatGroupPagedAudioReconstructions&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Fetches a paginated list of audio for each **Chat** within the specified **Chat Group**. For more details, see our guide on audio reconstruction [here](/docs/speech-to-speech-evi/faq#can-i-access-the-audio-of-previous-conversations-with-evi).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.ChatGroups.GetAudioAsync(
    "369846cf-6ad5-404d-905e-a8acb5cdfc78",
    new ChatGroupsGetAudioRequest
    {
        PageNumber = 0,
        PageSize = 10,
        AscendingOrder = true,
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Chat Group. Formatted as a UUID.
    
</dd>
</dl>

<dl>
<dd>

**request:** `ChatGroupsGetAudioRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.ChatGroups.<a href="/src/Hume/EmpathicVoice/ChatGroups/ChatGroupsClient.cs">ListChatGroupEventsAsync</a>(id, ChatGroupsListChatGroupEventsRequest { ... }) -> Pager&lt;ReturnChatEvent&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Fetches a paginated list of **Chat** events associated with a **Chat Group**.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.ChatGroups.ListChatGroupEventsAsync(
    "697056f0-6c7e-487d-9bd8-9c19df79f05f",
    new ChatGroupsListChatGroupEventsRequest
    {
        PageNumber = 0,
        PageSize = 3,
        AscendingOrder = true,
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Chat Group. Formatted as a UUID.
    
</dd>
</dl>

<dl>
<dd>

**request:** `ChatGroupsListChatGroupEventsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## EmpathicVoice Chats
<details><summary><code>client.EmpathicVoice.Chats.<a href="/src/Hume/EmpathicVoice/Chats/ChatsClient.cs">ListChatsAsync</a>(ChatsListChatsRequest { ... }) -> Pager&lt;ReturnChat&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Fetches a paginated list of **Chats**.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Chats.ListChatsAsync(
    new ChatsListChatsRequest
    {
        PageNumber = 0,
        PageSize = 1,
        AscendingOrder = true,
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ChatsListChatsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Chats.<a href="/src/Hume/EmpathicVoice/Chats/ChatsClient.cs">ListChatEventsAsync</a>(id, ChatsListChatEventsRequest { ... }) -> Pager&lt;ReturnChatEvent&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Fetches a paginated list of **Chat** events.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Chats.ListChatEventsAsync(
    "470a49f6-1dec-4afe-8b61-035d3b2d63b0",
    new ChatsListChatEventsRequest
    {
        PageNumber = 0,
        PageSize = 3,
        AscendingOrder = true,
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Chat. Formatted as a UUID.
    
</dd>
</dl>

<dl>
<dd>

**request:** `ChatsListChatEventsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Chats.<a href="/src/Hume/EmpathicVoice/Chats/ChatsClient.cs">GetAudioAsync</a>(id) -> WithRawResponseTask&lt;ReturnChatAudioReconstruction&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Fetches the audio of a previous **Chat**. For more details, see our guide on audio reconstruction [here](/docs/speech-to-speech-evi/faq#can-i-access-the-audio-of-previous-conversations-with-evi).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Chats.GetAudioAsync("470a49f6-1dec-4afe-8b61-035d3b2d63b0");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Chat. Formatted as a UUID.
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## EmpathicVoice Configs
<details><summary><code>client.EmpathicVoice.Configs.<a href="/src/Hume/EmpathicVoice/Configs/ConfigsClient.cs">ListConfigsAsync</a>(ConfigsListConfigsRequest { ... }) -> Pager&lt;ReturnConfig&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Fetches a paginated list of **Configs**.

For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Configs.ListConfigsAsync(
    new ConfigsListConfigsRequest { PageNumber = 0, PageSize = 1 }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ConfigsListConfigsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Configs.<a href="/src/Hume/EmpathicVoice/Configs/ConfigsClient.cs">CreateConfigAsync</a>(PostedConfig { ... }) -> WithRawResponseTask&lt;ReturnConfig&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Creates a **Config** which can be applied to EVI.

For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Configs.CreateConfigAsync(
    new PostedConfig
    {
        Name = "Weather Assistant Config",
        Prompt = new PostedConfigPromptSpec
        {
            Id = "af699d45-2985-42cc-91b9-af9e5da3bac5",
            Version = 0,
        },
        EviVersion = "3",
        Voice = new VoiceName
        {
            Provider = Hume.EmpathicVoice.VoiceProvider.HumeAi,
            Name = "Ava Song",
        },
        LanguageModel = new PostedLanguageModel
        {
            ModelProvider = ModelProviderEnum.Anthropic,
            ModelResource = LanguageModelType.Claude37SonnetLatest,
            Temperature = 1f,
        },
        EventMessages = new PostedEventMessageSpecs
        {
            OnNewChat = new PostedEventMessageSpec { Enabled = false, Text = "" },
            OnInactivityTimeout = new PostedEventMessageSpec { Enabled = false, Text = "" },
            OnMaxDurationTimeout = new PostedEventMessageSpec { Enabled = false, Text = "" },
        },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `PostedConfig` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Configs.<a href="/src/Hume/EmpathicVoice/Configs/ConfigsClient.cs">ListConfigVersionsAsync</a>(id, ConfigsListConfigVersionsRequest { ... }) -> Pager&lt;ReturnConfig&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Fetches a list of a **Config's** versions.

For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Configs.ListConfigVersionsAsync(
    "1b60e1a0-cc59-424a-8d2c-189d354db3f3",
    new ConfigsListConfigVersionsRequest()
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Config. Formatted as a UUID.
    
</dd>
</dl>

<dl>
<dd>

**request:** `ConfigsListConfigVersionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Configs.<a href="/src/Hume/EmpathicVoice/Configs/ConfigsClient.cs">CreateConfigVersionAsync</a>(id, PostedConfigVersion { ... }) -> WithRawResponseTask&lt;ReturnConfig&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Updates a **Config** by creating a new version of the **Config**.

For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Configs.CreateConfigVersionAsync(
    "1b60e1a0-cc59-424a-8d2c-189d354db3f3",
    new PostedConfigVersion
    {
        VersionDescription = "This is an updated version of the Weather Assistant Config.",
        EviVersion = "3",
        Prompt = new PostedConfigPromptSpec
        {
            Id = "af699d45-2985-42cc-91b9-af9e5da3bac5",
            Version = 0,
        },
        Voice = new VoiceName
        {
            Provider = Hume.EmpathicVoice.VoiceProvider.HumeAi,
            Name = "Ava Song",
        },
        LanguageModel = new PostedLanguageModel
        {
            ModelProvider = ModelProviderEnum.Anthropic,
            ModelResource = LanguageModelType.Claude37SonnetLatest,
            Temperature = 1f,
        },
        EllmModel = new PostedEllmModel { AllowShortResponses = true },
        EventMessages = new PostedEventMessageSpecs
        {
            OnNewChat = new PostedEventMessageSpec { Enabled = false, Text = "" },
            OnInactivityTimeout = new PostedEventMessageSpec { Enabled = false, Text = "" },
            OnMaxDurationTimeout = new PostedEventMessageSpec { Enabled = false, Text = "" },
        },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Config. Formatted as a UUID.
    
</dd>
</dl>

<dl>
<dd>

**request:** `PostedConfigVersion` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Configs.<a href="/src/Hume/EmpathicVoice/Configs/ConfigsClient.cs">DeleteConfigAsync</a>(id)</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Deletes a **Config** and its versions.

For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Configs.DeleteConfigAsync("1b60e1a0-cc59-424a-8d2c-189d354db3f3");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Config. Formatted as a UUID.
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Configs.<a href="/src/Hume/EmpathicVoice/Configs/ConfigsClient.cs">UpdateConfigNameAsync</a>(id, PostedConfigName { ... }) -> WithRawResponseTask&lt;string&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Updates the name of a **Config**.

For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Configs.UpdateConfigNameAsync(
    "1b60e1a0-cc59-424a-8d2c-189d354db3f3",
    new PostedConfigName { Name = "Updated Weather Assistant Config Name" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Config. Formatted as a UUID.
    
</dd>
</dl>

<dl>
<dd>

**request:** `PostedConfigName` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Configs.<a href="/src/Hume/EmpathicVoice/Configs/ConfigsClient.cs">GetConfigVersionAsync</a>(id, version) -> WithRawResponseTask&lt;ReturnConfig&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Fetches a specified version of a **Config**.

For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Configs.GetConfigVersionAsync("1b60e1a0-cc59-424a-8d2c-189d354db3f3", 1);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Config. Formatted as a UUID.
    
</dd>
</dl>

<dl>
<dd>

**version:** `int` 

Version number for a Config.

Configs, Prompts, Custom Voices, and Tools are versioned. This versioning system supports iterative development, allowing you to progressively refine configurations and revert to previous versions if needed.

Version numbers are integer values representing different iterations of the Config. Each update to the Config increments its version number.
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Configs.<a href="/src/Hume/EmpathicVoice/Configs/ConfigsClient.cs">DeleteConfigVersionAsync</a>(id, version)</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Deletes a specified version of a **Config**.

For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Configs.DeleteConfigVersionAsync(
    "1b60e1a0-cc59-424a-8d2c-189d354db3f3",
    1
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Config. Formatted as a UUID.
    
</dd>
</dl>

<dl>
<dd>

**version:** `int` 

Version number for a Config.

Configs, Prompts, Custom Voices, and Tools are versioned. This versioning system supports iterative development, allowing you to progressively refine configurations and revert to previous versions if needed.

Version numbers are integer values representing different iterations of the Config. Each update to the Config increments its version number.
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Configs.<a href="/src/Hume/EmpathicVoice/Configs/ConfigsClient.cs">UpdateConfigDescriptionAsync</a>(id, version, PostedConfigVersionDescription { ... }) -> WithRawResponseTask&lt;ReturnConfig&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Updates the description of a **Config**.

For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Configs.UpdateConfigDescriptionAsync(
    "1b60e1a0-cc59-424a-8d2c-189d354db3f3",
    1,
    new PostedConfigVersionDescription
    {
        VersionDescription = "This is an updated version_description.",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Config. Formatted as a UUID.
    
</dd>
</dl>

<dl>
<dd>

**version:** `int` 

Version number for a Config.

Configs, Prompts, Custom Voices, and Tools are versioned. This versioning system supports iterative development, allowing you to progressively refine configurations and revert to previous versions if needed.

Version numbers are integer values representing different iterations of the Config. Each update to the Config increments its version number.
    
</dd>
</dl>

<dl>
<dd>

**request:** `PostedConfigVersionDescription` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## EmpathicVoice Prompts
<details><summary><code>client.EmpathicVoice.Prompts.<a href="/src/Hume/EmpathicVoice/Prompts/PromptsClient.cs">ListPromptsAsync</a>(PromptsListPromptsRequest { ... }) -> Pager&lt;ReturnPrompt&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Fetches a paginated list of **Prompts**.

See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Prompts.ListPromptsAsync(
    new PromptsListPromptsRequest { PageNumber = 0, PageSize = 2 }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `PromptsListPromptsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Prompts.<a href="/src/Hume/EmpathicVoice/Prompts/PromptsClient.cs">CreatePromptAsync</a>(PostedPrompt { ... }) -> WithRawResponseTask&lt;ReturnPrompt?&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Creates a **Prompt** that can be added to an [EVI configuration](/reference/speech-to-speech-evi/configs/create-config).

See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Prompts.CreatePromptAsync(
    new PostedPrompt
    {
        Name = "Weather Assistant Prompt",
        Text =
            "<role>You are an AI weather assistant providing users with accurate and up-to-date weather information. Respond to user queries concisely and clearly. Use simple language and avoid technical jargon. Provide temperature, precipitation, wind conditions, and any weather alerts. Include helpful tips if severe weather is expected.</role>",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `PostedPrompt` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Prompts.<a href="/src/Hume/EmpathicVoice/Prompts/PromptsClient.cs">ListPromptVersionsAsync</a>(id, PromptsListPromptVersionsRequest { ... }) -> WithRawResponseTask&lt;ReturnPagedPrompts&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Fetches a list of a **Prompt's** versions.

See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Prompts.ListPromptVersionsAsync(
    "af699d45-2985-42cc-91b9-af9e5da3bac5",
    new PromptsListPromptVersionsRequest()
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` 
    
</dd>
</dl>

<dl>
<dd>

**request:** `PromptsListPromptVersionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Prompts.<a href="/src/Hume/EmpathicVoice/Prompts/PromptsClient.cs">CreatePromptVersionAsync</a>(id, PostedPromptVersion { ... }) -> WithRawResponseTask&lt;ReturnPrompt?&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Updates a **Prompt** by creating a new version of the **Prompt**.

See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Prompts.CreatePromptVersionAsync(
    "af699d45-2985-42cc-91b9-af9e5da3bac5",
    new PostedPromptVersion
    {
        Text =
            "<role>You are an updated version of an AI weather assistant providing users with accurate and up-to-date weather information. Respond to user queries concisely and clearly. Use simple language and avoid technical jargon. Provide temperature, precipitation, wind conditions, and any weather alerts. Include helpful tips if severe weather is expected.</role>",
        VersionDescription = "This is an updated version of the Weather Assistant Prompt.",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Prompt. Formatted as a UUID.
    
</dd>
</dl>

<dl>
<dd>

**request:** `PostedPromptVersion` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Prompts.<a href="/src/Hume/EmpathicVoice/Prompts/PromptsClient.cs">DeletePromptAsync</a>(id)</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Deletes a **Prompt** and its versions.

See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Prompts.DeletePromptAsync("af699d45-2985-42cc-91b9-af9e5da3bac5");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Prompt. Formatted as a UUID.
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Prompts.<a href="/src/Hume/EmpathicVoice/Prompts/PromptsClient.cs">UpdatePromptNameAsync</a>(id, PostedPromptName { ... }) -> WithRawResponseTask&lt;string&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Updates the name of a **Prompt**.

See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Prompts.UpdatePromptNameAsync(
    "af699d45-2985-42cc-91b9-af9e5da3bac5",
    new PostedPromptName { Name = "Updated Weather Assistant Prompt Name" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Prompt. Formatted as a UUID.
    
</dd>
</dl>

<dl>
<dd>

**request:** `PostedPromptName` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Prompts.<a href="/src/Hume/EmpathicVoice/Prompts/PromptsClient.cs">GetPromptVersionAsync</a>(id, version) -> WithRawResponseTask&lt;ReturnPrompt?&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Fetches a specified version of a **Prompt**.

See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Prompts.GetPromptVersionAsync("af699d45-2985-42cc-91b9-af9e5da3bac5", 0);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Prompt. Formatted as a UUID.
    
</dd>
</dl>

<dl>
<dd>

**version:** `int` 

Version number for a Prompt.

Prompts, Configs, Custom Voices, and Tools are versioned. This versioning system supports iterative development, allowing you to progressively refine prompts and revert to previous versions if needed.

Version numbers are integer values representing different iterations of the Prompt. Each update to the Prompt increments its version number.
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Prompts.<a href="/src/Hume/EmpathicVoice/Prompts/PromptsClient.cs">DeletePromptVersionAsync</a>(id, version)</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Deletes a specified version of a **Prompt**.

See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Prompts.DeletePromptVersionAsync(
    "af699d45-2985-42cc-91b9-af9e5da3bac5",
    1
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Prompt. Formatted as a UUID.
    
</dd>
</dl>

<dl>
<dd>

**version:** `int` 

Version number for a Prompt.

Prompts, Configs, Custom Voices, and Tools are versioned. This versioning system supports iterative development, allowing you to progressively refine prompts and revert to previous versions if needed.

Version numbers are integer values representing different iterations of the Prompt. Each update to the Prompt increments its version number.
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Prompts.<a href="/src/Hume/EmpathicVoice/Prompts/PromptsClient.cs">UpdatePromptDescriptionAsync</a>(id, version, PostedPromptVersionDescription { ... }) -> WithRawResponseTask&lt;ReturnPrompt?&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Updates the description of a **Prompt**.

See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Prompts.UpdatePromptDescriptionAsync(
    "af699d45-2985-42cc-91b9-af9e5da3bac5",
    1,
    new PostedPromptVersionDescription
    {
        VersionDescription = "This is an updated version_description.",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Prompt. Formatted as a UUID.
    
</dd>
</dl>

<dl>
<dd>

**version:** `int` 

Version number for a Prompt.

Prompts, Configs, Custom Voices, and Tools are versioned. This versioning system supports iterative development, allowing you to progressively refine prompts and revert to previous versions if needed.

Version numbers are integer values representing different iterations of the Prompt. Each update to the Prompt increments its version number.
    
</dd>
</dl>

<dl>
<dd>

**request:** `PostedPromptVersionDescription` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## EmpathicVoice Tools
<details><summary><code>client.EmpathicVoice.Tools.<a href="/src/Hume/EmpathicVoice/Tools/ToolsClient.cs">ListToolsAsync</a>(ToolsListToolsRequest { ... }) -> Pager&lt;ReturnUserDefinedTool&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Fetches a paginated list of **Tools**.

Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Tools.ListToolsAsync(
    new ToolsListToolsRequest { PageNumber = 0, PageSize = 2 }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ToolsListToolsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Tools.<a href="/src/Hume/EmpathicVoice/Tools/ToolsClient.cs">CreateToolAsync</a>(PostedUserDefinedTool { ... }) -> WithRawResponseTask&lt;ReturnUserDefinedTool?&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Creates a **Tool** that can be added to an [EVI configuration](/reference/speech-to-speech-evi/configs/create-config).

Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Tools.CreateToolAsync(
    new PostedUserDefinedTool
    {
        Name = "get_current_weather",
        Parameters =
            "{ \"type\": \"object\", \"properties\": { \"location\": { \"type\": \"string\", \"description\": \"The city and state, e.g. San Francisco, CA\" }, \"format\": { \"type\": \"string\", \"enum\": [\"celsius\", \"fahrenheit\"], \"description\": \"The temperature unit to use. Infer this from the users location.\" } }, \"required\": [\"location\", \"format\"] }",
        VersionDescription =
            "Fetches current weather and uses celsius or fahrenheit based on location of user.",
        Description = "This tool is for getting the current weather.",
        FallbackContent = "Unable to fetch current weather.",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `PostedUserDefinedTool` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Tools.<a href="/src/Hume/EmpathicVoice/Tools/ToolsClient.cs">ListToolVersionsAsync</a>(id, ToolsListToolVersionsRequest { ... }) -> Pager&lt;ReturnUserDefinedTool&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Fetches a list of a **Tool's** versions.

Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Tools.ListToolVersionsAsync(
    "00183a3f-79ba-413d-9f3b-609864268bea",
    new ToolsListToolVersionsRequest()
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` 
    
</dd>
</dl>

<dl>
<dd>

**request:** `ToolsListToolVersionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Tools.<a href="/src/Hume/EmpathicVoice/Tools/ToolsClient.cs">CreateToolVersionAsync</a>(id, PostedUserDefinedToolVersion { ... }) -> WithRawResponseTask&lt;ReturnUserDefinedTool?&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Updates a **Tool** by creating a new version of the **Tool**.

Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Tools.CreateToolVersionAsync(
    "00183a3f-79ba-413d-9f3b-609864268bea",
    new PostedUserDefinedToolVersion
    {
        Parameters =
            "{ \"type\": \"object\", \"properties\": { \"location\": { \"type\": \"string\", \"description\": \"The city and state, e.g. San Francisco, CA\" }, \"format\": { \"type\": \"string\", \"enum\": [\"celsius\", \"fahrenheit\", \"kelvin\"], \"description\": \"The temperature unit to use. Infer this from the users location.\" } }, \"required\": [\"location\", \"format\"] }",
        VersionDescription =
            "Fetches current weather and uses celsius, fahrenheit, or kelvin based on location of user.",
        FallbackContent = "Unable to fetch current weather.",
        Description = "This tool is for getting the current weather.",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Tool. Formatted as a UUID.
    
</dd>
</dl>

<dl>
<dd>

**request:** `PostedUserDefinedToolVersion` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Tools.<a href="/src/Hume/EmpathicVoice/Tools/ToolsClient.cs">DeleteToolAsync</a>(id)</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Deletes a **Tool** and its versions.

Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Tools.DeleteToolAsync("00183a3f-79ba-413d-9f3b-609864268bea");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Tool. Formatted as a UUID.
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Tools.<a href="/src/Hume/EmpathicVoice/Tools/ToolsClient.cs">UpdateToolNameAsync</a>(id, PostedUserDefinedToolName { ... }) -> WithRawResponseTask&lt;string&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Updates the name of a **Tool**.

Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Tools.UpdateToolNameAsync(
    "00183a3f-79ba-413d-9f3b-609864268bea",
    new PostedUserDefinedToolName { Name = "get_current_temperature" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Tool. Formatted as a UUID.
    
</dd>
</dl>

<dl>
<dd>

**request:** `PostedUserDefinedToolName` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Tools.<a href="/src/Hume/EmpathicVoice/Tools/ToolsClient.cs">GetToolVersionAsync</a>(id, version) -> WithRawResponseTask&lt;ReturnUserDefinedTool?&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Fetches a specified version of a **Tool**.

Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Tools.GetToolVersionAsync("00183a3f-79ba-413d-9f3b-609864268bea", 1);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Tool. Formatted as a UUID.
    
</dd>
</dl>

<dl>
<dd>

**version:** `int` 

Version number for a Tool.

Tools, Configs, Custom Voices, and Prompts are versioned. This versioning system supports iterative development, allowing you to progressively refine tools and revert to previous versions if needed.

Version numbers are integer values representing different iterations of the Tool. Each update to the Tool increments its version number.
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Tools.<a href="/src/Hume/EmpathicVoice/Tools/ToolsClient.cs">DeleteToolVersionAsync</a>(id, version)</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Deletes a specified version of a **Tool**.

Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Tools.DeleteToolVersionAsync("00183a3f-79ba-413d-9f3b-609864268bea", 1);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Tool. Formatted as a UUID.
    
</dd>
</dl>

<dl>
<dd>

**version:** `int` 

Version number for a Tool.

Tools, Configs, Custom Voices, and Prompts are versioned. This versioning system supports iterative development, allowing you to progressively refine tools and revert to previous versions if needed.

Version numbers are integer values representing different iterations of the Tool. Each update to the Tool increments its version number.
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.EmpathicVoice.Tools.<a href="/src/Hume/EmpathicVoice/Tools/ToolsClient.cs">UpdateToolDescriptionAsync</a>(id, version, PostedUserDefinedToolVersionDescription { ... }) -> WithRawResponseTask&lt;ReturnUserDefinedTool?&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Updates the description of a specified **Tool** version.

Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.EmpathicVoice.Tools.UpdateToolDescriptionAsync(
    "00183a3f-79ba-413d-9f3b-609864268bea",
    1,
    new PostedUserDefinedToolVersionDescription
    {
        VersionDescription =
            "Fetches current temperature, precipitation, wind speed, AQI, and other weather conditions. Uses Celsius, Fahrenheit, or kelvin depending on user's region.",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Identifier for a Tool. Formatted as a UUID.
    
</dd>
</dl>

<dl>
<dd>

**version:** `int` 

Version number for a Tool.

Tools, Configs, Custom Voices, and Prompts are versioned. This versioning system supports iterative development, allowing you to progressively refine tools and revert to previous versions if needed.

Version numbers are integer values representing different iterations of the Tool. Each update to the Tool increments its version number.
    
</dd>
</dl>

<dl>
<dd>

**request:** `PostedUserDefinedToolVersionDescription` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Tts Voices
<details><summary><code>client.Tts.Voices.<a href="/src/Hume/Tts/Voices/VoicesClient.cs">ListAsync</a>(VoicesListRequest { ... }) -> Pager&lt;ReturnVoice&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Lists voices you have saved in your account, or voices from the [Voice Library](https://app.hume.ai/tts/voice-library).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Tts.Voices.ListAsync(
    new VoicesListRequest { Provider = Hume.Tts.VoiceProvider.CustomVoice }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `VoicesListRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Tts.Voices.<a href="/src/Hume/Tts/Voices/VoicesClient.cs">CreateAsync</a>(PostedVoice { ... }) -> WithRawResponseTask&lt;ReturnVoice&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Saves a new custom voice to your account using the specified TTS generation ID.

Once saved, this voice can be reused in subsequent TTS requests, ensuring consistent speech style and prosody. For more details on voice creation, see the [Voices Guide](/docs/text-to-speech-tts/voices).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Tts.Voices.CreateAsync(
    new PostedVoice { GenerationId = "795c949a-1510-4a80-9646-7d0863b023ab", Name = "David Hume" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `PostedVoice` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Tts.Voices.<a href="/src/Hume/Tts/Voices/VoicesClient.cs">DeleteAsync</a>(VoicesDeleteRequest { ... })</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Deletes a previously generated custom voice.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Tts.Voices.DeleteAsync(new VoicesDeleteRequest { Name = "David Hume" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `VoicesDeleteRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Tts
<details><summary><code>client.Tts.<a href="/src/Hume/Tts/TtsClient.cs">SynthesizeJsonAsync</a>(PostedTts { ... }) -> WithRawResponseTask&lt;ReturnTts&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Synthesizes one or more input texts into speech using the specified voice. If no voice is provided, a novel voice will be generated dynamically. Optionally, additional context can be included to influence the speech's style and prosody.

The response includes the base64-encoded audio and metadata in JSON format.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Tts.SynthesizeJsonAsync(
    new PostedTts
    {
        Context = new PostedContextWithUtterances
        {
            Utterances = new List<PostedUtterance>()
            {
                new PostedUtterance
                {
                    Text = "How can people see beauty so differently?",
                    Description =
                        "A curious student with a clear and respectful tone, seeking clarification on Hume's ideas with a straightforward question.",
                },
            },
        },
        Format = new FormatMp3 { Type = "mp3" },
        NumGenerations = 1,
        Utterances = new List<PostedUtterance>()
        {
            new PostedUtterance
            {
                Text =
                    "Beauty is no quality in things themselves: It exists merely in the mind which contemplates them.",
                Description =
                    "Middle-aged masculine voice with a clear, rhythmic Scots lilt, rounded vowels, and a warm, steady tone with an articulate, academic quality.",
            },
        },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `PostedTts` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Tts.<a href="/src/Hume/Tts/TtsClient.cs">SynthesizeFileAsync</a>(PostedTts { ... }) -> WithRawResponseTask&lt;Stream&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Synthesizes one or more input texts into speech using the specified voice. If no voice is provided, a novel voice will be generated dynamically. Optionally, additional context can be included to influence the speech's style and prosody. 

The response contains the generated audio file in the requested format.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Tts.SynthesizeFileAsync(
    new PostedTts
    {
        Context = new PostedContextWithGenerationId
        {
            GenerationId = "09ad914d-8e7f-40f8-a279-e34f07f7dab2",
        },
        Format = new FormatMp3 { Type = "mp3" },
        NumGenerations = 1,
        Utterances = new List<PostedUtterance>()
        {
            new PostedUtterance
            {
                Text =
                    "Beauty is no quality in things themselves: It exists merely in the mind which contemplates them.",
                Description =
                    "Middle-aged masculine voice with a clear, rhythmic Scots lilt, rounded vowels, and a warm, steady tone with an articulate, academic quality.",
            },
        },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `PostedTts` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Tts.<a href="/src/Hume/Tts/TtsClient.cs">SynthesizeFileStreamingAsync</a>(PostedTts { ... }) -> WithRawResponseTask&lt;Stream&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Streams synthesized speech using the specified voice. If no voice is provided, a novel voice will be generated dynamically. Optionally, additional context can be included to influence the speech's style and prosody.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Tts.SynthesizeFileStreamingAsync(
    new PostedTts
    {
        Utterances = new List<PostedUtterance>()
        {
            new PostedUtterance
            {
                Text =
                    "Beauty is no quality in things themselves: It exists merely in the mind which contemplates them.",
                Voice = new PostedUtteranceVoiceWithName
                {
                    Name = "Male English Actor",
                    Provider = Hume.Tts.VoiceProvider.HumeAi,
                },
            },
        },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `PostedTts` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Tts.<a href="/src/Hume/Tts/TtsClient.cs">SynthesizeJsonStreamingAsync</a>(PostedTts { ... }) -> IAsyncEnumerable&lt;OneOf&lt;SnippetAudioChunk, TimestampMessage&gt;&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Streams synthesized speech using the specified voice. If no voice is provided, a novel voice will be generated dynamically. Optionally, additional context can be included to influence the speech's style and prosody. 

The response is a stream of JSON objects including audio encoded in base64.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
client.Tts.SynthesizeJsonStreamingAsync(
    new PostedTts
    {
        Utterances = new List<PostedUtterance>()
        {
            new PostedUtterance
            {
                Text =
                    "Beauty is no quality in things themselves: It exists merely in the mind which contemplates them.",
                Voice = new PostedUtteranceVoiceWithName
                {
                    Name = "Male English Actor",
                    Provider = Hume.Tts.VoiceProvider.HumeAi,
                },
            },
        },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `PostedTts` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Tts.<a href="/src/Hume/Tts/TtsClient.cs">ConvertVoiceJsonAsync</a>(ConvertVoiceJsonRequest { ... }) -> IAsyncEnumerable&lt;OneOf&lt;SnippetAudioChunk, TimestampMessage&gt;&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
client.Tts.ConvertVoiceJsonAsync(new ConvertVoiceJsonRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ConvertVoiceJsonRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## ExpressionMeasurement Batch
<details><summary><code>client.ExpressionMeasurement.Batch.<a href="/src/Hume/ExpressionMeasurement/Batch/BatchClient.cs">ListJobsAsync</a>(BatchListJobsRequest { ... }) -> WithRawResponseTask&lt;IEnumerable&lt;InferenceJob&gt;&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Sort and filter jobs.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.ExpressionMeasurement.Batch.ListJobsAsync(new BatchListJobsRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `BatchListJobsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.ExpressionMeasurement.Batch.<a href="/src/Hume/ExpressionMeasurement/Batch/BatchClient.cs">StartInferenceJobAsync</a>(InferenceBaseRequest { ... }) -> WithRawResponseTask&lt;JobId&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Start a new measurement inference job.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.ExpressionMeasurement.Batch.StartInferenceJobAsync(
    new InferenceBaseRequest
    {
        Urls = new List<string>() { "https://hume-tutorials.s3.amazonaws.com/faces.zip" },
        Notify = true,
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `InferenceBaseRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.ExpressionMeasurement.Batch.<a href="/src/Hume/ExpressionMeasurement/Batch/BatchClient.cs">GetJobDetailsAsync</a>(id) -> WithRawResponseTask&lt;InferenceJob&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Get the request details and state of a given job.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.ExpressionMeasurement.Batch.GetJobDetailsAsync("job_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — The unique identifier for the job.
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.ExpressionMeasurement.Batch.<a href="/src/Hume/ExpressionMeasurement/Batch/BatchClient.cs">GetJobPredictionsAsync</a>(id) -> WithRawResponseTask&lt;IEnumerable&lt;InferenceSourcePredictResult&gt;&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Get the JSON predictions of a completed inference job.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.ExpressionMeasurement.Batch.GetJobPredictionsAsync("job_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — The unique identifier for the job.
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.ExpressionMeasurement.Batch.<a href="/src/Hume/ExpressionMeasurement/Batch/BatchClient.cs">StartInferenceJobFromLocalFileAsync</a>(BatchStartInferenceJobFromLocalFileRequest { ... }) -> WithRawResponseTask&lt;JobId&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Start a new batch inference job.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.ExpressionMeasurement.Batch.StartInferenceJobFromLocalFileAsync(
    new BatchStartInferenceJobFromLocalFileRequest()
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `BatchStartInferenceJobFromLocalFileRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>
