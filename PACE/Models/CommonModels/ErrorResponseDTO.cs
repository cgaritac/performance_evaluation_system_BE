using PACE.Utils.Constants;

namespace PACE.Models.CommonModels;

public class ErrorResponseDTO
{
    public bool Success { get; set; } = false;
    public string ErrorMessage { get; set; } = ErrorConstants.UnexpectedError;
    public string? Exception { get; set; }
    public string? StackTrace { get; set; }
}
