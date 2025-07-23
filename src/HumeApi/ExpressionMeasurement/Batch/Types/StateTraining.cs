// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Batch;

[JsonConverter(typeof(StateTraining.JsonConverter))]
[Serializable]
public record StateTraining
{
    internal StateTraining(string type, object? value)
    {
        Status = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of StateTraining with <see cref="StateTraining.Queued"/>.
    /// </summary>
    public StateTraining(StateTraining.Queued value)
    {
        Status = "QUEUED";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of StateTraining with <see cref="StateTraining.InProgress"/>.
    /// </summary>
    public StateTraining(StateTraining.InProgress value)
    {
        Status = "IN_PROGRESS";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of StateTraining with <see cref="StateTraining.Completed"/>.
    /// </summary>
    public StateTraining(StateTraining.Completed value)
    {
        Status = "COMPLETED";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of StateTraining with <see cref="StateTraining.Failed"/>.
    /// </summary>
    public StateTraining(StateTraining.Failed value)
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
    /// Returns the value as a <see cref="HumeApi.ExpressionMeasurement.Batch.StateTrainingQueued"/> if <see cref="Status"/> is 'QUEUED', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'QUEUED'.</exception>
    public HumeApi.ExpressionMeasurement.Batch.StateTrainingQueued AsQueued() =>
        IsQueued
            ? (HumeApi.ExpressionMeasurement.Batch.StateTrainingQueued)Value!
            : throw new Exception("StateTraining.Status is not 'QUEUED'");

    /// <summary>
    /// Returns the value as a <see cref="HumeApi.ExpressionMeasurement.Batch.StateTrainingInProgress"/> if <see cref="Status"/> is 'IN_PROGRESS', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'IN_PROGRESS'.</exception>
    public HumeApi.ExpressionMeasurement.Batch.StateTrainingInProgress AsInProgress() =>
        IsInProgress
            ? (HumeApi.ExpressionMeasurement.Batch.StateTrainingInProgress)Value!
            : throw new Exception("StateTraining.Status is not 'IN_PROGRESS'");

    /// <summary>
    /// Returns the value as a <see cref="HumeApi.ExpressionMeasurement.Batch.StateTrainingCompletedTraining"/> if <see cref="Status"/> is 'COMPLETED', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'COMPLETED'.</exception>
    public HumeApi.ExpressionMeasurement.Batch.StateTrainingCompletedTraining AsCompleted() =>
        IsCompleted
            ? (HumeApi.ExpressionMeasurement.Batch.StateTrainingCompletedTraining)Value!
            : throw new Exception("StateTraining.Status is not 'COMPLETED'");

    /// <summary>
    /// Returns the value as a <see cref="HumeApi.ExpressionMeasurement.Batch.StateTrainingFailed"/> if <see cref="Status"/> is 'FAILED', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'FAILED'.</exception>
    public HumeApi.ExpressionMeasurement.Batch.StateTrainingFailed AsFailed() =>
        IsFailed
            ? (HumeApi.ExpressionMeasurement.Batch.StateTrainingFailed)Value!
            : throw new Exception("StateTraining.Status is not 'FAILED'");

    public T Match<T>(
        Func<HumeApi.ExpressionMeasurement.Batch.StateTrainingQueued, T> onQueued,
        Func<HumeApi.ExpressionMeasurement.Batch.StateTrainingInProgress, T> onInProgress,
        Func<HumeApi.ExpressionMeasurement.Batch.StateTrainingCompletedTraining, T> onCompleted,
        Func<HumeApi.ExpressionMeasurement.Batch.StateTrainingFailed, T> onFailed,
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
        Action<HumeApi.ExpressionMeasurement.Batch.StateTrainingQueued> onQueued,
        Action<HumeApi.ExpressionMeasurement.Batch.StateTrainingInProgress> onInProgress,
        Action<HumeApi.ExpressionMeasurement.Batch.StateTrainingCompletedTraining> onCompleted,
        Action<HumeApi.ExpressionMeasurement.Batch.StateTrainingFailed> onFailed,
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
    /// Attempts to cast the value to a <see cref="HumeApi.ExpressionMeasurement.Batch.StateTrainingQueued"/> and returns true if successful.
    /// </summary>
    public bool TryAsQueued(out HumeApi.ExpressionMeasurement.Batch.StateTrainingQueued? value)
    {
        if (Status == "QUEUED")
        {
            value = (HumeApi.ExpressionMeasurement.Batch.StateTrainingQueued)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="HumeApi.ExpressionMeasurement.Batch.StateTrainingInProgress"/> and returns true if successful.
    /// </summary>
    public bool TryAsInProgress(
        out HumeApi.ExpressionMeasurement.Batch.StateTrainingInProgress? value
    )
    {
        if (Status == "IN_PROGRESS")
        {
            value = (HumeApi.ExpressionMeasurement.Batch.StateTrainingInProgress)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="HumeApi.ExpressionMeasurement.Batch.StateTrainingCompletedTraining"/> and returns true if successful.
    /// </summary>
    public bool TryAsCompleted(
        out HumeApi.ExpressionMeasurement.Batch.StateTrainingCompletedTraining? value
    )
    {
        if (Status == "COMPLETED")
        {
            value = (HumeApi.ExpressionMeasurement.Batch.StateTrainingCompletedTraining)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="HumeApi.ExpressionMeasurement.Batch.StateTrainingFailed"/> and returns true if successful.
    /// </summary>
    public bool TryAsFailed(out HumeApi.ExpressionMeasurement.Batch.StateTrainingFailed? value)
    {
        if (Status == "FAILED")
        {
            value = (HumeApi.ExpressionMeasurement.Batch.StateTrainingFailed)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator StateTraining(StateTraining.Queued value) => new(value);

    public static implicit operator StateTraining(StateTraining.InProgress value) => new(value);

    public static implicit operator StateTraining(StateTraining.Completed value) => new(value);

    public static implicit operator StateTraining(StateTraining.Failed value) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<StateTraining>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(StateTraining).IsAssignableFrom(typeToConvert);

        public override StateTraining Read(
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
                    json.Deserialize<HumeApi.ExpressionMeasurement.Batch.StateTrainingQueued>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize HumeApi.ExpressionMeasurement.Batch.StateTrainingQueued"
                        ),
                "IN_PROGRESS" =>
                    json.Deserialize<HumeApi.ExpressionMeasurement.Batch.StateTrainingInProgress>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize HumeApi.ExpressionMeasurement.Batch.StateTrainingInProgress"
                        ),
                "COMPLETED" =>
                    json.Deserialize<HumeApi.ExpressionMeasurement.Batch.StateTrainingCompletedTraining>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize HumeApi.ExpressionMeasurement.Batch.StateTrainingCompletedTraining"
                        ),
                "FAILED" =>
                    json.Deserialize<HumeApi.ExpressionMeasurement.Batch.StateTrainingFailed>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize HumeApi.ExpressionMeasurement.Batch.StateTrainingFailed"
                        ),
                _ => json.Deserialize<object?>(options),
            };
            return new StateTraining(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            StateTraining value,
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
        public Queued(HumeApi.ExpressionMeasurement.Batch.StateTrainingQueued value)
        {
            Value = value;
        }

        internal HumeApi.ExpressionMeasurement.Batch.StateTrainingQueued Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Queued(
            HumeApi.ExpressionMeasurement.Batch.StateTrainingQueued value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for IN_PROGRESS
    /// </summary>
    [Serializable]
    public struct InProgress
    {
        public InProgress(HumeApi.ExpressionMeasurement.Batch.StateTrainingInProgress value)
        {
            Value = value;
        }

        internal HumeApi.ExpressionMeasurement.Batch.StateTrainingInProgress Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator InProgress(
            HumeApi.ExpressionMeasurement.Batch.StateTrainingInProgress value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for COMPLETED
    /// </summary>
    [Serializable]
    public struct Completed
    {
        public Completed(HumeApi.ExpressionMeasurement.Batch.StateTrainingCompletedTraining value)
        {
            Value = value;
        }

        internal HumeApi.ExpressionMeasurement.Batch.StateTrainingCompletedTraining Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Completed(
            HumeApi.ExpressionMeasurement.Batch.StateTrainingCompletedTraining value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for FAILED
    /// </summary>
    [Serializable]
    public struct Failed
    {
        public Failed(HumeApi.ExpressionMeasurement.Batch.StateTrainingFailed value)
        {
            Value = value;
        }

        internal HumeApi.ExpressionMeasurement.Batch.StateTrainingFailed Value { get; set; }

        public override string ToString() => Value.ToString();

        public static implicit operator Failed(
            HumeApi.ExpressionMeasurement.Batch.StateTrainingFailed value
        ) => new(value);
    }
}
