namespace HumeApi;

/// <summary>
/// Base exception class for all exceptions thrown by the SDK.
/// </summary>
public class HumeApiException(string message, Exception? innerException = null)
    : Exception(message, innerException);
