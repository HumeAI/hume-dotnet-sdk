// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using HumeApi.Core;

namespace HumeApi.Tts;

/// <summary>
/// Specifies the output audio file format.
/// </summary>
[JsonConverter(typeof(Format.JsonConverter))]
[Serializable]
public record Format
{
    internal Format(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of Format with <see cref="Format.Mp3"/>.
    /// </summary>
    public Format(Format.Mp3 value)
    {
        Type = "mp3";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of Format with <see cref="Format.Pcm"/>.
    /// </summary>
    public Format(Format.Pcm value)
    {
        Type = "pcm";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of Format with <see cref="Format.Wav"/>.
    /// </summary>
    public Format(Format.Wav value)
    {
        Type = "wav";
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
    /// Returns true if <see cref="Type"/> is "mp3"
    /// </summary>
    public bool IsMp3 => Type == "mp3";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "pcm"
    /// </summary>
    public bool IsPcm => Type == "pcm";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "wav"
    /// </summary>
    public bool IsWav => Type == "wav";

    /// <summary>
    /// Returns the value as a <see cref="HumeApi.Tts.FormatMp3"/> if <see cref="Type"/> is 'mp3', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'mp3'.</exception>
    public HumeApi.Tts.FormatMp3 AsMp3() =>
        IsMp3 ? (HumeApi.Tts.FormatMp3)Value! : throw new Exception("Format.Type is not 'mp3'");

    /// <summary>
    /// Returns the value as a <see cref="HumeApi.Tts.FormatPcm"/> if <see cref="Type"/> is 'pcm', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'pcm'.</exception>
    public HumeApi.Tts.FormatPcm AsPcm() =>
        IsPcm ? (HumeApi.Tts.FormatPcm)Value! : throw new Exception("Format.Type is not 'pcm'");

    /// <summary>
    /// Returns the value as a <see cref="HumeApi.Tts.FormatWav"/> if <see cref="Type"/> is 'wav', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'wav'.</exception>
    public HumeApi.Tts.FormatWav AsWav() =>
        IsWav ? (HumeApi.Tts.FormatWav)Value! : throw new Exception("Format.Type is not 'wav'");

    public T Match<T>(
        Func<HumeApi.Tts.FormatMp3, T> onMp3,
        Func<HumeApi.Tts.FormatPcm, T> onPcm,
        Func<HumeApi.Tts.FormatWav, T> onWav,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "mp3" => onMp3(AsMp3()),
            "pcm" => onPcm(AsPcm()),
            "wav" => onWav(AsWav()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<HumeApi.Tts.FormatMp3> onMp3,
        Action<HumeApi.Tts.FormatPcm> onPcm,
        Action<HumeApi.Tts.FormatWav> onWav,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "mp3":
                onMp3(AsMp3());
                break;
            case "pcm":
                onPcm(AsPcm());
                break;
            case "wav":
                onWav(AsWav());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="HumeApi.Tts.FormatMp3"/> and returns true if successful.
    /// </summary>
    public bool TryAsMp3(out HumeApi.Tts.FormatMp3? value)
    {
        if (Type == "mp3")
        {
            value = (HumeApi.Tts.FormatMp3)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="HumeApi.Tts.FormatPcm"/> and returns true if successful.
    /// </summary>
    public bool TryAsPcm(out HumeApi.Tts.FormatPcm? value)
    {
        if (Type == "pcm")
        {
            value = (HumeApi.Tts.FormatPcm)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="HumeApi.Tts.FormatWav"/> and returns true if successful.
    /// </summary>
    public bool TryAsWav(out HumeApi.Tts.FormatWav? value)
    {
        if (Type == "wav")
        {
            value = (HumeApi.Tts.FormatWav)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator Format(Format.Mp3 value) => new(value);

    public static implicit operator Format(Format.Pcm value) => new(value);

    public static implicit operator Format(Format.Wav value) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<Format>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(Format).IsAssignableFrom(typeToConvert);

        public override Format Read(
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
                "mp3" => json.Deserialize<HumeApi.Tts.FormatMp3>(options)
                    ?? throw new JsonException("Failed to deserialize HumeApi.Tts.FormatMp3"),
                "pcm" => json.Deserialize<HumeApi.Tts.FormatPcm>(options)
                    ?? throw new JsonException("Failed to deserialize HumeApi.Tts.FormatPcm"),
                "wav" => json.Deserialize<HumeApi.Tts.FormatWav>(options)
                    ?? throw new JsonException("Failed to deserialize HumeApi.Tts.FormatWav"),
                _ => json.Deserialize<object?>(options),
            };
            return new Format(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            Format value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "mp3" => JsonSerializer.SerializeToNode(value.Value, options),
                    "pcm" => JsonSerializer.SerializeToNode(value.Value, options),
                    "wav" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for mp3
    /// </summary>
    [Serializable]
    public struct Mp3
    {
        public Mp3(HumeApi.Tts.FormatMp3 value)
        {
            Value = value;
        }

        internal HumeApi.Tts.FormatMp3 Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Mp3(HumeApi.Tts.FormatMp3 value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for pcm
    /// </summary>
    [Serializable]
    public struct Pcm
    {
        public Pcm(HumeApi.Tts.FormatPcm value)
        {
            Value = value;
        }

        internal HumeApi.Tts.FormatPcm Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Pcm(HumeApi.Tts.FormatPcm value) => new(value);
    }

    /// <summary>
    /// Discriminated union type for wav
    /// </summary>
    [Serializable]
    public struct Wav
    {
        public Wav(HumeApi.Tts.FormatWav value)
        {
            Value = value;
        }

        internal HumeApi.Tts.FormatWav Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Wav(HumeApi.Tts.FormatWav value) => new(value);
    }
}
