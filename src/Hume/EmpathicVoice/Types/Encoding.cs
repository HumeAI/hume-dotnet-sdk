using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(EncodingConverter))]
public readonly struct Encoding
{
    public const string Value = "linear16";

    public static implicit operator string(Encoding _) => Value;

    public override string ToString() => Value;

    public override int GetHashCode() => global::System.StringComparer.Ordinal.GetHashCode(Value);

    public override bool Equals(object? obj) => obj is Encoding;

    public static bool operator ==(Encoding _, Encoding __) => true;

    public static bool operator !=(Encoding _, Encoding __) => false;

    internal sealed class EncodingConverter : JsonConverter<Encoding>
    {
        public override Encoding Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var value = reader.GetString();
            if (value != Encoding.Value)
            {
                throw new JsonException(
                    "Expected \""
                        + Encoding.Value
                        + "\" for type discriminator but got \""
                        + value
                        + "\"."
                );
            }
            return new Encoding();
        }

        public override void Write(
            Utf8JsonWriter writer,
            Encoding value,
            JsonSerializerOptions options
        ) => writer.WriteStringValue(Encoding.Value);

        public override Encoding ReadAsPropertyName(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var value = reader.GetString();
            if (value != Encoding.Value)
            {
                throw new JsonException(
                    "Expected \""
                        + Encoding.Value
                        + "\" for type discriminator but got \""
                        + value
                        + "\"."
                );
            }
            return new Encoding();
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            Encoding value,
            JsonSerializerOptions options
        ) => writer.WritePropertyName(Encoding.Value);
    }
}
