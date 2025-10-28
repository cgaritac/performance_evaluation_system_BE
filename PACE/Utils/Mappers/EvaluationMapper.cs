using PACE.Models.EmployeeModels;
using PACE.Models.EvaluationModels;
using PACE.Models.GoalModels;
using PACE.Utils.Constants;
using PACE.Utils.Enums;
using PACE.Utils.Helpers;

namespace PACE.Utils.Mappers;

public static class EvaluationMapper
{
    public static EvaluationResponseDTO ToDto(this EvaluationModel evaluation) => new(
        evaluation.Id,
        evaluation.EmployeeId,
        evaluation.Employee != null
            ? FullNameHelper.GetFullName(evaluation.Employee.FirstName, evaluation.Employee.LastName)
            : MessagesConstants.NameNotFound,
        evaluation.Year,
        evaluation.FeedbackComments ?? string.Empty,
        evaluation.Feedback ?? FeedbackEnum.NotEvaluated
    );

    public static EvaluationWithGoalsResponseDTO ToEvaluationWithGoalsDto(this EvaluationModel evaluation) => new(
        evaluation.Id,
        evaluation.EmployeeId,
        evaluation.Employee != null
            ? FullNameHelper.GetFullName(evaluation.Employee.FirstName, evaluation.Employee.LastName)
            : MessagesConstants.NameNotFound,
        evaluation.Year,
        evaluation.FeedbackComments ?? string.Empty,
        evaluation.Feedback ?? FeedbackEnum.NotEvaluated,

        (evaluation.Goals ?? new List<GoalModel>())
            .Where(goal => goal.IsActive)
            .Select(goal => goal.ToDto())
            .ToList()
    );

    public static EvaluationModel ToEntity(this EvaluationRequestDTO evaluationRequest, string user) => new(
        evaluationRequest.EmployeeId ?? 0,
        evaluationRequest.Year, 
        user
    );

    public static EvaluationModel ToEvaluation(this EmployeeDTO employee, int year, string user) => new(
        employee.Id,
        year,
        user
    );
}
