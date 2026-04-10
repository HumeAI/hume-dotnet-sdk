using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Batch;

[JsonConverter(typeof(Granularity.GranularitySerializer))]
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

    internal class GranularitySerializer : JsonConverter<Granularity>
    {
        public override Granularity Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON value could not be read as a string."
                );
            return new Granularity(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            Granularity value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override Granularity ReadAsPropertyName(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON property name could not be read as a string."
                );
            return new Granularity(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            Granularity value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Value);
        }
    }

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
