using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.EmpathicVoice;

[JsonConverter(typeof(StringEnumSerializer<LanguageModelType>))]
[Serializable]
public readonly record struct LanguageModelType : IStringEnum
{
    public static readonly LanguageModelType Claude37SonnetLatest = new(
        Values.Claude37SonnetLatest
    );

    public static readonly LanguageModelType Claude35SonnetLatest = new(
        Values.Claude35SonnetLatest
    );

    public static readonly LanguageModelType Claude35HaikuLatest = new(Values.Claude35HaikuLatest);

    public static readonly LanguageModelType Claude35Sonnet20240620 = new(
        Values.Claude35Sonnet20240620
    );

    public static readonly LanguageModelType Claude3Opus20240229 = new(Values.Claude3Opus20240229);

    public static readonly LanguageModelType Claude3Sonnet20240229 = new(
        Values.Claude3Sonnet20240229
    );

    public static readonly LanguageModelType Claude3Haiku20240307 = new(
        Values.Claude3Haiku20240307
    );

    public static readonly LanguageModelType ClaudeSonnet420250514 = new(
        Values.ClaudeSonnet420250514
    );

    public static readonly LanguageModelType ClaudeSonnet4520250929 = new(
        Values.ClaudeSonnet4520250929
    );

    public static readonly LanguageModelType UsAnthropicClaude35Haiku20241022V10 = new(
        Values.UsAnthropicClaude35Haiku20241022V10
    );

    public static readonly LanguageModelType UsAnthropicClaude35Sonnet20240620V10 = new(
        Values.UsAnthropicClaude35Sonnet20240620V10
    );

    public static readonly LanguageModelType UsAnthropicClaude3Haiku20240307V10 = new(
        Values.UsAnthropicClaude3Haiku20240307V10
    );

    public static readonly LanguageModelType GptOss120B = new(Values.GptOss120B);

    public static readonly LanguageModelType Qwen3235Ba22B = new(Values.Qwen3235Ba22B);

    public static readonly LanguageModelType Qwen3235Ba22BInstruct2507 = new(
        Values.Qwen3235Ba22BInstruct2507
    );

    public static readonly LanguageModelType Qwen3235Ba22BThinking2507 = new(
        Values.Qwen3235Ba22BThinking2507
    );

    public static readonly LanguageModelType Gemini15Pro = new(Values.Gemini15Pro);

    public static readonly LanguageModelType Gemini15Flash = new(Values.Gemini15Flash);

    public static readonly LanguageModelType Gemini15Pro002 = new(Values.Gemini15Pro002);

    public static readonly LanguageModelType Gemini15Flash002 = new(Values.Gemini15Flash002);

    public static readonly LanguageModelType Gemini20Flash = new(Values.Gemini20Flash);

    public static readonly LanguageModelType Gemini25Flash = new(Values.Gemini25Flash);

    public static readonly LanguageModelType Gemini25FlashPreview0417 = new(
        Values.Gemini25FlashPreview0417
    );

    public static readonly LanguageModelType Gpt4Turbo = new(Values.Gpt4Turbo);

    public static readonly LanguageModelType Gpt4TurboPreview = new(Values.Gpt4TurboPreview);

    public static readonly LanguageModelType Gpt35Turbo0125 = new(Values.Gpt35Turbo0125);

    public static readonly LanguageModelType Gpt35Turbo = new(Values.Gpt35Turbo);

    public static readonly LanguageModelType Gpt4O = new(Values.Gpt4O);

    public static readonly LanguageModelType Gpt4OMini = new(Values.Gpt4OMini);

    public static readonly LanguageModelType Gpt41 = new(Values.Gpt41);

    public static readonly LanguageModelType Gpt5 = new(Values.Gpt5);

    public static readonly LanguageModelType Gpt5Mini = new(Values.Gpt5Mini);

    public static readonly LanguageModelType Gpt5Nano = new(Values.Gpt5Nano);

    public static readonly LanguageModelType Gpt4OPriority = new(Values.Gpt4OPriority);

    public static readonly LanguageModelType Gpt4OMiniPriority = new(Values.Gpt4OMiniPriority);

    public static readonly LanguageModelType Gpt41Priority = new(Values.Gpt41Priority);

    public static readonly LanguageModelType Gpt5Priority = new(Values.Gpt5Priority);

    public static readonly LanguageModelType Gpt5MiniPriority = new(Values.Gpt5MiniPriority);

    public static readonly LanguageModelType Gpt5NanoPriority = new(Values.Gpt5NanoPriority);

    public static readonly LanguageModelType Gemma7BIt = new(Values.Gemma7BIt);

    public static readonly LanguageModelType Llama38B8192 = new(Values.Llama38B8192);

    public static readonly LanguageModelType Llama370B8192 = new(Values.Llama370B8192);

    public static readonly LanguageModelType Llama3170BVersatile = new(Values.Llama3170BVersatile);

    public static readonly LanguageModelType Llama3370BVersatile = new(Values.Llama3370BVersatile);

    public static readonly LanguageModelType Llama318BInstant = new(Values.Llama318BInstant);

    public static readonly LanguageModelType MoonshotaiKimiK2Instruct = new(
        Values.MoonshotaiKimiK2Instruct
    );

    public static readonly LanguageModelType AccountsFireworksModelsMixtral8X7BInstruct = new(
        Values.AccountsFireworksModelsMixtral8X7BInstruct
    );

    public static readonly LanguageModelType AccountsFireworksModelsLlamaV3P1405BInstruct = new(
        Values.AccountsFireworksModelsLlamaV3P1405BInstruct
    );

    public static readonly LanguageModelType AccountsFireworksModelsLlamaV3P170BInstruct = new(
        Values.AccountsFireworksModelsLlamaV3P170BInstruct
    );

    public static readonly LanguageModelType AccountsFireworksModelsLlamaV3P18BInstruct = new(
        Values.AccountsFireworksModelsLlamaV3P18BInstruct
    );

    public static readonly LanguageModelType Sonar = new(Values.Sonar);

    public static readonly LanguageModelType SonarPro = new(Values.SonarPro);

    public static readonly LanguageModelType Sambanova = new(Values.Sambanova);

    public static readonly LanguageModelType DeepSeekR1DistillLlama70B = new(
        Values.DeepSeekR1DistillLlama70B
    );

    public static readonly LanguageModelType Llama4Maverick17B128EInstruct = new(
        Values.Llama4Maverick17B128EInstruct
    );

    public static readonly LanguageModelType Qwen332B = new(Values.Qwen332B);

    public static readonly LanguageModelType Grok4FastNonReasoningLatest = new(
        Values.Grok4FastNonReasoningLatest
    );

    public static readonly LanguageModelType Ellm = new(Values.Ellm);

    public static readonly LanguageModelType CustomLanguageModel = new(Values.CustomLanguageModel);

    public static readonly LanguageModelType HumeEvi3WebSearch = new(Values.HumeEvi3WebSearch);

    public LanguageModelType(string value)
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
    public static LanguageModelType FromCustom(string value)
    {
        return new LanguageModelType(value);
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

    public static bool operator ==(LanguageModelType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(LanguageModelType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(LanguageModelType value) => value.Value;

    public static explicit operator LanguageModelType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Claude37SonnetLatest = "claude-3-7-sonnet-latest";

        public const string Claude35SonnetLatest = "claude-3-5-sonnet-latest";

        public const string Claude35HaikuLatest = "claude-3-5-haiku-latest";

        public const string Claude35Sonnet20240620 = "claude-3-5-sonnet-20240620";

        public const string Claude3Opus20240229 = "claude-3-opus-20240229";

        public const string Claude3Sonnet20240229 = "claude-3-sonnet-20240229";

        public const string Claude3Haiku20240307 = "claude-3-haiku-20240307";

        public const string ClaudeSonnet420250514 = "claude-sonnet-4-20250514";

        public const string ClaudeSonnet4520250929 = "claude-sonnet-4-5-20250929";

        public const string UsAnthropicClaude35Haiku20241022V10 =
            "us.anthropic.claude-3-5-haiku-20241022-v1:0";

        public const string UsAnthropicClaude35Sonnet20240620V10 =
            "us.anthropic.claude-3-5-sonnet-20240620-v1:0";

        public const string UsAnthropicClaude3Haiku20240307V10 =
            "us.anthropic.claude-3-haiku-20240307-v1:0";

        public const string GptOss120B = "gpt-oss-120b";

        public const string Qwen3235Ba22B = "qwen-3-235b-a22b";

        public const string Qwen3235Ba22BInstruct2507 = "qwen-3-235b-a22b-instruct-2507";

        public const string Qwen3235Ba22BThinking2507 = "qwen-3-235b-a22b-thinking-2507";

        public const string Gemini15Pro = "gemini-1.5-pro";

        public const string Gemini15Flash = "gemini-1.5-flash";

        public const string Gemini15Pro002 = "gemini-1.5-pro-002";

        public const string Gemini15Flash002 = "gemini-1.5-flash-002";

        public const string Gemini20Flash = "gemini-2.0-flash";

        public const string Gemini25Flash = "gemini-2.5-flash";

        public const string Gemini25FlashPreview0417 = "gemini-2.5-flash-preview-04-17";

        public const string Gpt4Turbo = "gpt-4-turbo";

        public const string Gpt4TurboPreview = "gpt-4-turbo-preview";

        public const string Gpt35Turbo0125 = "gpt-3.5-turbo-0125";

        public const string Gpt35Turbo = "gpt-3.5-turbo";

        public const string Gpt4O = "gpt-4o";

        public const string Gpt4OMini = "gpt-4o-mini";

        public const string Gpt41 = "gpt-4.1";

        public const string Gpt5 = "gpt-5";

        public const string Gpt5Mini = "gpt-5-mini";

        public const string Gpt5Nano = "gpt-5-nano";

        public const string Gpt4OPriority = "gpt-4o-priority";

        public const string Gpt4OMiniPriority = "gpt-4o-mini-priority";

        public const string Gpt41Priority = "gpt-4.1-priority";

        public const string Gpt5Priority = "gpt-5-priority";

        public const string Gpt5MiniPriority = "gpt-5-mini-priority";

        public const string Gpt5NanoPriority = "gpt-5-nano-priority";

        public const string Gemma7BIt = "gemma-7b-it";

        public const string Llama38B8192 = "llama3-8b-8192";

        public const string Llama370B8192 = "llama3-70b-8192";

        public const string Llama3170BVersatile = "llama-3.1-70b-versatile";

        public const string Llama3370BVersatile = "llama-3.3-70b-versatile";

        public const string Llama318BInstant = "llama-3.1-8b-instant";

        public const string MoonshotaiKimiK2Instruct = "moonshotai/kimi-k2-instruct";

        public const string AccountsFireworksModelsMixtral8X7BInstruct =
            "accounts/fireworks/models/mixtral-8x7b-instruct";

        public const string AccountsFireworksModelsLlamaV3P1405BInstruct =
            "accounts/fireworks/models/llama-v3p1-405b-instruct";

        public const string AccountsFireworksModelsLlamaV3P170BInstruct =
            "accounts/fireworks/models/llama-v3p1-70b-instruct";

        public const string AccountsFireworksModelsLlamaV3P18BInstruct =
            "accounts/fireworks/models/llama-v3p1-8b-instruct";

        public const string Sonar = "sonar";

        public const string SonarPro = "sonar-pro";

        public const string Sambanova = "sambanova";

        public const string DeepSeekR1DistillLlama70B = "DeepSeek-R1-Distill-Llama-70B";

        public const string Llama4Maverick17B128EInstruct = "Llama-4-Maverick-17B-128E-Instruct";

        public const string Qwen332B = "Qwen3-32B";

        public const string Grok4FastNonReasoningLatest = "grok-4-fast-non-reasoning-latest";

        public const string Ellm = "ellm";

        public const string CustomLanguageModel = "custom-language-model";

        public const string HumeEvi3WebSearch = "hume-evi-3-web-search";
    }
}
