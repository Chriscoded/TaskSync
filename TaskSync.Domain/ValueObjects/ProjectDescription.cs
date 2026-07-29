using TaskSync.SharedKernel.Primitives;

namespace TaskSync.Domain.ValueObjects;

public sealed class ProjectDescription : ValueObject
{
    public const int MaxLength = 1000;

    public string Value { get; }

    public ProjectDescription(string value)
    {
        value ??= string.Empty;

        value = value.Trim();

        if (value.Length > MaxLength)
            throw new ArgumentException($"Description cannot exceed {MaxLength} characters.");

        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    public static implicit operator string(ProjectDescription description) => description.Value;

    public static explicit operator ProjectDescription(string value) => new(value);
}