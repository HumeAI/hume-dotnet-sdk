using System.Text.Json.Serialization;
using Hume.Core;

namespace Hume.ExpressionMeasurement.Batch;

[JsonConverter(typeof(StringEnumSerializer<Bcp47Tag>))]
[Serializable]
public readonly record struct Bcp47Tag : IStringEnum
{
    public static readonly Bcp47Tag Zh = new(Values.Zh);

    public static readonly Bcp47Tag Da = new(Values.Da);

    public static readonly Bcp47Tag Nl = new(Values.Nl);

    public static readonly Bcp47Tag En = new(Values.En);

    public static readonly Bcp47Tag EnAu = new(Values.EnAu);

    public static readonly Bcp47Tag EnIn = new(Values.EnIn);

    public static readonly Bcp47Tag EnNz = new(Values.EnNz);

    public static readonly Bcp47Tag EnGb = new(Values.EnGb);

    public static readonly Bcp47Tag Fr = new(Values.Fr);

    public static readonly Bcp47Tag FrCa = new(Values.FrCa);

    public static readonly Bcp47Tag De = new(Values.De);

    public static readonly Bcp47Tag Hi = new(Values.Hi);

    public static readonly Bcp47Tag HiLatn = new(Values.HiLatn);

    public static readonly Bcp47Tag Id = new(Values.Id);

    public static readonly Bcp47Tag It = new(Values.It);

    public static readonly Bcp47Tag Ja = new(Values.Ja);

    public static readonly Bcp47Tag Ko = new(Values.Ko);

    public static readonly Bcp47Tag No = new(Values.No);

    public static readonly Bcp47Tag Pl = new(Values.Pl);

    public static readonly Bcp47Tag Pt = new(Values.Pt);

    public static readonly Bcp47Tag PtBr = new(Values.PtBr);

    public static readonly Bcp47Tag PtPt = new(Values.PtPt);

    public static readonly Bcp47Tag Ru = new(Values.Ru);

    public static readonly Bcp47Tag Es = new(Values.Es);

    public static readonly Bcp47Tag Es419 = new(Values.Es419);

    public static readonly Bcp47Tag Sv = new(Values.Sv);

    public static readonly Bcp47Tag Ta = new(Values.Ta);

    public static readonly Bcp47Tag Tr = new(Values.Tr);

    public static readonly Bcp47Tag Uk = new(Values.Uk);

    public Bcp47Tag(string value)
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
    public static Bcp47Tag FromCustom(string value)
    {
        return new Bcp47Tag(value);
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

    public static bool operator ==(Bcp47Tag value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(Bcp47Tag value1, string value2) => !value1.Value.Equals(value2);

    public static explicit operator string(Bcp47Tag value) => value.Value;

    public static explicit operator Bcp47Tag(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Zh = "zh";

        public const string Da = "da";

        public const string Nl = "nl";

        public const string En = "en";

        public const string EnAu = "en-AU";

        public const string EnIn = "en-IN";

        public const string EnNz = "en-NZ";

        public const string EnGb = "en-GB";

        public const string Fr = "fr";

        public const string FrCa = "fr-CA";

        public const string De = "de";

        public const string Hi = "hi";

        public const string HiLatn = "hi-Latn";

        public const string Id = "id";

        public const string It = "it";

        public const string Ja = "ja";

        public const string Ko = "ko";

        public const string No = "no";

        public const string Pl = "pl";

        public const string Pt = "pt";

        public const string PtBr = "pt-BR";

        public const string PtPt = "pt-PT";

        public const string Ru = "ru";

        public const string Es = "es";

        public const string Es419 = "es-419";

        public const string Sv = "sv";

        public const string Ta = "ta";

        public const string Tr = "tr";

        public const string Uk = "uk";
    }
}
