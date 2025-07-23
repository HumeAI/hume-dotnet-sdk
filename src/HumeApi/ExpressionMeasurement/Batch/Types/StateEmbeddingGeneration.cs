// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Batch;

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
    /// Returns the value as a <see cref="HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued"/> if <see cref="Status"/> is 'QUEUED', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'QUEUED'.</exception>
    public HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued AsQueued() =>
        IsQueued
            ? (HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued)Value!
            : throw new Exception("StateEmbeddingGeneration.Status is not 'QUEUED'");

    /// <summary>
    /// Returns the value as a <see cref="HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress"/> if <see cref="Status"/> is 'IN_PROGRESS', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'IN_PROGRESS'.</exception>
    public HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress AsInProgress() =>
        IsInProgress
            ? (HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress)Value!
            : throw new Exception("StateEmbeddingGeneration.Status is not 'IN_PROGRESS'");

    /// <summary>
    /// Returns the value as a <see cref="HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration"/> if <see cref="Status"/> is 'COMPLETED', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'COMPLETED'.</exception>
    public HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration AsCompleted() =>
        IsCompleted
            ? (HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration)
                Value!
            : throw new Exception("StateEmbeddingGeneration.Status is not 'COMPLETED'");

    /// <summary>
    /// Returns the value as a <see cref="HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed"/> if <see cref="Status"/> is 'FAILED', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'FAILED'.</exception>
    public HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed AsFailed() =>
        IsFailed
            ? (HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed)Value!
            : throw new Exception("StateEmbeddingGeneration.Status is not 'FAILED'");

    public T Match<T>(
        Func<HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued, T> onQueued,
        Func<
            HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress,
            T
        > onInProgress,
        Func<
            HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration,
            T
        > onCompleted,
        Func<HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed, T> onFailed,
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
        Action<HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued> onQueued,
        Action<HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress> onInProgress,
        Action<HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration> onCompleted,
        Action<HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed> onFailed,
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
    /// Attempts to cast the value to a <see cref="HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued"/> and returns true if successful.
    /// </summary>
    public bool TryAsQueued(
        out HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued? value
    )
    {
        if (Status == "QUEUED")
        {
            value = (HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress"/> and returns true if successful.
    /// </summary>
    public bool TryAsInProgress(
        out HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress? value
    )
    {
        if (Status == "IN_PROGRESS")
        {
            value = (HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration"/> and returns true if successful.
    /// </summary>
    public bool TryAsCompleted(
        out HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration? value
    )
    {
        if (Status == "COMPLETED")
        {
            value =
                (HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration)
                    Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed"/> and returns true if successful.
    /// </summary>
    public bool TryAsFailed(
        out HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed? value
    )
    {
        if (Status == "FAILED")
        {
            value = (HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed)Value!;
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
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(StateEmbeddingGeneration).IsAssignableFrom(typeToConvert);

        public override StateEmbeddingGeneration Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
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
                    json.Deserialize<HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued"
                        ),
                "IN_PROGRESS" =>
                    json.Deserialize<HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress"
                        ),
                "COMPLETED" =>
                    json.Deserialize<HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration"
                        ),
                "FAILED" =>
                    json.Deserialize<HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed"
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
        public Queued(HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued value)
        {
            Value = value;
        }

        internal HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Queued(
            HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationQueued value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for IN_PROGRESS
    /// </summary>
    [Serializable]
    public struct InProgress
    {
        public InProgress(
            HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress value
        )
        {
            Value = value;
        }

        internal HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator InProgress(
            HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationInProgress value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for COMPLETED
    /// </summary>
    [Serializable]
    public struct Completed
    {
        public Completed(
            HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration value
        )
        {
            Value = value;
        }

        internal HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Completed(
            HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationCompletedEmbeddingGeneration value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for FAILED
    /// </summary>
    [Serializable]
    public struct Failed
    {
        public Failed(HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed value)
        {
            Value = value;
        }

        internal HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Failed(
            HumeApi.ExpressionMeasurement.Batch.StateEmbeddingGenerationFailed value
        ) => new(value);
    }
}
