// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Batch;

[JsonConverter(typeof(StateTlInference.JsonConverter))]
[Serializable]
public record StateTlInference
{
    internal StateTlInference(string type, object? value)
    {
        Status = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of StateTlInference with <see cref="StateTlInference.Queued"/>.
    /// </summary>
    public StateTlInference(StateTlInference.Queued value)
    {
        Status = "QUEUED";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of StateTlInference with <see cref="StateTlInference.InProgress"/>.
    /// </summary>
    public StateTlInference(StateTlInference.InProgress value)
    {
        Status = "IN_PROGRESS";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of StateTlInference with <see cref="StateTlInference.Completed"/>.
    /// </summary>
    public StateTlInference(StateTlInference.Completed value)
    {
        Status = "COMPLETED";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of StateTlInference with <see cref="StateTlInference.Failed"/>.
    /// </summary>
    public StateTlInference(StateTlInference.Failed value)
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
    /// Returns the value as a <see cref="Hume.ExpressionMeasurement.Batch.StateTlInferenceQueued"/> if <see cref="Status"/> is 'QUEUED', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'QUEUED'.</exception>
    public Hume.ExpressionMeasurement.Batch.StateTlInferenceQueued AsQueued() =>
        IsQueued
            ? (Hume.ExpressionMeasurement.Batch.StateTlInferenceQueued)Value!
            : throw new System.Exception("StateTlInference.Status is not 'QUEUED'");

    /// <summary>
    /// Returns the value as a <see cref="Hume.ExpressionMeasurement.Batch.StateTlInferenceInProgress"/> if <see cref="Status"/> is 'IN_PROGRESS', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'IN_PROGRESS'.</exception>
    public Hume.ExpressionMeasurement.Batch.StateTlInferenceInProgress AsInProgress() =>
        IsInProgress
            ? (Hume.ExpressionMeasurement.Batch.StateTlInferenceInProgress)Value!
            : throw new System.Exception("StateTlInference.Status is not 'IN_PROGRESS'");

    /// <summary>
    /// Returns the value as a <see cref="Hume.ExpressionMeasurement.Batch.StateTlInferenceCompletedTlInference"/> if <see cref="Status"/> is 'COMPLETED', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'COMPLETED'.</exception>
    public Hume.ExpressionMeasurement.Batch.StateTlInferenceCompletedTlInference AsCompleted() =>
        IsCompleted
            ? (Hume.ExpressionMeasurement.Batch.StateTlInferenceCompletedTlInference)Value!
            : throw new System.Exception("StateTlInference.Status is not 'COMPLETED'");

    /// <summary>
    /// Returns the value as a <see cref="Hume.ExpressionMeasurement.Batch.StateTlInferenceFailed"/> if <see cref="Status"/> is 'FAILED', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Status"/> is not 'FAILED'.</exception>
    public Hume.ExpressionMeasurement.Batch.StateTlInferenceFailed AsFailed() =>
        IsFailed
            ? (Hume.ExpressionMeasurement.Batch.StateTlInferenceFailed)Value!
            : throw new System.Exception("StateTlInference.Status is not 'FAILED'");

    public T Match<T>(
        Func<Hume.ExpressionMeasurement.Batch.StateTlInferenceQueued, T> onQueued,
        Func<Hume.ExpressionMeasurement.Batch.StateTlInferenceInProgress, T> onInProgress,
        Func<Hume.ExpressionMeasurement.Batch.StateTlInferenceCompletedTlInference, T> onCompleted,
        Func<Hume.ExpressionMeasurement.Batch.StateTlInferenceFailed, T> onFailed,
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
        Action<Hume.ExpressionMeasurement.Batch.StateTlInferenceQueued> onQueued,
        Action<Hume.ExpressionMeasurement.Batch.StateTlInferenceInProgress> onInProgress,
        Action<Hume.ExpressionMeasurement.Batch.StateTlInferenceCompletedTlInference> onCompleted,
        Action<Hume.ExpressionMeasurement.Batch.StateTlInferenceFailed> onFailed,
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
    /// Attempts to cast the value to a <see cref="Hume.ExpressionMeasurement.Batch.StateTlInferenceQueued"/> and returns true if successful.
    /// </summary>
    public bool TryAsQueued(out Hume.ExpressionMeasurement.Batch.StateTlInferenceQueued? value)
    {
        if (Status == "QUEUED")
        {
            value = (Hume.ExpressionMeasurement.Batch.StateTlInferenceQueued)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Hume.ExpressionMeasurement.Batch.StateTlInferenceInProgress"/> and returns true if successful.
    /// </summary>
    public bool TryAsInProgress(
        out Hume.ExpressionMeasurement.Batch.StateTlInferenceInProgress? value
    )
    {
        if (Status == "IN_PROGRESS")
        {
            value = (Hume.ExpressionMeasurement.Batch.StateTlInferenceInProgress)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Hume.ExpressionMeasurement.Batch.StateTlInferenceCompletedTlInference"/> and returns true if successful.
    /// </summary>
    public bool TryAsCompleted(
        out Hume.ExpressionMeasurement.Batch.StateTlInferenceCompletedTlInference? value
    )
    {
        if (Status == "COMPLETED")
        {
            value = (Hume.ExpressionMeasurement.Batch.StateTlInferenceCompletedTlInference)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="Hume.ExpressionMeasurement.Batch.StateTlInferenceFailed"/> and returns true if successful.
    /// </summary>
    public bool TryAsFailed(out Hume.ExpressionMeasurement.Batch.StateTlInferenceFailed? value)
    {
        if (Status == "FAILED")
        {
            value = (Hume.ExpressionMeasurement.Batch.StateTlInferenceFailed)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator StateTlInference(StateTlInference.Queued value) => new(value);

    public static implicit operator StateTlInference(StateTlInference.InProgress value) =>
        new(value);

    public static implicit operator StateTlInference(StateTlInference.Completed value) =>
        new(value);

    public static implicit operator StateTlInference(StateTlInference.Failed value) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<StateTlInference>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(StateTlInference).IsAssignableFrom(typeToConvert);

        public override StateTlInference Read(
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
                    json.Deserialize<Hume.ExpressionMeasurement.Batch.StateTlInferenceQueued?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Hume.ExpressionMeasurement.Batch.StateTlInferenceQueued"
                        ),
                "IN_PROGRESS" =>
                    json.Deserialize<Hume.ExpressionMeasurement.Batch.StateTlInferenceInProgress?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Hume.ExpressionMeasurement.Batch.StateTlInferenceInProgress"
                        ),
                "COMPLETED" =>
                    json.Deserialize<Hume.ExpressionMeasurement.Batch.StateTlInferenceCompletedTlInference?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Hume.ExpressionMeasurement.Batch.StateTlInferenceCompletedTlInference"
                        ),
                "FAILED" =>
                    json.Deserialize<Hume.ExpressionMeasurement.Batch.StateTlInferenceFailed?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize Hume.ExpressionMeasurement.Batch.StateTlInferenceFailed"
                        ),
                _ => json.Deserialize<object?>(options),
            };
            return new StateTlInference(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            StateTlInference value,
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
        public Queued(Hume.ExpressionMeasurement.Batch.StateTlInferenceQueued value)
        {
            Value = value;
        }

        internal Hume.ExpressionMeasurement.Batch.StateTlInferenceQueued Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator StateTlInference.Queued(
            Hume.ExpressionMeasurement.Batch.StateTlInferenceQueued value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for IN_PROGRESS
    /// </summary>
    [Serializable]
    public struct InProgress
    {
        public InProgress(Hume.ExpressionMeasurement.Batch.StateTlInferenceInProgress value)
        {
            Value = value;
        }

        internal Hume.ExpressionMeasurement.Batch.StateTlInferenceInProgress Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator StateTlInference.InProgress(
            Hume.ExpressionMeasurement.Batch.StateTlInferenceInProgress value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for COMPLETED
    /// </summary>
    [Serializable]
    public struct Completed
    {
        public Completed(
            Hume.ExpressionMeasurement.Batch.StateTlInferenceCompletedTlInference value
        )
        {
            Value = value;
        }

        internal Hume.ExpressionMeasurement.Batch.StateTlInferenceCompletedTlInference Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator StateTlInference.Completed(
            Hume.ExpressionMeasurement.Batch.StateTlInferenceCompletedTlInference value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for FAILED
    /// </summary>
    [Serializable]
    public struct Failed
    {
        public Failed(Hume.ExpressionMeasurement.Batch.StateTlInferenceFailed value)
        {
            Value = value;
        }

        internal Hume.ExpressionMeasurement.Batch.StateTlInferenceFailed Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator StateTlInference.Failed(
            Hume.ExpressionMeasurement.Batch.StateTlInferenceFailed value
        ) => new(value);
    }
}
