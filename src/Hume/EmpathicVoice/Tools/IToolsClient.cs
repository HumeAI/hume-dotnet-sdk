using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

public partial interface IToolsClient
{
    /// <summary>
    /// Fetches a paginated list of **Tools**.
    ///
    /// Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
    /// </summary>
    System.Threading.Tasks.Task<Pager<ReturnUserDefinedTool>> ListToolsAsync(
        ToolsListToolsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Creates a **Tool** that can be added to an [EVI configuration](/reference/speech-to-speech-evi/configs/create-config).
    ///
    /// Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
    /// </summary>
    WithRawResponseTask<ReturnUserDefinedTool?> CreateToolAsync(
        PostedUserDefinedTool request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Fetches a list of a **Tool's** versions.
    ///
    /// Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
    /// </summary>
    System.Threading.Tasks.Task<Pager<ReturnUserDefinedTool>> ListToolVersionsAsync(
        string id,
        ToolsListToolVersionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Updates a **Tool** by creating a new version of the **Tool**.
    ///
    /// Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
    /// </summary>
    WithRawResponseTask<ReturnUserDefinedTool?> CreateToolVersionAsync(
        string id,
        PostedUserDefinedToolVersion request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Deletes a **Tool** and its versions.
    ///
    /// Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
    /// </summary>
    System.Threading.Tasks.Task DeleteToolAsync(
        string id,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Updates the name of a **Tool**.
    ///
    /// Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
    /// </summary>
    WithRawResponseTask<string> UpdateToolNameAsync(
        string id,
        PostedUserDefinedToolName request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Fetches a specified version of a **Tool**.
    ///
    /// Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
    /// </summary>
    WithRawResponseTask<ReturnUserDefinedTool?> GetToolVersionAsync(
        string id,
        int version,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Deletes a specified version of a **Tool**.
    ///
    /// Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
    /// </summary>
    System.Threading.Tasks.Task DeleteToolVersionAsync(
        string id,
        int version,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Updates the description of a specified **Tool** version.
    ///
    /// Refer to our [tool use](/docs/speech-to-speech-evi/features/tool-use#function-calling) guide for comprehensive instructions on defining and integrating tools into EVI.
    /// </summary>
    WithRawResponseTask<ReturnUserDefinedTool?> UpdateToolDescriptionAsync(
        string id,
        int version,
        PostedUserDefinedToolVersionDescription request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
