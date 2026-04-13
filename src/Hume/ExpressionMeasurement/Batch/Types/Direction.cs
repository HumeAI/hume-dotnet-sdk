using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Batch;

[JsonConverter(typeof(Direction.DirectionSerializer))]
[Serializable]
public readonly record struct Direction : IStringEnum
{
    public static readonly Direction Asc = new(Values.Asc);

    public static readonly Direction Desc = new(Values.Desc);

    public Direction(string value)
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
    public static Direction FromCustom(string value)
    {
        return new Direction(value);
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

    public static bool operator ==(Direction value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(Direction value1, string value2) => !value1.Value.Equals(value2);

    public static explicit operator string(Direction value) => value.Value;

    public static explicit operator Direction(string value) => new(value);

    internal class DirectionSerializer : JsonConverter<Direction>
    {
        public override Direction Read(
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
            return new Direction(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            Direction value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override Direction ReadAsPropertyName(
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
            return new Direction(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            Direction value,
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
        public const string Asc = "asc";

        public const string Desc = "desc";
    }
}
