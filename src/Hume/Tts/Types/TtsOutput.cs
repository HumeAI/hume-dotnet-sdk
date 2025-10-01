// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.Tts;

[JsonConverter(typeof(TtsOutput.JsonConverter))]
[Serializable]
public record TtsOutput
{
    internal TtsOutput(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of TtsOutput with <see cref="TtsOutput.Timestamp"/>.
    /// </summary>
    public TtsOutput(TtsOutput.Timestamp value)
    {
        Type = "timestamp";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of TtsOutput with <see cref="TtsOutput.Audio"/>.
    /// </summary>
    public TtsOutput(TtsOutput.Audio value)
    {
        Type = "audio";
        Value = value.Value;
    }

    /// <summary>
    /// Discriminant value
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; internal set; }

    /// <summary>
    /// Discriminated union value
    /// </summary>
    public object? Value { get; internal set; }

    /// <summary>
    /// Returns true if <see cref="Type"/> is "timestamp"
    /// </summary>
    public bool IsTimestamp => Type == "timestamp";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "audio"
    /// </summary>
    public bool IsAudio => Type == "audio";

    /// <summary>
    /// Returns the value as a <see cref="Hume.Tts.TimestampMessage"/> if <see cref="Type"/> is 'timestamp', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'timestamp'.</exception>
    public Hume.Tts.TimestampMessage AsTimestamp() =>
        IsTimestamp
            ? (Hume.Tts.TimestampMessage)Value!
            : throw new Exception("TtsOutput.Type is not 'timestamp'");

    /// <summary>
    /// Returns the value as a <see cref="Hume.Tts.SnippetAudioChunk"/> if <see cref="Type"/> is 'audio', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'audio'.</exception>
    public Hume.Tts.SnippetAudioChunk AsAudio() =>
        IsAudio
            ? (Hume.Tts.SnippetAudioChunk)Value!
            : throw new Exception("TtsOutput.Type is not 'audio'");

    public T Match<T>(
        Func<Hume.Tts.TimestampMessage, T> onTimestamp,
        Func<Hume.Tts.SnippetAudioChunk, T> onAudio,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "timestamp" => onTimestamp(AsTimestamp()),
            "audio" => onAudio(AsAudio()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Hume.Tts.TimestampMessage> onTimestamp,
        Action<Hume.Tts.SnippetAudioChunk> onAudio,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "timestamp":
                onTimestamp(AsTimestamp());
                break;
            case "audio":
                onAudio(AsAudio());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Hume.Tts.TimestampMessage"/> and returns true if successful.
    /// </summary>
    public bool TryAsTimestamp(out Hume.Tts.TimestampMessage? value)
    {
        if (Type == "timestamp")
        {
            value = (Hume.Tts.TimestampMessage)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Hume.Tts.SnippetAudioChunk"/> and returns true if successful.
    /// </summary>
    public bool TryAsAudio(out Hume.Tts.SnippetAudioChunk? value)
    {
        if (Type == "audio")
        {
            value = (Hume.Tts.SnippetAudioChunk)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator TtsOutput(TtsOutput.Timestamp value) => new(value);

    public static implicit operator TtsOutput(TtsOutput.Audio value) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<TtsOutput>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(TtsOutput).IsAssignableFrom(typeToConvert);

        public override TtsOutput Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var json = JsonElement.ParseValue(ref reader);
            if (!json.TryGetProperty("type", out var discriminatorElement))
            {
                throw new JsonException("Missing discriminator property 'type'");
            }
            if (discriminatorElement.ValueKind != JsonValueKind.String)
            {
                if (discriminatorElement.ValueKind == JsonValueKind.Null)
                {
                    throw new JsonException("Discriminator property 'type' is null");
                }

                throw new JsonException(
                    $"Discriminator property 'type' is not a string, instead is {discriminatorElement.ToString()}"
                );
            }

            var discriminator =
                discriminatorElement.GetString()
                ?? throw new JsonException("Discriminator property 'type' is null");

            var value = discriminator switch
            {
                "timestamp" => json.Deserialize<Hume.Tts.TimestampMessage>(options)
                    ?? throw new JsonException("Failed to deserialize Hume.Tts.TimestampMessage"),
                "audio" => json.Deserialize<Hume.Tts.SnippetAudioChunk>(options)
                    ?? throw new JsonException("Failed to deserialize Hume.Tts.SnippetAudioChunk"),
                _ => json.Deserialize<object?>(options),
            };
            return new TtsOutput(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            TtsOutput value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "timestamp" => JsonSerializer.SerializeToNode(value.Value, options),
                    "audio" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for timestamp
    /// </summary>
    [Serializable]
    public struct Timestamp
    {
        public Timestamp(Hume.Tts.TimestampMessage value)
        {
            Value = value;
        }

        internal Hume.Tts.TimestampMessage Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Timestamp(Hume.Tts.TimestampMessage value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for audio
    /// </summary>
    [Serializable]
    public struct Audio
    {
        public Audio(Hume.Tts.SnippetAudioChunk value)
        {
            Value = value;
        }

        internal Hume.Tts.SnippetAudioChunk Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Audio(Hume.Tts.SnippetAudioChunk value) => new(value);
    }
}
