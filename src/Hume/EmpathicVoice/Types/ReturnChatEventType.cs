using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(StringEnumSerializer<ReturnChatEventType>))]
[Serializable]
public readonly record struct ReturnChatEventType : IStringEnum
{
    public static readonly ReturnChatEventType FunctionCall = new(Values.FunctionCall);

    public static readonly ReturnChatEventType FunctionCallResponse = new(
        Values.FunctionCallResponse
    );

    public static readonly ReturnChatEventType ChatEndMessage = new(Values.ChatEndMessage);

    public static readonly ReturnChatEventType AgentMessage = new(Values.AgentMessage);

    public static readonly ReturnChatEventType SystemPrompt = new(Values.SystemPrompt);

    public static readonly ReturnChatEventType UserRecordingStartMessage = new(
        Values.UserRecordingStartMessage
    );

    public static readonly ReturnChatEventType ResumeOnset = new(Values.ResumeOnset);

    public static readonly ReturnChatEventType UserInterruption = new(Values.UserInterruption);

    public static readonly ReturnChatEventType ChatStartMessage = new(Values.ChatStartMessage);

    public static readonly ReturnChatEventType PauseOnset = new(Values.PauseOnset);

    public static readonly ReturnChatEventType UserMessage = new(Values.UserMessage);

    public ReturnChatEventType(string value)
    {
        Value = value;
    }

    /// <summary>
    /// The string value of the enum.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Create a string enum with the given value.
    /// </summary>
    public static ReturnChatEventType FromCustom(string value)
    {
        return new ReturnChatEventType(value);
    }

    public bool Equals(string? other)
    {
        return Value.Equals(other);
    }

    /// <summary>
    /// Returns the string value of the enum.
    /// </summary>
    public override string ToString()
    {
        return Value;
    }

    public static bool operator ==(ReturnChatEventType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ReturnChatEventType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ReturnChatEventType value) => value.Value;

    public static explicit operator ReturnChatEventType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string FunctionCall = "FUNCTION_CALL";

        public const string FunctionCallResponse = "FUNCTION_CALL_RESPONSE";

        public const string ChatEndMessage = "CHAT_END_MESSAGE";

        public const string AgentMessage = "AGENT_MESSAGE";

        public const string SystemPrompt = "SYSTEM_PROMPT";

        public const string UserRecordingStartMessage = "USER_RECORDING_START_MESSAGE";

        public const string ResumeOnset = "RESUME_ONSET";

        public const string UserInterruption = "USER_INTERRUPTION";

        public const string ChatStartMessage = "CHAT_START_MESSAGE";

        public const string PauseOnset = "PAUSE_ONSET";

        public const string UserMessage = "USER_MESSAGE";
    }
}
