using PACE.Models.GoalModels;

namespace PACE.Utils.Mappers;

public static class GoalMapper
{
    public static GoalResponseDTO ToDto(this GoalModel model) => new(
        model.Id,
        model.EvaluationId,
        model.GoalCategory,
        model.GoalType,
        model.Title,
        model.StartDate,
        model.EndDate,
        model.DueDate,
        model.Approval,
        model.Description,
        model.Status,
        model.CreatedBy,
        model.CreatedOn,
        model.UpdatedBy,
        model.UpdatedOn
    );

    public static GoalModel ToEntity(this GoalRequestDTO dto, string user) => new(
        dto.EvaluationId,
        dto.GoalCategory,
        dto.GoalType,
        dto.Title,
        dto.StartDate,
        dto.EndDate,
        dto.DueDate,
        dto.Approval,
        dto.Description,
        dto.Status,
        user
    );
}
