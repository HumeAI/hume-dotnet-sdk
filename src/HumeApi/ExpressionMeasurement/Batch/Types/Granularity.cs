using System.Text.Json.Serialization;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Batch;

[JsonConverter(typeof(StringEnumSerializer<Granularity>))]
[Serializable]
public readonly record struct Granularity : IStringEnum
{
    public static readonly Granularity Word = new(Values.Word);

    public static readonly Granularity Sentence = new(Values.Sentence);

    public static readonly Granularity Utterance = new(Values.Utterance);

    public static readonly Granularity ConversationalTurn = new(Values.ConversationalTurn);

    public Granularity(string value)
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
    public static Granularity FromCustom(string value)
    {
        return new Granularity(value);
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

    public static bool operator ==(Granularity value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(Granularity value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(Granularity value) => value.Value;

    public static explicit operator Granularity(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Word = "word";

        public const string Sentence = "sentence";

        public const string Utterance = "utterance";

        public const string ConversationalTurn = "conversational_turn";
    }
}
