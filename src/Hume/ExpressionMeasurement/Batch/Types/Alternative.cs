using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Hume.ExpressionMeasurement.Batch;

[JsonConverter(typeof(AlternativeConverter))]
public readonly struct Alternative
{
    public const string Value = "language_only";

    public static implicit operator string(Alternative _) => Value;

    public override string ToString() => Value;

    public override int GetHashCode() => global::System.StringComparer.Ordinal.GetHashCode(Value);

    public override bool Equals(object? obj) => obj is Alternative;

    public static bool operator ==(Alternative _, Alternative __) => true;

    public static bool operator !=(Alternative _, Alternative __) => false;

    internal sealed class AlternativeConverter : JsonConverter<Alternative>
    {
        public override Alternative Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var value = reader.GetString();
            if (value != Alternative.Value)
            {
                throw new JsonException(
                    "Expected \""
                        + Alternative.Value
                        + "\" for type discriminator but got \""
                        + value
                        + "\"."
                );
            }
            return new Alternative();
        }

        public override void Write(
            Utf8JsonWriter writer,
            Alternative value,
            JsonSerializerOptions options
        ) => writer.WriteStringValue(Alternative.Value);

        public override Alternative ReadAsPropertyName(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var value = reader.GetString();
            if (value != Alternative.Value)
            {
                throw new JsonException(
                    "Expected \""
                        + Alternative.Value
                        + "\" for type discriminator but got \""
                        + value
                        + "\"."
                );
            }
            return new Alternative();
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            Alternative value,
            JsonSerializerOptions options
        ) => writer.WritePropertyName(Alternative.Value);
    }
}
