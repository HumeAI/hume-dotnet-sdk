using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

public partial class EmpathicVoiceClient
{
    private RawClient _client;

    internal EmpathicVoiceClient(RawClient client)
    {
        _client = client;
        Tools = new ToolsClient(_client);
        Prompts = new PromptsClient(_client);
        CustomVoices = new CustomVoicesClient(_client);
        Configs = new ConfigsClient(_client);
        Chats = new ChatsClient(_client);
        ChatGroups = new ChatGroupsClient(_client);
        ChatWebhooks = new ChatWebhooksClient(_client);
        Chat = new ChatClient(_client);
    }

    public ToolsClient Tools { get; }

    public PromptsClient Prompts { get; }

    public CustomVoicesClient CustomVoices { get; }

    public ConfigsClient Configs { get; }

    public ChatsClient Chats { get; }

    public ChatGroupsClient ChatGroups { get; }

    public ChatWebhooksClient ChatWebhooks { get; }

    public ChatClient Chat { get; }
}
