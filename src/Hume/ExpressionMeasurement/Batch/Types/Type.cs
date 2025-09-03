using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Batch;

[JsonConverter(typeof(StringEnumSerializer<Type>))]
[Serializable]
public readonly record struct Type : IStringEnum
{
    public static readonly Type EmbeddingGeneration = new(Values.EmbeddingGeneration);

    public static readonly Type Inference = new(Values.Inference);

    public static readonly Type TlInference = new(Values.TlInference);

    public static readonly Type Training = new(Values.Training);

    public Type(string value)
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
    public static Type FromCustom(string value)
    {
        return new Type(value);
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

    public static bool operator ==(Type value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(Type value1, string value2) => !value1.Value.Equals(value2);

    public static explicit operator string(Type value) => value.Value;

    public static explicit operator Type(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string EmbeddingGeneration = "EMBEDDING_GENERATION";

        public const string Inference = "INFERENCE";

        public const string TlInference = "TL_INFERENCE";

        public const string Training = "TRAINING";
    }
}
