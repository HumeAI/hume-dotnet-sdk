using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

public partial class ChatWebhooksClient
{
    private RawClient _client;

    internal ChatWebhooksClient(RawClient client)
    {
        _client = client;
    }
}
