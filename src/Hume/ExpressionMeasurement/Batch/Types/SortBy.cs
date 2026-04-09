using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Batch;

[JsonConverter(typeof(SortBy.SortBySerializer))]
[Serializable]
public readonly record struct SortBy : IStringEnum
{
    public static readonly SortBy Created = new(Values.Created);

    public static readonly SortBy Started = new(Values.Started);

    public static readonly SortBy Ended = new(Values.Ended);

    public SortBy(string value)
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
    public static SortBy FromCustom(string value)
    {
        return new SortBy(value);
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

    public static bool operator ==(SortBy value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(SortBy value1, string value2) => !value1.Value.Equals(value2);

    public static explicit operator string(SortBy value) => value.Value;

    public static explicit operator SortBy(string value) => new(value);

    internal class SortBySerializer : JsonConverter<SortBy>
    {
        public override SortBy Read(
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
            return new SortBy(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            SortBy value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override SortBy ReadAsPropertyName(
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
            return new SortBy(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            SortBy value,
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
        public const string Created = "created";

        public const string Started = "started";

        public const string Ended = "ended";
    }
}
