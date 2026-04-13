using global::System.Text.Json;
using global::System.Text.Json.Serialization;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(ErrorLevelConverter))]
public readonly struct ErrorLevel
{
    public const string Value = "warn";

    public static implicit operator string(ErrorLevel _) => Value;

    public override string ToString() => Value;

    public override int GetHashCode() => global::System.StringComparer.Ordinal.GetHashCode(Value);

    public override bool Equals(object? obj) => obj is ErrorLevel;

    public static bool operator ==(ErrorLevel _, ErrorLevel __) => true;

    public static bool operator !=(ErrorLevel _, ErrorLevel __) => false;

    internal sealed class ErrorLevelConverter : JsonConverter<ErrorLevel>
    {
        public override ErrorLevel Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var value = reader.GetString();
            if (value != ErrorLevel.Value)
            {
                throw new JsonException(
                    "Expected \""
                        + ErrorLevel.Value
                        + "\" for type discriminator but got \""
                        + value
                        + "\"."
                );
            }
            return new ErrorLevel();
        }

        public override void Write(
            Utf8JsonWriter writer,
            ErrorLevel value,
            JsonSerializerOptions options
        ) => writer.WriteStringValue(ErrorLevel.Value);

        public override ErrorLevel ReadAsPropertyName(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var value = reader.GetString();
            if (value != ErrorLevel.Value)
            {
                throw new JsonException(
                    "Expected \""
                        + ErrorLevel.Value
                        + "\" for type discriminator but got \""
                        + value
                        + "\"."
                );
            }
            return new ErrorLevel();
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ErrorLevel value,
            JsonSerializerOptions options
        ) => writer.WritePropertyName(ErrorLevel.Value);
    }
}
