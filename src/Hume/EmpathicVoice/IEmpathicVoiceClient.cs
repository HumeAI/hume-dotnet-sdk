namespace Hume.EmpathicVoice;

public partial interface IEmpathicVoiceClient
{
    public IControlPlaneClient ControlPlane { get; }
    public IChatGroupsClient ChatGroups { get; }
    public IChatsClient Chats { get; }
    public IConfigsClient Configs { get; }
    public IPromptsClient Prompts { get; }
    public IToolsClient Tools { get; }

    public ChatApi CreateChatApi();

    public ChatApi CreateChatApi(ChatApi.Options options);
}
