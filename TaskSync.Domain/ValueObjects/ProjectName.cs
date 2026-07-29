using TaskSync.SharedKernel.Primitives;

namespace TaskSync.Domain.ValueObjects;

public sealed class ProjectName : ValueObject
{
    public const int MaxLength = 100;

    public string Value { get; }

    public ProjectName(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        value = value.Trim();

        if (value.Length > MaxLength)
            throw new ArgumentException($"Project name cannot exceed {MaxLength} characters.");

        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    public static implicit operator string(ProjectName name) => name.Value;

    public static explicit operator ProjectName(string value) => new(value);
}