using Hume;

namespace Hume.EmpathicVoice;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
[Serializable]
public class UnprocessableEntityError(HttpValidationError body)
    : HumeClientApiException("UnprocessableEntityError", 422, body)
{
    /// <summary>
    /// The body of the response that triggered the exception.
    /// </summary>
    public new HttpValidationError Body => body;
}
