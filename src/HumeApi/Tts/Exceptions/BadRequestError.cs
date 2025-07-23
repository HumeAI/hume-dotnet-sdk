using HumeApi;

namespace HumeApi.Tts;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
[Serializable]
public class BadRequestError(ErrorResponse body) : HumeApiApiException("BadRequestError", 400, body)
{
    /// <summary>
    /// The body of the response that triggered the exception.
    /// </summary>
    public new ErrorResponse Body => body;
}
