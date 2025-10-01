// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Batch;

[JsonConverter(typeof(Task.JsonConverter))]
[Serializable]
public record Task
{
    internal Task(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of Task with <see cref="Task.Classification"/>.
    /// </summary>
    public Task(Task.Classification value)
    {
        Type = "classification";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of Task with <see cref="Task.Regression"/>.
    /// </summary>
    public Task(Task.Regression value)
    {
        Type = "regression";
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
    /// Returns true if <see cref="Type"/> is "classification"
    /// </summary>
    public bool IsClassification => Type == "classification";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "regression"
    /// </summary>
    public bool IsRegression => Type == "regression";

    /// <summary>
    /// Returns the value as a <see cref="Hume.ExpressionMeasurement.Batch.TaskClassification"/> if <see cref="Type"/> is 'classification', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'classification'.</exception>
    public Hume.ExpressionMeasurement.Batch.TaskClassification AsClassification() =>
        IsClassification
            ? (Hume.ExpressionMeasurement.Batch.TaskClassification)Value!
            : throw new Exception("Task.Type is not 'classification'");

    /// <summary>
    /// Returns the value as a <see cref="Hume.ExpressionMeasurement.Batch.TaskRegression"/> if <see cref="Type"/> is 'regression', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'regression'.</exception>
    public Hume.ExpressionMeasurement.Batch.TaskRegression AsRegression() =>
        IsRegression
            ? (Hume.ExpressionMeasurement.Batch.TaskRegression)Value!
            : throw new Exception("Task.Type is not 'regression'");

    public T Match<T>(
        Func<Hume.ExpressionMeasurement.Batch.TaskClassification, T> onClassification,
        Func<Hume.ExpressionMeasurement.Batch.TaskRegression, T> onRegression,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "classification" => onClassification(AsClassification()),
            "regression" => onRegression(AsRegression()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<Hume.ExpressionMeasurement.Batch.TaskClassification> onClassification,
        Action<Hume.ExpressionMeasurement.Batch.TaskRegression> onRegression,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "classification":
                onClassification(AsClassification());
                break;
            case "regression":
                onRegression(AsRegression());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Hume.ExpressionMeasurement.Batch.TaskClassification"/> and returns true if successful.
    /// </summary>
    public bool TryAsClassification(out Hume.ExpressionMeasurement.Batch.TaskClassification? value)
    {
        if (Type == "classification")
        {
            value = (Hume.ExpressionMeasurement.Batch.TaskClassification)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Hume.ExpressionMeasurement.Batch.TaskRegression"/> and returns true if successful.
    /// </summary>
    public bool TryAsRegression(out Hume.ExpressionMeasurement.Batch.TaskRegression? value)
    {
        if (Type == "regression")
        {
            value = (Hume.ExpressionMeasurement.Batch.TaskRegression)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator Task(Task.Classification value) => new(value);

    public static implicit operator Task(Task.Regression value) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<Task>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(Task).IsAssignableFrom(typeToConvert);

        public override Task Read(
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
                "classification" =>
                    json.Deserialize<Hume.ExpressionMeasurement.Batch.TaskClassification>(options)
                        ?? throw new JsonException(
                            "Failed to deserialize Hume.ExpressionMeasurement.Batch.TaskClassification"
                        ),
                "regression" => json.Deserialize<Hume.ExpressionMeasurement.Batch.TaskRegression>(
                    options
                )
                    ?? throw new JsonException(
                        "Failed to deserialize Hume.ExpressionMeasurement.Batch.TaskRegression"
                    ),
                _ => json.Deserialize<object?>(options),
            };
            return new Task(discriminator, value);
        }

        public override void Write(Utf8JsonWriter writer, Task value, JsonSerializerOptions options)
        {
            JsonNode json =
                value.Type switch
                {
                    "classification" => JsonSerializer.SerializeToNode(value.Value, options),
                    "regression" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for classification
    /// </summary>
    [Serializable]
    public struct Classification
    {
        public Classification(Hume.ExpressionMeasurement.Batch.TaskClassification value)
        {
            Value = value;
        }

        internal Hume.ExpressionMeasurement.Batch.TaskClassification Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Task.Classification(
            Hume.ExpressionMeasurement.Batch.TaskClassification value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for regression
    /// </summary>
    [Serializable]
    public struct Regression
    {
        public Regression(Hume.ExpressionMeasurement.Batch.TaskRegression value)
        {
            Value = value;
        }

        internal Hume.ExpressionMeasurement.Batch.TaskRegression Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Task.Regression(
            Hume.ExpressionMeasurement.Batch.TaskRegression value
        ) => new(value);
    }
}
