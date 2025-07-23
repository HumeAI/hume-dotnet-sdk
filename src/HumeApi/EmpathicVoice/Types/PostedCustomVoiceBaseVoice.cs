using System.Text.Json.Serialization;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

[JsonConverter(typeof(StringEnumSerializer<PostedCustomVoiceBaseVoice>))]
[Serializable]
public readonly record struct PostedCustomVoiceBaseVoice : IStringEnum
{
    public static readonly PostedCustomVoiceBaseVoice Ito = new(Values.Ito);

    public static readonly PostedCustomVoiceBaseVoice Kora = new(Values.Kora);

    public static readonly PostedCustomVoiceBaseVoice Dacher = new(Values.Dacher);

    public static readonly PostedCustomVoiceBaseVoice Aura = new(Values.Aura);

    public static readonly PostedCustomVoiceBaseVoice Finn = new(Values.Finn);

    public static readonly PostedCustomVoiceBaseVoice Whimsy = new(Values.Whimsy);

    public static readonly PostedCustomVoiceBaseVoice Stella = new(Values.Stella);

    public static readonly PostedCustomVoiceBaseVoice Sunny = new(Values.Sunny);

    public PostedCustomVoiceBaseVoice(string value)
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
    public static PostedCustomVoiceBaseVoice FromCustom(string value)
    {
        return new PostedCustomVoiceBaseVoice(value);
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

    public static bool operator ==(PostedCustomVoiceBaseVoice value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PostedCustomVoiceBaseVoice value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PostedCustomVoiceBaseVoice value) => value.Value;

    public static explicit operator PostedCustomVoiceBaseVoice(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Ito = "ITO";

        public const string Kora = "KORA";

        public const string Dacher = "DACHER";

        public const string Aura = "AURA";

        public const string Finn = "FINN";

        public const string Whimsy = "WHIMSY";

        public const string Stella = "STELLA";

        public const string Sunny = "SUNNY";
    }
}
