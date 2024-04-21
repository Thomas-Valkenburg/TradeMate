namespace Domain;

public class Result
{
    public bool Success { get; }
    public ErrorType? Error { get; }
    public string ErrorPropertyName { get; }
    public string ErrorMessage { get; }

    protected Result(bool success, ErrorType? error, string errorMessage, string errorPropertyName)
    {
        switch (success)
        {
            case true when error is not null || !string.IsNullOrEmpty(errorMessage):
                throw new InvalidOperationException("Success result should not have a type or errorMessage");
            case false when error is null:
                throw new InvalidOperationException("Failed result should have a type and errorMessage");
        }

        Success = success;
        Error = error;
        ErrorMessage = errorMessage;
        ErrorPropertyName = errorPropertyName;
    }
    
    public static Result FromSuccess() => new(true, null, "", "");
    public static Result<T> FromSuccess<T>(T value) => new(value, true, null, "", "");
    public static Result FromError(ErrorType error, string errorMessage, string errorPropertyName) 
        => new(false, error, errorMessage, errorPropertyName);

    public static Result<T?> FromError<T>(ErrorType error, string errorMessage, string errorPropertyName) =>
	    new(default, false, error, errorMessage, errorPropertyName);
}

public class Result<T> : Result
{
    public T Value { get; set; }

    protected internal Result(T value, bool success, ErrorType? error, string errorMessage, string errorPropertyName) : base(success, error, errorMessage, errorPropertyName)
    {
        Value = value;
    }
}

public enum ErrorType
{
    Unknown,
    Duplicate,
    NotFound,
    Null,
    TooShort,
    TooLong,
}