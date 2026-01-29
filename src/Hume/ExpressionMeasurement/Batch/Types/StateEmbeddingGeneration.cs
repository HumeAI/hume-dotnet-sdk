// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Batch;

[JsonConverter(typeof(StateEmbeddingGeneration.JsonConverter))]
[Serializable]
public record StateEmbeddingGeneration
{
    internal StateEmbeddingGeneration(string type, object? value)
    {
        Status = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of StateEmbeddingGeneration with <see cref="StateEmbeddingGeneration.Queued"/>.
    /// </summary>
    public StateEmbeddingGeneration(StateEmbeddingGeneration.Queued value)
    {
        Status = "QUEUED";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of StateEmbeddingGeneration with <see cref="StateEmbeddingGeneration.InProgress"/>.
    /// </summary>
    public StateEmbeddingGeneration(StateEmbeddingGeneration.InProgress value)
    {
        Status = "IN_PROGRESS";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of StateEmbeddingGeneration with <see cref="StateEmbeddingGeneration.Completed"/>.
    /// </summary>
    public StateEmbeddingGeneration(StateEmbeddingGeneration.Completed value)
    {
        Status = "COMPLETED";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of StateEmbeddingGeneration with <see cref="StateEmbeddingGeneration.Failed"/>.
    /// </summary>
    public StateEmbeddingGeneration(StateEmbeddingGeneration.Failed value)
    {
        Status = "FAILED";
        Value = value.Value;
    }

    /// <summary>
    /// Discriminant value
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; internal set; }

    /// <summary>
    /// Discriminated union value
    /// </summary>
    public object? Value { get; internal set; }

    /// <summary>
    /// Returns true if <see cref="Status"/> is "QUEUED"
    /// </summary>
    public bool IsQueued => Status == "QUEUED";

    /// <summary>
    /// Returns true if <see cref="Status"/> is "IN_PROGRESS"
    /// </summary>
    public bool IsInProgress => Status == "IN_PROGRESS";

    /// <summary>
    /// Returns true if <see cref="Status"/> is "COMPLETED"
    /// </summary>
    public bool IsCompleted => Status == "COMPLETED";

    /// <summary>
    /// Returns true if <see cref="Status"/> is "FAILED"
    /// </summary>
    public bool IsFailed => Status == "FAILED";

    /// <summary>
    /// Returns the value as a <see cref="Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued"/> if <see cref="Status"/> is 'QUEUED', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'QUEUED'.</exception>
    public Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued AsQueued() =>
        IsQueued
            ? (Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued)Value!
            : throw new System.Exception("StateEmbeddingGeneration.Status is not 'QUEUED'");

    /// <summary>
    /// Returns the value as a <see cref="Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress"/> if <see cref="Status"/> is 'IN_PROGRESS', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'IN_PROGRESS'.</exception>
    public Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress AsInProgress() =>
        IsInProgress
            ? (Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress)Value!
            : throw new System.Exception("StateEmbeddingGeneration.Status is not 'IN_PROGRESS'");

    /// <summary>
    /// Returns the value as a <see cref="Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration"/> if <see cref="Status"/> is 'COMPLETED', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'COMPLETED'.</exception>
    public Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration AsCompleted() =>
        IsCompleted
            ? (Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration)
                Value!
            : throw new System.Exception("StateEmbeddingGeneration.Status is not 'COMPLETED'");

    /// <summary>
    /// Returns the value as a <see cref="Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed"/> if <see cref="Status"/> is 'FAILED', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'FAILED'.</exception>
    public Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed AsFailed() =>
        IsFailed
            ? (Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed)Value!
            : throw new System.Exception("StateEmbeddingGeneration.Status is not 'FAILED'");

    public T Match<T>(
        Func<Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued, T> onQueued,
        Func<Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress, T> onInProgress,
        Func<
            Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration,
            T
        > onCompleted,
        Func<Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed, T> onFailed,
        Func<string, object?, T> onUnknown_
    )
    {
        return Status switch
        {
            "QUEUED" => onQueued(AsQueued()),
            "IN_PROGRESS" => onInProgress(AsInProgress()),
            "COMPLETED" => onCompleted(AsCompleted()),
            "FAILED" => onFailed(AsFailed()),
            _ => onUnknown_(Status, Value),
        };
    }

    public void Visit(
        Action<Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued> onQueued,
        Action<Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress> onInProgress,
        Action<Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration> onCompleted,
        Action<Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed> onFailed,
        Action<string, object?> onUnknown_
    )
    {
        switch (Status)
        {
            case "QUEUED":
                onQueued(AsQueued());
                break;
            case "IN_PROGRESS":
                onInProgress(AsInProgress());
                break;
            case "COMPLETED":
                onCompleted(AsCompleted());
                break;
            case "FAILED":
                onFailed(AsFailed());
                break;
            default:
                onUnknown_(Status, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued"/> and returns true if successful.
    /// </summary>
    public bool TryAsQueued(
        out Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued? value
    )
    {
        if (Status == "QUEUED")
        {
            value = (Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress"/> and returns true if successful.
    /// </summary>
    public bool TryAsInProgress(
        out Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress? value
    )
    {
        if (Status == "IN_PROGRESS")
        {
            value = (Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration"/> and returns true if successful.
    /// </summary>
    public bool TryAsCompleted(
        out Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration? value
    )
    {
        if (Status == "COMPLETED")
        {
            value =
                (Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration)
                    Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed"/> and returns true if successful.
    /// </summary>
    public bool TryAsFailed(
        out Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed? value
    )
    {
        if (Status == "FAILED")
        {
            value = (Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator StateEmbeddingGeneration(
        StateEmbeddingGeneration.Queued value
    ) => new(value);

    public static implicit operator StateEmbeddingGeneration(
        StateEmbeddingGeneration.InProgress value
    ) => new(value);

    public static implicit operator StateEmbeddingGeneration(
        StateEmbeddingGeneration.Completed value
    ) => new(value);

    public static implicit operator StateEmbeddingGeneration(
        StateEmbeddingGeneration.Failed value
    ) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<StateEmbeddingGeneration>
    {
        public override bool CanConvert(System.Type typeToConvert) =>
            typeof(StateEmbeddingGeneration).IsAssignableFrom(typeToConvert);

        public override StateEmbeddingGeneration Read(
            ref Utf8JsonReader reader,
            System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var json = JsonElement.ParseValue(ref reader);
            if (!json.TryGetProperty("status", out var discriminatorElement))
            {
                throw new JsonException("Missing discriminator property 'status'");
            }
            if (discriminatorElement.ValueKind != JsonValueKind.String)
            {
                if (discriminatorElement.ValueKind == JsonValueKind.Null)
                {
                    throw new JsonException("Discriminator property 'status' is null");
                }

                throw new JsonException(
                    $"Discriminator property 'status' is not a string, instead is {discriminatorElement.ToString()}"
                );
            }

            var discriminator =
                discriminatorElement.GetString()
                ?? throw new JsonException("Discriminator property 'status' is null");

            var value = discriminator switch
            {
                "QUEUED" =>
                    json.Deserialize<Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued"
                        ),
                "IN_PROGRESS" =>
                    json.Deserialize<Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress"
                        ),
                "COMPLETED" =>
                    json.Deserialize<Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration"
                        ),
                "FAILED" =>
                    json.Deserialize<Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed"
                        ),
                _ => json.Deserialize<object?>(options),
            };
            return new StateEmbeddingGeneration(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            StateEmbeddingGeneration value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Status switch
                {
                    "QUEUED" => JsonSerializer.SerializeToNode(value.Value, options),
                    "IN_PROGRESS" => JsonSerializer.SerializeToNode(value.Value, options),
                    "COMPLETED" => JsonSerializer.SerializeToNode(value.Value, options),
                    "FAILED" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["status"] = value.Status;
            json.WriteTo(writer, options);
        }
    }

    /// <summary>
    /// Discriminated union type for QUEUED
    /// </summary>
    [Serializable]
    public struct Queued
    {
        public Queued(Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued value)
        {
            Value = value;
        }

        internal Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator StateEmbeddingGeneration.Queued(
            Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for IN_PROGRESS
    /// </summary>
    [Serializable]
    public struct InProgress
    {
        public InProgress(Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress value)
        {
            Value = value;
        }

        internal Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator StateEmbeddingGeneration.InProgress(
            Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for COMPLETED
    /// </summary>
    [Serializable]
    public struct Completed
    {
        public Completed(
            Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration value
        )
        {
            Value = value;
        }

        internal Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator StateEmbeddingGeneration.Completed(
            Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for FAILED
    /// </summary>
    [Serializable]
    public struct Failed
    {
        public Failed(Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed value)
        {
            Value = value;
        }

        internal Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator StateEmbeddingGeneration.Failed(
            Hume.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed value
        ) => new(value);
    }
}
