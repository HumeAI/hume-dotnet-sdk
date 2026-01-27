namespace Hume.EmpathicVoice;

public partial interface IEmpathicVoiceClient
{
    public ControlPlaneClient ControlPlane { get; }
    public ChatGroupsClient ChatGroups { get; }
    public ChatsClient Chats { get; }
    public ConfigsClient Configs { get; }
    public PromptsClient Prompts { get; }
    public ToolsClient Tools { get; }
}
