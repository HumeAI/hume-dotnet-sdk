// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Batch;

[JsonConverter(typeof(StateInference.JsonConverter))]
[Serializable]
public record StateInference
{
    internal StateInference(string type, object? value)
    {
        Status = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of StateInference with <see cref="StateInference.Queued"/>.
    /// </summary>
    public StateInference(StateInference.Queued value)
    {
        Status = "QUEUED";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of StateInference with <see cref="StateInference.InProgress"/>.
    /// </summary>
    public StateInference(StateInference.InProgress value)
    {
        Status = "IN_PROGRESS";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of StateInference with <see cref="StateInference.Completed"/>.
    /// </summary>
    public StateInference(StateInference.Completed value)
    {
        Status = "COMPLETED";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of StateInference with <see cref="StateInference.Failed"/>.
    /// </summary>
    public StateInference(StateInference.Failed value)
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
    /// Returns the value as a <see cref="Hume.ExpressionMeasurement.Batch.QueuedState"/> if <see cref="Status"/> is 'QUEUED', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'QUEUED'.</exception>
    public Hume.ExpressionMeasurement.Batch.QueuedState AsQueued() =>
        IsQueued
            ? (Hume.ExpressionMeasurement.Batch.QueuedState)Value!
            : throw new System.Exception("StateInference.Status is not 'QUEUED'");

    /// <summary>
    /// Returns the value as a <see cref="Hume.ExpressionMeasurement.Batch.InProgressState"/> if <see cref="Status"/> is 'IN_PROGRESS', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'IN_PROGRESS'.</exception>
    public Hume.ExpressionMeasurement.Batch.InProgressState AsInProgress() =>
        IsInProgress
            ? (Hume.ExpressionMeasurement.Batch.InProgressState)Value!
            : throw new System.Exception("StateInference.Status is not 'IN_PROGRESS'");

    /// <summary>
    /// Returns the value as a <see cref="Hume.ExpressionMeasurement.Batch.CompletedState"/> if <see cref="Status"/> is 'COMPLETED', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'COMPLETED'.</exception>
    public Hume.ExpressionMeasurement.Batch.CompletedState AsCompleted() =>
        IsCompleted
            ? (Hume.ExpressionMeasurement.Batch.CompletedState)Value!
            : throw new System.Exception("StateInference.Status is not 'COMPLETED'");

    /// <summary>
    /// Returns the value as a <see cref="Hume.ExpressionMeasurement.Batch.FailedState"/> if <see cref="Status"/> is 'FAILED', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'FAILED'.</exception>
    public Hume.ExpressionMeasurement.Batch.FailedState AsFailed() =>
        IsFailed
            ? (Hume.ExpressionMeasurement.Batch.FailedState)Value!
            : throw new System.Exception("StateInference.Status is not 'FAILED'");

    public T Match<T>(
        Func<Hume.ExpressionMeasurement.Batch.QueuedState, T> onQueued,
        Func<Hume.ExpressionMeasurement.Batch.InProgressState, T> onInProgress,
        Func<Hume.ExpressionMeasurement.Batch.CompletedState, T> onCompleted,
        Func<Hume.ExpressionMeasurement.Batch.FailedState, T> onFailed,
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
        Action<Hume.ExpressionMeasurement.Batch.QueuedState> onQueued,
        Action<Hume.ExpressionMeasurement.Batch.InProgressState> onInProgress,
        Action<Hume.ExpressionMeasurement.Batch.CompletedState> onCompleted,
        Action<Hume.ExpressionMeasurement.Batch.FailedState> onFailed,
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
    /// Attempts to cast the value to a <see cref="Hume.ExpressionMeasurement.Batch.QueuedState"/> and returns true if successful.
    /// </summary>
    public bool TryAsQueued(out Hume.ExpressionMeasurement.Batch.QueuedState? value)
    {
        if (Status == "QUEUED")
        {
            value = (Hume.ExpressionMeasurement.Batch.QueuedState)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Hume.ExpressionMeasurement.Batch.InProgressState"/> and returns true if successful.
    /// </summary>
    public bool TryAsInProgress(out Hume.ExpressionMeasurement.Batch.InProgressState? value)
    {
        if (Status == "IN_PROGRESS")
        {
            value = (Hume.ExpressionMeasurement.Batch.InProgressState)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Hume.ExpressionMeasurement.Batch.CompletedState"/> and returns true if successful.
    /// </summary>
    public bool TryAsCompleted(out Hume.ExpressionMeasurement.Batch.CompletedState? value)
    {
        if (Status == "COMPLETED")
        {
            value = (Hume.ExpressionMeasurement.Batch.CompletedState)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Hume.ExpressionMeasurement.Batch.FailedState"/> and returns true if successful.
    /// </summary>
    public bool TryAsFailed(out Hume.ExpressionMeasurement.Batch.FailedState? value)
    {
        if (Status == "FAILED")
        {
            value = (Hume.ExpressionMeasurement.Batch.FailedState)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator StateInference(StateInference.Queued value) => new(value);

    public static implicit operator StateInference(StateInference.InProgress value) => new(value);

    public static implicit operator StateInference(StateInference.Completed value) => new(value);

    public static implicit operator StateInference(StateInference.Failed value) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<StateInference>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(StateInference).IsAssignableFrom(typeToConvert);

        public override StateInference Read(
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
                "QUEUED" => json.Deserialize<Hume.ExpressionMeasurement.Batch.QueuedState?>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Hume.ExpressionMeasurement.Batch.QueuedState"
                    ),
                "IN_PROGRESS" =>
                    json.Deserialize<Hume.ExpressionMeasurement.Batch.InProgressState?>(options)
                        ?? throw new JsonException(
                            "Failed to deserialize Hume.ExpressionMeasurement.Batch.InProgressState"
                        ),
                "COMPLETED" => json.Deserialize<Hume.ExpressionMeasurement.Batch.CompletedState?>(
                    options
                )
                    ?? throw new JsonException(
                        "Failed to deserialize Hume.ExpressionMeasurement.Batch.CompletedState"
                    ),
                "FAILED" => json.Deserialize<Hume.ExpressionMeasurement.Batch.FailedState?>(options)
                    ?? throw new JsonException(
                        "Failed to deserialize Hume.ExpressionMeasurement.Batch.FailedState"
                    ),
                _ => json.Deserialize<object?>(options),
            };
            return new StateInference(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            StateInference value,
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
        public Queued(Hume.ExpressionMeasurement.Batch.QueuedState value)
        {
            Value = value;
        }

        internal Hume.ExpressionMeasurement.Batch.QueuedState Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator StateInference.Queued(
            Hume.ExpressionMeasurement.Batch.QueuedState value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for IN_PROGRESS
    /// </summary>
    [Serializable]
    public struct InProgress
    {
        public InProgress(Hume.ExpressionMeasurement.Batch.InProgressState value)
        {
            Value = value;
        }

        internal Hume.ExpressionMeasurement.Batch.InProgressState Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator StateInference.InProgress(
            Hume.ExpressionMeasurement.Batch.InProgressState value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for COMPLETED
    /// </summary>
    [Serializable]
    public struct Completed
    {
        public Completed(Hume.ExpressionMeasurement.Batch.CompletedState value)
        {
            Value = value;
        }

        internal Hume.ExpressionMeasurement.Batch.CompletedState Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator StateInference.Completed(
            Hume.ExpressionMeasurement.Batch.CompletedState value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for FAILED
    /// </summary>
    [Serializable]
    public struct Failed
    {
        public Failed(Hume.ExpressionMeasurement.Batch.FailedState value)
        {
            Value = value;
        }

        internal Hume.ExpressionMeasurement.Batch.FailedState Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator StateInference.Failed(
            Hume.ExpressionMeasurement.Batch.FailedState value
        ) => new(value);
    }
}
