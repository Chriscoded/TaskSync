using TaskSync.SharedKernel.Primitives;

namespace TaskSync.Domain.ValueObjects;

public sealed class TaskTitle : ValueObject
{
    public const int MaxLength = 200;

    public string Value { get; }

    public TaskTitle(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        value = value.Trim();

        if (value.Length > MaxLength)
            throw new ArgumentException($"Task title cannot exceed {MaxLength} characters.");

        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    public static implicit operator string(TaskTitle value) => value.Value;

    public static explicit operator TaskTitle(string value) => new(value);
}