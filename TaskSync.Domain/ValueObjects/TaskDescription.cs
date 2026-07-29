using TaskSync.SharedKernel.Primitives;

namespace TaskSync.Domain.ValueObjects;

public sealed class TaskDescription : ValueObject
{
    public const int MaxLength = 2000;

    public string Value { get; }

    public TaskDescription(string value)
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

    public static implicit operator string(TaskDescription value) => value.Value;

    public static explicit operator TaskDescription(string value) => new(value);
}