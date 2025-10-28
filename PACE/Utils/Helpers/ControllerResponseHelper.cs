using Microsoft.AspNetCore.Mvc;
using PACE.Models.CommonModels;
using PACE.Utils.Enums;

namespace PACE.Utils.Helpers;

public class ControllerResponseHelper
{
    public static IActionResult HandleControllerResult<T>(ActionResultDTO<T> result)
    {
        if (result.Success)
            return new OkObjectResult(result.Data);

        return result.ErrorType switch
        {
            ErrorType.NotFound => new NotFoundObjectResult(result.ErrorMessage),
            ErrorType.Validation => new BadRequestObjectResult(result.ErrorMessage),
            ErrorType.Conflict => new ConflictObjectResult(result.ErrorMessage),
            ErrorType.Unauthorized => new UnauthorizedObjectResult(result.ErrorMessage),
            ErrorType.Forbidden => new ForbidResult(),
            _ => new ObjectResult(result.ErrorMessage) { StatusCode = 500 }
        };
    }
}
