namespace HumeApi.Core;

public interface IIsRetryableContent
{
    public bool IsRetryable { get; }
}
