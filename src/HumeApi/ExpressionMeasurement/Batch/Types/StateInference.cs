// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Batch;

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
    /// Returns the value as a <see cref="HumeApi.ExpressionMeasurement.Batch.QueuedState"/> if <see cref="Status"/> is 'QUEUED', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'QUEUED'.</exception>
    public HumeApi.ExpressionMeasurement.Batch.QueuedState AsQueued() =>
        IsQueued
            ? (HumeApi.ExpressionMeasurement.Batch.QueuedState)Value!
            : throw new Exception("StateInference.Status is not 'QUEUED'");

    /// <summary>
    /// Returns the value as a <see cref="HumeApi.ExpressionMeasurement.Batch.InProgressState"/> if <see cref="Status"/> is 'IN_PROGRESS', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'IN_PROGRESS'.</exception>
    public HumeApi.ExpressionMeasurement.Batch.InProgressState AsInProgress() =>
        IsInProgress
            ? (HumeApi.ExpressionMeasurement.Batch.InProgressState)Value!
            : throw new Exception("StateInference.Status is not 'IN_PROGRESS'");

    /// <summary>
    /// Returns the value as a <see cref="HumeApi.ExpressionMeasurement.Batch.CompletedState"/> if <see cref="Status"/> is 'COMPLETED', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'COMPLETED'.</exception>
    public HumeApi.ExpressionMeasurement.Batch.CompletedState AsCompleted() =>
        IsCompleted
            ? (HumeApi.ExpressionMeasurement.Batch.CompletedState)Value!
            : throw new Exception("StateInference.Status is not 'COMPLETED'");

    /// <summary>
    /// Returns the value as a <see cref="HumeApi.ExpressionMeasurement.Batch.FailedState"/> if <see cref="Status"/> is 'FAILED', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'FAILED'.</exception>
    public HumeApi.ExpressionMeasurement.Batch.FailedState AsFailed() =>
        IsFailed
            ? (HumeApi.ExpressionMeasurement.Batch.FailedState)Value!
            : throw new Exception("StateInference.Status is not 'FAILED'");

    public T Match<T>(
        Func<HumeApi.ExpressionMeasurement.Batch.QueuedState, T> onQueued,
        Func<HumeApi.ExpressionMeasurement.Batch.InProgressState, T> onInProgress,
        Func<HumeApi.ExpressionMeasurement.Batch.CompletedState, T> onCompleted,
        Func<HumeApi.ExpressionMeasurement.Batch.FailedState, T> onFailed,
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
        Action<HumeApi.ExpressionMeasurement.Batch.QueuedState> onQueued,
        Action<HumeApi.ExpressionMeasurement.Batch.InProgressState> onInProgress,
        Action<HumeApi.ExpressionMeasurement.Batch.CompletedState> onCompleted,
        Action<HumeApi.ExpressionMeasurement.Batch.FailedState> onFailed,
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
    /// Attempts to cast the value to a <see cref="HumeApi.ExpressionMeasurement.Batch.QueuedState"/> and returns true if successful.
    /// </summary>
    public bool TryAsQueued(out HumeApi.ExpressionMeasurement.Batch.QueuedState? value)
    {
        if (Status == "QUEUED")
        {
            value = (HumeApi.ExpressionMeasurement.Batch.QueuedState)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="HumeApi.ExpressionMeasurement.Batch.InProgressState"/> and returns true if successful.
    /// </summary>
    public bool TryAsInProgress(out HumeApi.ExpressionMeasurement.Batch.InProgressState? value)
    {
        if (Status == "IN_PROGRESS")
        {
            value = (HumeApi.ExpressionMeasurement.Batch.InProgressState)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="HumeApi.ExpressionMeasurement.Batch.CompletedState"/> and returns true if successful.
    /// </summary>
    public bool TryAsCompleted(out HumeApi.ExpressionMeasurement.Batch.CompletedState? value)
    {
        if (Status == "COMPLETED")
        {
            value = (HumeApi.ExpressionMeasurement.Batch.CompletedState)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="HumeApi.ExpressionMeasurement.Batch.FailedState"/> and returns true if successful.
    /// </summary>
    public bool TryAsFailed(out HumeApi.ExpressionMeasurement.Batch.FailedState? value)
    {
        if (Status == "FAILED")
        {
            value = (HumeApi.ExpressionMeasurement.Batch.FailedState)Value!;
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
                "QUEUED" => json.Deserialize<HumeApi.ExpressionMeasurement.Batch.QueuedState>(
                    options
                )
                    ?? throw new JsonException(
                        "Failed to deserialize HumeApi.ExpressionMeasurement.Batch.QueuedState"
                    ),
                "IN_PROGRESS" =>
                    json.Deserialize<HumeApi.ExpressionMeasurement.Batch.InProgressState>(options)
                        ?? throw new JsonException(
                            "Failed to deserialize HumeApi.ExpressionMeasurement.Batch.InProgressState"
                        ),
                "COMPLETED" => json.Deserialize<HumeApi.ExpressionMeasurement.Batch.CompletedState>(
                    options
                )
                    ?? throw new JsonException(
                        "Failed to deserialize HumeApi.ExpressionMeasurement.Batch.CompletedState"
                    ),
                "FAILED" => json.Deserialize<HumeApi.ExpressionMeasurement.Batch.FailedState>(
                    options
                )
                    ?? throw new JsonException(
                        "Failed to deserialize HumeApi.ExpressionMeasurement.Batch.FailedState"
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
        public Queued(HumeApi.ExpressionMeasurement.Batch.QueuedState value)
        {
            Value = value;
        }

        internal HumeApi.ExpressionMeasurement.Batch.QueuedState Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Queued(
            HumeApi.ExpressionMeasurement.Batch.QueuedState value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for IN_PROGRESS
    /// </summary>
    [Serializable]
    public struct InProgress
    {
        public InProgress(HumeApi.ExpressionMeasurement.Batch.InProgressState value)
        {
            Value = value;
        }

        internal HumeApi.ExpressionMeasurement.Batch.InProgressState Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator InProgress(
            HumeApi.ExpressionMeasurement.Batch.InProgressState value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for COMPLETED
    /// </summary>
    [Serializable]
    public struct Completed
    {
        public Completed(HumeApi.ExpressionMeasurement.Batch.CompletedState value)
        {
            Value = value;
        }

        internal HumeApi.ExpressionMeasurement.Batch.CompletedState Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Completed(
            HumeApi.ExpressionMeasurement.Batch.CompletedState value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for FAILED
    /// </summary>
    [Serializable]
    public struct Failed
    {
        public Failed(HumeApi.ExpressionMeasurement.Batch.FailedState value)
        {
            Value = value;
        }

        internal HumeApi.ExpressionMeasurement.Batch.FailedState Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Failed(
            HumeApi.ExpressionMeasurement.Batch.FailedState value
        ) => new(value);
    }
}
