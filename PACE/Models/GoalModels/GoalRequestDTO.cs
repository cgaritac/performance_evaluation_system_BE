using PACE.Utils.Enums;

namespace PACE.Models.GoalModels;

public class GoalRequestDTO
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

    public GoalRequestDTO()
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
    }

    public GoalRequestDTO(
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
        GoalStatusEnum status)
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
    }
}
