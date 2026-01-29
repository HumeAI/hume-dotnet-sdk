// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Batch;

[JsonConverter(typeof(Source.JsonConverter))]
[Serializable]
public record Source
{
    internal Source(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of Source with <see cref="Source.Url"/>.
    /// </summary>
    public Source(Source.Url value)
    {
        Type = "url";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of Source with <see cref="Source.File"/>.
    /// </summary>
    public Source(Source.File value)
    {
        Type = "file";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of Source with <see cref="Source.Text"/>.
    /// </summary>
    public Source(Source.Text value)
    {
        Type = "text";
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
    /// Returns true if <see cref="Type"/> is "url"
    /// </summary>
    public bool IsUrl => Type == "url";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "file"
    /// </summary>
    public bool IsFile => Type == "file";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "text"
    /// </summary>
    public bool IsText => Type == "text";

    /// <summary>
    /// Returns the value as a <see cref="Hume.ExpressionMeasurement.Batch.SourceUrl"/> if <see cref="Type"/> is 'url', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'url'.</exception>
    public Hume.ExpressionMeasurement.Batch.SourceUrl AsUrl() =>
        IsUrl
            ? (Hume.ExpressionMeasurement.Batch.SourceUrl)Value!
            : throw new System.Exception("Source.Type is not 'url'");

    /// <summary>
    /// Returns the value as a <see cref="Hume.ExpressionMeasurement.Batch.SourceFile"/> if <see cref="Type"/> is 'file', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'file'.</exception>
    public Hume.ExpressionMeasurement.Batch.SourceFile AsFile() =>
        IsFile
            ? (Hume.ExpressionMeasurement.Batch.SourceFile)Value!
            : throw new System.Exception("Source.Type is not 'file'");

    /// <summary>
    /// Returns the value as a <see cref="Hume.ExpressionMeasurement.Batch.SourceTextSource"/> if <see cref="Type"/> is 'text', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'text'.</exception>
    public Hume.ExpressionMeasurement.Batch.SourceTextSource AsText() =>
        IsText
            ? (Hume.ExpressionMeasurement.Batch.SourceTextSource)Value!
            : throw new System.Exception("Source.Type is not 'text'");

    public T Match<T>(
        Func<Hume.ExpressionMeasurement.Batch.SourceUrl, T> onUrl,
        Func<Hume.ExpressionMeasurement.Batch.SourceFile, T> onFile,
        Func<Hume.ExpressionMeasurement.Batch.SourceTextSource, T> onText,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "url" => onUrl(AsUrl()),
            "file" => onFile(AsFile()),
            "text" => onText(AsText()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Hume.ExpressionMeasurement.Batch.SourceUrl> onUrl,
        Action<Hume.ExpressionMeasurement.Batch.SourceFile> onFile,
        Action<Hume.ExpressionMeasurement.Batch.SourceTextSource> onText,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "url":
                onUrl(AsUrl());
                break;
            case "file":
                onFile(AsFile());
                break;
            case "text":
                onText(AsText());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Hume.ExpressionMeasurement.Batch.SourceUrl"/> and returns true if successful.
    /// </summary>
    public bool TryAsUrl(out Hume.ExpressionMeasurement.Batch.SourceUrl? value)
    {
        if (Type == "url")
        {
            value = (Hume.ExpressionMeasurement.Batch.SourceUrl)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Hume.ExpressionMeasurement.Batch.SourceFile"/> and returns true if successful.
    /// </summary>
    public bool TryAsFile(out Hume.ExpressionMeasurement.Batch.SourceFile? value)
    {
        if (Type == "file")
        {
            value = (Hume.ExpressionMeasurement.Batch.SourceFile)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Hume.ExpressionMeasurement.Batch.SourceTextSource"/> and returns true if successful.
    /// </summary>
    public bool TryAsText(out Hume.ExpressionMeasurement.Batch.SourceTextSource? value)
    {
        if (Type == "text")
        {
            value = (Hume.ExpressionMeasurement.Batch.SourceTextSource)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator Source(Source.Url value) => new(value);

    public static implicit operator Source(Source.File value) => new(value);

    public static implicit operator Source(Source.Text value) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<Source>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(Source).IsAssignableFrom(typeToConvert);

        public override Source Read(
            ref Utf8JsonReader reader,
            System.Type typeToConvert,
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
                "url" => json.Deserialize<Hume.ExpressionMeasurement.Batch.SourceUrl?>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Hume.ExpressionMeasurement.Batch.SourceUrl"
                    ),
                "file" => json.Deserialize<Hume.ExpressionMeasurement.Batch.SourceFile?>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Hume.ExpressionMeasurement.Batch.SourceFile"
                    ),
                "text" => json.Deserialize<Hume.ExpressionMeasurement.Batch.SourceTextSource?>(
                    options
                )
                    ?? throw new JsonException(
                        "Failed to deserialize Hume.ExpressionMeasurement.Batch.SourceTextSource"
                    ),
                _ => json.Deserialize<object?>(options),
            };
            return new Source(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            Source value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "url" => JsonSerializer.SerializeToNode(value.Value, options),
                    "file" => JsonSerializer.SerializeToNode(value.Value, options),
                    "text" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for url
    /// </summary>
    [Serializable]
    public struct Url
    {
        public Url(Hume.ExpressionMeasurement.Batch.SourceUrl value)
        {
            Value = value;
        }

        internal Hume.ExpressionMeasurement.Batch.SourceUrl Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator Source.Url(
            Hume.ExpressionMeasurement.Batch.SourceUrl value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for file
    /// </summary>
    [Serializable]
    public struct File
    {
        public File(Hume.ExpressionMeasurement.Batch.SourceFile value)
        {
            Value = value;
        }

        internal Hume.ExpressionMeasurement.Batch.SourceFile Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator Source.File(
            Hume.ExpressionMeasurement.Batch.SourceFile value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for text
    /// </summary>
    [Serializable]
    public struct Text
    {
        public Text(Hume.ExpressionMeasurement.Batch.SourceTextSource value)
        {
            Value = value;
        }

        internal Hume.ExpressionMeasurement.Batch.SourceTextSource Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator Source.Text(
            Hume.ExpressionMeasurement.Batch.SourceTextSource value
        ) => new(value);
    }
}
