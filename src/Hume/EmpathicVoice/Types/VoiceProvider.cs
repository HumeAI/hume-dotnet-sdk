using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(StringEnumSerializer<VoiceProvider>))]
[Serializable]
public readonly record struct VoiceProvider : IStringEnum
{
    public static readonly VoiceProvider HumeAi = new(Values.HumeAi);

    public static readonly VoiceProvider CustomVoice = new(Values.CustomVoice);

    public VoiceProvider(string value)
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
    public static VoiceProvider FromCustom(string value)
    {
        return new VoiceProvider(value);
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

    public static bool operator ==(VoiceProvider value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(VoiceProvider value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(VoiceProvider value) => value.Value;

    public static explicit operator VoiceProvider(string value) => new(value);

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
