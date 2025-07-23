using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

public partial class ChatClient
{
    private RawClient _client;

    internal ChatClient(RawClient client)
    {
        _client = client;
    }
}
