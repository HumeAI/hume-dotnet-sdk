using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

public partial interface IPromptsClient
{
    /// <summary>
    /// Fetches a paginated list of **Prompts**.
    ///
    /// See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
    /// </summary>
    System.Threading.Tasks.Task<Pager<ReturnPrompt>> ListPromptsAsync(
        PromptsListPromptsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Creates a **Prompt** that can be added to an [EVI configuration](/reference/speech-to-speech-evi/configs/create-config).
    ///
    /// See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
    /// </summary>
    WithRawResponseTask<ReturnPrompt?> CreatePromptAsync(
        PostedPrompt request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Fetches a list of a **Prompt's** versions.
    ///
    /// See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
    /// </summary>
    WithRawResponseTask<ReturnPagedPrompts> ListPromptVersionsAsync(
        string id,
        PromptsListPromptVersionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Updates a **Prompt** by creating a new version of the **Prompt**.
    ///
    /// See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
    /// </summary>
    WithRawResponseTask<ReturnPrompt?> CreatePromptVersionAsync(
        string id,
        PostedPromptVersion request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Deletes a **Prompt** and its versions.
    ///
    /// See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
    /// </summary>
    System.Threading.Tasks.Task DeletePromptAsync(
        string id,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Updates the name of a **Prompt**.
    ///
    /// See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
    /// </summary>
    WithRawResponseTask<string> UpdatePromptNameAsync(
        string id,
        PostedPromptName request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Fetches a specified version of a **Prompt**.
    ///
    /// See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
    /// </summary>
    WithRawResponseTask<ReturnPrompt?> GetPromptVersionAsync(
        string id,
        int version,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Deletes a specified version of a **Prompt**.
    ///
    /// See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
    /// </summary>
    System.Threading.Tasks.Task DeletePromptVersionAsync(
        string id,
        int version,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Updates the description of a **Prompt**.
    ///
    /// See our [prompting guide](/docs/speech-to-speech-evi/guides/phone-calling) for tips on crafting your system prompt.
    /// </summary>
    WithRawResponseTask<ReturnPrompt?> UpdatePromptDescriptionAsync(
        string id,
        int version,
        PostedPromptVersionDescription request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
