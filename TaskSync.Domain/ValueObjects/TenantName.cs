using TaskSync.SharedKernel.Primitives;

namespace TaskSync.Domain.ValueObjects;

public sealed class TenantName : ValueObject
{
    public const int MaxLength = 100;

    public string Value { get; }

    public TenantName(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        value = value.Trim();

        if (value.Length > MaxLength)
        {
            throw new ArgumentException(
                $"Tenant name cannot exceed {MaxLength} characters.",
                nameof(value));
        }

        Value = value;
    }

    public override string ToString() => Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(TenantName name)
        => name.Value;

    public static explicit operator TenantName(string value)
        => new(value);
}