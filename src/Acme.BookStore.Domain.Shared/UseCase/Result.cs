namespace Acme.BookStore.UseCase;

public class Result<T>
{
    private Result(T value, bool isSuccess, string message)
    {
        Value = value;
        IsSuccess = isSuccess;
        Message = message;
    }

    public static Result<T?> Success(T? value = default)
    {
        return new Result<T?>(value, true, string.Empty);
    }

    public static Result<T?> Failure(string error, T? value = default)
    {
        return new Result<T?>(value, false, error);
    }

    public bool IsSuccess { get; }

    public T Value { get; }

    public string Message { get; }
}