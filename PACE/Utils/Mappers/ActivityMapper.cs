using PACE.Models.ActivityModels;

namespace PACE.Utils.Mappers;

public static class ActivityMapper
{
    public static ActivityResponseDTO ToDto(this ActivityModel model) => new(
        model.Id,
        model.GoalId,
        model.Title,
        model.Description,
        model.CreatedBy,
        model.CreatedOn,
        model.UpdatedBy,
        model.UpdatedOn
    );

    public static ActivityModel ToEntity(this ActivityRequestDTO dto, string user) => new(
        dto.GoalId,
        dto.Title,
        dto.Description,
        user
    );
}
