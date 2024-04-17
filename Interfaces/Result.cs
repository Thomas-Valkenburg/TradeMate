namespace Interfaces;

public class Result
{
    public bool Success { get; }
    public ErrorType? Error { get; }
    public string ErrorPropertyName { get; }
    public string ErrorErrorMessage { get; }

    private Result(bool success, ErrorType? error, string errorMessage, string errorPropertyName)
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
        ErrorErrorMessage = errorMessage;
        ErrorPropertyName = errorPropertyName;
    }
    
    public static Result FromSuccess() => new(true, null, "", "");
    public static Result FromError(ErrorType error, string errorMessage, string errorPropertyName) 
        => new(false, error, errorMessage, errorPropertyName);
}

public enum ErrorType
{
    AlreadyDeleted,
    Duplicate,
    NotFound,
}