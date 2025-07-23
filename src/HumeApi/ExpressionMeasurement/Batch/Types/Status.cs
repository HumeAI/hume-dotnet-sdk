using System.Text.Json.Serialization;
using HumeApi.Core;

namespace HumeApi.ExpressionMeasurement.Batch;

[JsonConverter(typeof(StringEnumSerializer<Status>))]
[Serializable]
public readonly record struct Status : IStringEnum
{
    public static readonly Status Queued = new(Values.Queued);

    public static readonly Status InProgress = new(Values.InProgress);

    public static readonly Status Completed = new(Values.Completed);

    public static readonly Status Failed = new(Values.Failed);

    public Status(string value)
    {
        Value = value;
    }

    /// <summary>
    /// The string value of the enum.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Create a string enum with the given value.
    /// </summary>
    public static Status FromCustom(string value)
    {
        return new Status(value);
    }

    public bool Equals(string? other)
    {
        return Value.Equals(other);
    }

    /// <summary>
    /// Returns the string value of the enum.
    /// </summary>
    public override string ToString()
    {
        return Value;
    }

    public static bool operator ==(Status value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(Status value1, string value2) => !value1.Value.Equals(value2);

    public static explicit operator string(Status value) => value.Value;

    public static explicit operator Status(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Queued = "QUEUED";

        public const string InProgress = "IN_PROGRESS";

        public const string Completed = "COMPLETED";

        public const string Failed = "FAILED";
    }
}
