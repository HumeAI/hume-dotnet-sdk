using System.Text.Json;
using Hume;
using Hume.Core;
using OneOf;

namespace Hume.EmpathicVoice;

public partial class ControlPlaneClient : IControlPlaneClient
{
    private RawClient _client;

    internal ControlPlaneClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Send a message to a specific chat.
    /// </summary>
    /// <example><code>
    /// await client.EmpathicVoice.ControlPlane.SendAsync(
    ///     "chat_id",
    ///     new SessionSettings { Type = "session_settings" }
    /// );
    /// </code></example>
    public async System.Threading.Tasks.Task SendAsync(
        string chatId,
        OneOf<
            SessionSettings,
            UserInput,
            AssistantInput,
            ToolResponseMessage,
            ToolErrorMessage,
            PauseAssistantMessage,
            ResumeAssistantMessage
        > request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.Environment.Base,
                    Method = HttpMethod.Post,
                    Path = string.Format(
                        "v0/evi/chat/{0}/send",
                        ValueConvert.ToPathParameterString(chatId)
                    ),
                    Body = request,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            return;
        }
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                switch (response.StatusCode)
                {
                    case 422:
                        throw new UnprocessableEntityError(
                            JsonUtils.Deserialize<HttpValidationError>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new HumeClientApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }
}
