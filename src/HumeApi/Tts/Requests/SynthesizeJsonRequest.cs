using System.Text.Json.Serialization;
using HumeApi.Core;

namespace HumeApi.Tts;

[Serializable]
public record SynthesizeJsonRequest
{
    /// <summary>
    /// Access token used for authenticating the client. If not provided, an `api_key` must be provided to authenticate.
    ///
    /// The access token is generated using both an API key and a Secret key, which provides an additional layer of security compared to using just an API key.
    ///
    /// For more details, refer to the [Authentication Strategies Guide](/docs/introduction/api-key#authentication-strategies).
    /// </summary>
    [JsonIgnore]
    public string? AccessToken { get; set; }

    [JsonIgnore]
    public required PostedTts Body { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
