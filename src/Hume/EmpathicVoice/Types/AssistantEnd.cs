using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume;
using Hume.Core;

namespace Hume.EmpathicVoice;

/// <summary>
/// **Indicates the conclusion of the assistant's response**, signaling that the assistant has finished speaking for the current conversational turn.
/// </summary>
[Serializable]
public record AssistantEnd : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Used to manage conversational state, correlate frontend and backend data, and persist conversations across EVI sessions.
    /// </summary>
    [JsonPropertyName("custom_session_id")]
    public string? CustomSessionId { get; set; }

    /// <summary>
    /// The type of message sent through the socket; for an Assistant End message, this must be `assistant_end`.
    ///
    /// This message indicates the conclusion of the assistant's response, signaling that the assistant has finished speaking for the current conversational turn.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("type")]
    public AssistantEnd.TypeLiteral Type { get;
#if NET5_0_OR_GREATER
        init;
#else
        set;
#endif
    } = new();

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }

    [JsonConverter(typeof(TypeLiteralConverter))]
    public readonly struct TypeLiteral
    {
        public const string Value = "assistant_end";

        public static implicit operator string(TypeLiteral _) => Value;

        public override string ToString() => Value;

        public override int GetHashCode() =>
            global::System.StringComparer.Ordinal.GetHashCode(Value);

        public override bool Equals(object? obj) => obj is TypeLiteral;

        public static bool operator ==(TypeLiteral _, TypeLiteral __) => true;

        public static bool operator !=(TypeLiteral _, TypeLiteral __) => false;

        internal sealed class TypeLiteralConverter : JsonConverter<TypeLiteral>
        {
            public override TypeLiteral Read(
                ref Utf8JsonReader reader,
                global::System.Type typeToConvert,
                JsonSerializerOptions options
            )
            {
                var value = reader.GetString();
                if (value != TypeLiteral.Value)
                {
                    throw new JsonException(
                        "Expected \""
                            + TypeLiteral.Value
                            + "\" for type discriminator but got \""
                            + value
                            + "\"."
                    );
                }
                return new TypeLiteral();
            }

            public override void Write(
                Utf8JsonWriter writer,
                TypeLiteral value,
                JsonSerializerOptions options
            ) => writer.WriteStringValue(TypeLiteral.Value);

            public override TypeLiteral ReadAsPropertyName(
                ref Utf8JsonReader reader,
                global::System.Type typeToConvert,
                JsonSerializerOptions options
            )
            {
                var value = reader.GetString();
                if (value != TypeLiteral.Value)
                {
                    throw new JsonException(
                        "Expected \""
                            + TypeLiteral.Value
                            + "\" for type discriminator but got \""
                            + value
                            + "\"."
                    );
                }
                return new TypeLiteral();
            }

            public override void WriteAsPropertyName(
                Utf8JsonWriter writer,
                TypeLiteral value,
                JsonSerializerOptions options
            ) => writer.WritePropertyName(TypeLiteral.Value);
        }
    }
}
