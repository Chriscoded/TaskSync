using System.Text.RegularExpressions;
using TaskSync.SharedKernel.Primitives;

namespace TaskSync.Domain.ValueObjects;

public sealed partial class TenantSlug : ValueObject
{
    public const int MaxLength = 50;

    private static readonly Regex SlugRegex =
        MyRegex();

    public string Value { get; }

    public TenantSlug(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        value = value.Trim().ToLowerInvariant();

        if (value.Length > MaxLength)
        {
            throw new ArgumentException(
                $"Tenant slug cannot exceed {MaxLength} characters.",
                nameof(value));
        }

        if (!SlugRegex.IsMatch(value))
        {
            throw new ArgumentException(
                "Slug may only contain lowercase letters, numbers and hyphens.",
                nameof(value));
        }

        Value = value;
    }

    public override string ToString() => Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(TenantSlug slug)
        => slug.Value;

    public static explicit operator TenantSlug(string value)
        => new(value);

    [GeneratedRegex("^[a-z0-9-]+$")]
    private static partial Regex MyRegex();
}