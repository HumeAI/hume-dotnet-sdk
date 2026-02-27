using Hume.Core;

namespace Hume.EmpathicVoice;

public partial class EmpathicVoiceClient : IEmpathicVoiceClient
{
    private RawClient _client;

    internal EmpathicVoiceClient(RawClient client)
    {
        _client = client;
        ControlPlane = new ControlPlaneClient(_client);
        ChatGroups = new ChatGroupsClient(_client);
        Chats = new ChatsClient(_client);
        Configs = new ConfigsClient(_client);
        Prompts = new PromptsClient(_client);
        Tools = new ToolsClient(_client);
    }

    public IControlPlaneClient ControlPlane { get; }

    public IChatGroupsClient ChatGroups { get; }

    public IChatsClient Chats { get; }

    public IConfigsClient Configs { get; }

    public IPromptsClient Prompts { get; }

    public IToolsClient Tools { get; }

    public ControlPlaneApi CreateControlPlaneApi(ControlPlaneApi.Options options)
    {
        return new ControlPlaneApi(options);
    }

    public ChatApi CreateChatApi()
    {
        return new ChatApi(new ChatApi.Options());
    }

    public ChatApi CreateChatApi(ChatApi.Options options)
    {
        return new ChatApi(options);
    }
}
