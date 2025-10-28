using PACE.Utils.Enums;

namespace PACE.Models.GoalModels;

public class GoalResponseDTO
{
    public int Id { get; set; }
    public int EvaluationId { get; set; }
    public GoalCategoryEnum GoalCategory { get; set; }
    public GoalTypeEnum GoalType { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ApprovalEnum? Approval { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime DueDate { get; set; }
    public GoalStatusEnum Status { get; set; }
    public string CreatedBy { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public string? UpdatedBy { get; private set; }
    public DateTime? UpdatedOn { get; private set; }

    public GoalResponseDTO()
    {
        Id = 0;
        EvaluationId = 0;
        GoalCategory = GoalCategoryEnum.Personal;
        GoalType = GoalTypeEnum.SelfAssigned;
        Title = string.Empty;
        StartDate = null;
        EndDate = null;
        DueDate = DateTime.UtcNow;
        Approval = null;
        Description = string.Empty;
        Status = GoalStatusEnum.NotStarted;
        CreatedBy = string.Empty;
        CreatedOn = DateTime.UtcNow;
        UpdatedBy = null;
        UpdatedOn = null;
    }

    public GoalResponseDTO(
            int id,
            int evaluationId,
            GoalCategoryEnum goalCategory,
            GoalTypeEnum goalType,
            string title,
            DateTime? startDate,
            DateTime? endDate,
            DateTime dueDate,
            ApprovalEnum? approval,
            string description,
            GoalStatusEnum status,
            string createdBy,
            DateTime createdOn,
            string? updatedBy,
            DateTime? updatedOn
        )
    {
        Id = id;
        EvaluationId = evaluationId;
        GoalCategory = goalCategory;
        GoalType = goalType;
        Title = title;
        StartDate = startDate;
        EndDate = endDate;
        DueDate = dueDate;
        Approval = approval;
        Description = description;
        Status = status;
        CreatedBy = createdBy;
        CreatedOn = createdOn;
        UpdatedBy = updatedBy;
        UpdatedOn = updatedOn;
    }
}
