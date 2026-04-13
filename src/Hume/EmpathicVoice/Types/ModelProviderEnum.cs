using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(ModelProviderEnum.ModelProviderEnumSerializer))]
[Serializable]
public readonly record struct ModelProviderEnum : IStringEnum
{
    public static readonly ModelProviderEnum Groq = new(Values.Groq);

    public static readonly ModelProviderEnum OpenAi = new(Values.OpenAi);

    public static readonly ModelProviderEnum Fireworks = new(Values.Fireworks);

    public static readonly ModelProviderEnum Anthropic = new(Values.Anthropic);

    public static readonly ModelProviderEnum CustomLanguageModel = new(Values.CustomLanguageModel);

    public static readonly ModelProviderEnum Google = new(Values.Google);

    public static readonly ModelProviderEnum HumeAi = new(Values.HumeAi);

    public static readonly ModelProviderEnum AmazonBedrock = new(Values.AmazonBedrock);

    public static readonly ModelProviderEnum Perplexity = new(Values.Perplexity);

    public static readonly ModelProviderEnum Sambanova = new(Values.Sambanova);

    public static readonly ModelProviderEnum Cerebras = new(Values.Cerebras);

    public static readonly ModelProviderEnum XAi = new(Values.XAi);

    public ModelProviderEnum(string value)
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
    public static ModelProviderEnum FromCustom(string value)
    {
        return new ModelProviderEnum(value);
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

    public static bool operator ==(ModelProviderEnum value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ModelProviderEnum value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ModelProviderEnum value) => value.Value;

    public static explicit operator ModelProviderEnum(string value) => new(value);

    internal class ModelProviderEnumSerializer : JsonConverter<ModelProviderEnum>
    {
        public override ModelProviderEnum Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON value could not be read as a string."
                );
            return new ModelProviderEnum(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ModelProviderEnum value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ModelProviderEnum ReadAsPropertyName(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON property name could not be read as a string."
                );
            return new ModelProviderEnum(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ModelProviderEnum value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Value);
        }
    }

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Groq = "GROQ";

        public const string OpenAi = "OPEN_AI";

        public const string Fireworks = "FIREWORKS";

        public const string Anthropic = "ANTHROPIC";

        public const string CustomLanguageModel = "CUSTOM_LANGUAGE_MODEL";

        public const string Google = "GOOGLE";

        public const string HumeAi = "HUME_AI";

        public const string AmazonBedrock = "AMAZON_BEDROCK";

        public const string Perplexity = "PERPLEXITY";

        public const string Sambanova = "SAMBANOVA";

        public const string Cerebras = "CEREBRAS";

        public const string XAi = "X_AI";
    }
}
