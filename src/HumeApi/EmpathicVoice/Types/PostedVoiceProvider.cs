using System.Text.Json.Serialization;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

[JsonConverter(typeof(StringEnumSerializer<PostedVoiceProvider>))]
[Serializable]
public readonly record struct PostedVoiceProvider : IStringEnum
{
    public static readonly PostedVoiceProvider HumeAi = new(Values.HumeAi);

    public static readonly PostedVoiceProvider CustomVoice = new(Values.CustomVoice);

    public PostedVoiceProvider(string value)
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
    public static PostedVoiceProvider FromCustom(string value)
    {
        return new PostedVoiceProvider(value);
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

    public static bool operator ==(PostedVoiceProvider value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PostedVoiceProvider value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PostedVoiceProvider value) => value.Value;

    public static explicit operator PostedVoiceProvider(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string HumeAi = "HUME_AI";

        public const string CustomVoice = "CUSTOM_VOICE";
    }
}
