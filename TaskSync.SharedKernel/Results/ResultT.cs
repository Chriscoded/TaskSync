
namespace TaskSync.SharedKernel.Results;

public class Result<T> : Result
{
    private readonly T? _value;

    protected internal Result(T value)
        : base(true, Error.None)
    {
        _value = value;
    }

    protected internal Result(Error error)
        : base(false, error)
    {
    }

    public T Value =>
        IsSuccess
            ? _value!
            : throw new InvalidOperationException("No value for failed result.");

    public static Result<T> Success(T value)
        => new(value);

    public new static Result<T> Failure(Error error)
        => new(error);
}