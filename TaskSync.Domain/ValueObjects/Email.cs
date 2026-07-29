using System.Net.Mail;
using TaskSync.SharedKernel.Primitives;

namespace TaskSync.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public const int MaxLength = 256;

    public string Value { get; }

    public Email(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        value = value.Trim().ToLowerInvariant();

        if (value.Length > MaxLength)
        {
            throw new ArgumentException(
                $"Email cannot exceed {MaxLength} characters.",
                nameof(value));
        }

        try
        {
            _ = new MailAddress(value);
        }
        catch
        {
            throw new ArgumentException(
                "Invalid email address.",
                nameof(value));
        }

        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
        => Value;

    public static implicit operator string(Email email)
        => email.Value;

    public static explicit operator Email(string value)
        => new(value);
}