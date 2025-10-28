using PACE.Utils.Enums;

namespace PACE.Models.CommonModels;

public class ActionResultDTO<T>
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public ErrorType ErrorType { get; set; } = ErrorType.Unknown;
    public T? Data { get; set; }

    public static ActionResultDTO<T> Ok(T data) => new()
    {
        Success = true,
        Data = data
    };

    public static ActionResultDTO<T> Fail(string errorMessage, ErrorType errorType = ErrorType.Unknown) => new()
    {
        Success = false,
        ErrorMessage = errorMessage,
        ErrorType = errorType
    };
}
