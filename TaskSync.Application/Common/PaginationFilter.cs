namespace TaskSync.Application.Common;

public sealed class PaginationFilter
{
    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 20;
}