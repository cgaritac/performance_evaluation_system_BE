using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PACE.Utils.Constants;

namespace PACE.Utils.Helpers;

public static class ValidatorHelper
{
    public static List<string> GetModeStateErrors(ModelStateDictionary modelState)
    {
        return modelState.Values
                         .SelectMany(v => v.Errors)
                         .Select(e => e.ErrorMessage)
                         .ToList();
    }

    public static IActionResult BadRequestResult(List<string>? errors, string? error)
    {
        return new BadRequestObjectResult(new
        {
            Success = false,
            Error = error is null
                ? errors
                : new List<string> { error },
        });
    }

    public static string? ValidateGoalDates(DateTime? startDate, DateTime? endDate, DateTime dueDate)
    {
        if (startDate is null && endDate.HasValue)
            return ErrorConstants.EndDateWithoutStartDateError;

        if (startDate.HasValue && endDate.HasValue && startDate > endDate)
            return ErrorConstants.StartDateAfterEndDateError;

        return null;
    }
}