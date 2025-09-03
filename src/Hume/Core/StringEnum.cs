using System.Text.Json.Serialization;

namespace Hume.Core;

public interface IStringEnum : IEquatable<string>
{
    public string Value { get; }
}
