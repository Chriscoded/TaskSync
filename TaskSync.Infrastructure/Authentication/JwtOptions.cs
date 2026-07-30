namespace TaskSync.Infrastructure.Authentication;

public sealed class JwtOptions
{
    public string Issuer { get; set; } = default!;

    public string Audience { get; set; } = default!;

    public string Key { get; set; } = default!;
}