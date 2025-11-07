using Hume.Core;

namespace Hume.EmpathicVoice;

public partial class EmpathicVoiceClient
{
    private RawClient _client;

    internal EmpathicVoiceClient(RawClient client)
    {
        _client = client;
        ControlPlane = new ControlPlaneClient(_client);
        Tools = new ToolsClient(_client);
        Prompts = new PromptsClient(_client);
        Configs = new ConfigsClient(_client);
        Chats = new ChatsClient(_client);
        ChatGroups = new ChatGroupsClient(_client);
    }

    public ControlPlaneClient ControlPlane { get; }

    public ToolsClient Tools { get; }

    public PromptsClient Prompts { get; }

    public ConfigsClient Configs { get; }

    public ChatsClient Chats { get; }

    public ChatGroupsClient ChatGroups { get; }

    public ControlPlaneApi CreateControlPlaneApi(ControlPlaneApi.Options options)
    {
        return new ControlPlaneApi(options);
    }

    public ChatApi CreateChatApi(ChatApi.Options options)
    {
        return new ChatApi(options);
    }
}
