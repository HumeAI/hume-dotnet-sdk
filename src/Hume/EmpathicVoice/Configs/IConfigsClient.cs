using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

public partial interface IConfigsClient
{
    /// <summary>
    /// Fetches a paginated list of **Configs**.
    ///
    /// For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
    /// </summary>
    System.Threading.Tasks.Task<Pager<ReturnConfig>> ListConfigsAsync(
        ConfigsListConfigsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Creates a **Config** which can be applied to EVI.
    ///
    /// For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
    /// </summary>
    WithRawResponseTask<ReturnConfig> CreateConfigAsync(
        PostedConfig request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Fetches a list of a **Config's** versions.
    ///
    /// For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
    /// </summary>
    System.Threading.Tasks.Task<Pager<ReturnConfig>> ListConfigVersionsAsync(
        string id,
        ConfigsListConfigVersionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Updates a **Config** by creating a new version of the **Config**.
    ///
    /// For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
    /// </summary>
    WithRawResponseTask<ReturnConfig> CreateConfigVersionAsync(
        string id,
        PostedConfigVersion request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Deletes a **Config** and its versions.
    ///
    /// For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
    /// </summary>
    System.Threading.Tasks.Task DeleteConfigAsync(
        string id,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Updates the name of a **Config**.
    ///
    /// For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
    /// </summary>
    WithRawResponseTask<string> UpdateConfigNameAsync(
        string id,
        PostedConfigName request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Fetches a specified version of a **Config**.
    ///
    /// For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
    /// </summary>
    WithRawResponseTask<ReturnConfig> GetConfigVersionAsync(
        string id,
        int version,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Deletes a specified version of a **Config**.
    ///
    /// For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
    /// </summary>
    System.Threading.Tasks.Task DeleteConfigVersionAsync(
        string id,
        int version,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Updates the description of a **Config**.
    ///
    /// For more details on configuration options and how to configure EVI, see our [configuration guide](/docs/speech-to-speech-evi/configuration).
    /// </summary>
    WithRawResponseTask<ReturnConfig> UpdateConfigDescriptionAsync(
        string id,
        int version,
        PostedConfigVersionDescription request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
