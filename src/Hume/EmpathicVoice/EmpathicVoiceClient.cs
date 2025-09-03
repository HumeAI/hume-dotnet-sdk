using Hume.Core;

namespace Hume.EmpathicVoice;

public partial class EmpathicVoiceClient
{
    private RawClient _client;

    internal EmpathicVoiceClient(RawClient client)
    {
        _client = client;
        Tools = new ToolsClient(_client);
        Prompts = new PromptsClient(_client);
        Configs = new ConfigsClient(_client);
        Chats = new ChatsClient(_client);
        ChatGroups = new ChatGroupsClient(_client);
    }

    public ToolsClient Tools { get; }

    public PromptsClient Prompts { get; }

    public ConfigsClient Configs { get; }

    public ChatsClient Chats { get; }

    public ChatGroupsClient ChatGroups { get; }
}
