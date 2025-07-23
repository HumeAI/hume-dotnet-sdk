using System.Text.Json.Serialization;
using HumeApi.Core;

namespace HumeApi.EmpathicVoice;

[JsonConverter(typeof(StringEnumSerializer<ReturnCustomVoiceBaseVoice>))]
[Serializable]
public readonly record struct ReturnCustomVoiceBaseVoice : IStringEnum
{
    public static readonly ReturnCustomVoiceBaseVoice Ito = new(Values.Ito);

    public static readonly ReturnCustomVoiceBaseVoice Kora = new(Values.Kora);

    public static readonly ReturnCustomVoiceBaseVoice Dacher = new(Values.Dacher);

    public static readonly ReturnCustomVoiceBaseVoice Aura = new(Values.Aura);

    public static readonly ReturnCustomVoiceBaseVoice Finn = new(Values.Finn);

    public static readonly ReturnCustomVoiceBaseVoice Whimsy = new(Values.Whimsy);

    public static readonly ReturnCustomVoiceBaseVoice Stella = new(Values.Stella);

    public static readonly ReturnCustomVoiceBaseVoice Sunny = new(Values.Sunny);

    public ReturnCustomVoiceBaseVoice(string value)
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
    public static ReturnCustomVoiceBaseVoice FromCustom(string value)
    {
        return new ReturnCustomVoiceBaseVoice(value);
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

    public static bool operator ==(ReturnCustomVoiceBaseVoice value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ReturnCustomVoiceBaseVoice value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ReturnCustomVoiceBaseVoice value) => value.Value;

    public static explicit operator ReturnCustomVoiceBaseVoice(string value) => new(value);

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
