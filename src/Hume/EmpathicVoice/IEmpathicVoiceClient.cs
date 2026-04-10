namespace Hume.EmpathicVoice;

public partial interface IEmpathicVoiceClient
{
    public IControlPlaneClient ControlPlane { get; }
    public IChatGroupsClient ChatGroups { get; }
    public IChatsClient Chats { get; }
    public IConfigsClient Configs { get; }
    public IPromptsClient Prompts { get; }
    public IToolsClient Tools { get; }
    IControlPlaneApi CreateControlPlaneApi(ControlPlaneApi.Options options);

    IChatApi CreateChatApi();

    IChatApi CreateChatApi(ChatApi.Options options);
}
