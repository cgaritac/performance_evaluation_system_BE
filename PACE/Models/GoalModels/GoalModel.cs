using PACE.Models.ActivityModels;
using PACE.Models.EvaluationModels;
using PACE.Utils.Constants;
using PACE.Utils.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PACE.Models.GoalModels;

public class GoalModel
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = ErrorConstants.RequiredEvaluationIdError)]
    public int EvaluationId { get; set; }

    [Required(ErrorMessage = ErrorConstants.RequiredGoalCategoryError)]
    [EnumDataType(typeof(GoalCategoryEnum))]
    public GoalCategoryEnum GoalCategory { get; set; }

    [Required(ErrorMessage = ErrorConstants.RequiredGoalTypeError)]
    [EnumDataType(typeof(GoalTypeEnum))]
    public GoalTypeEnum GoalType { get; set; }

    [Required(ErrorMessage = ErrorConstants.RequiredTitleError)]
    [StringLength(NumericConstants.MaxTitle, ErrorMessage = ErrorConstants.TitleLengthError)]
    public string Title { get; set; }

    [Required(ErrorMessage = ErrorConstants.RequiredDescriptionError)]
    [StringLength(NumericConstants.MaxDescription, ErrorMessage = ErrorConstants.DescriptionMaxLegthError)]
    public string Description { get; set; }

    [EnumDataType(typeof(ApprovalEnum))]
    public ApprovalEnum? Approval { get; set; }

    [DataType(DataType.Date)]
    public DateTime? StartDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }

    [Required(ErrorMessage = ErrorConstants.RequiredDueDateError)]
    [DataType(DataType.Date)]
    public DateTime DueDate { get; set; }

    [Required(ErrorMessage = ErrorConstants.RequiredStatusError)]
    [EnumDataType(typeof(GoalStatusEnum))]
    public GoalStatusEnum Status { get; set; }

    [JsonIgnore]
    public string CreatedBy { get; private set; }
    [JsonIgnore]
    public DateTime CreatedOn { get; private set; }
    [JsonIgnore]
    public string? UpdatedBy { get; private set; }
    [JsonIgnore]
    public DateTime? UpdatedOn { get; private set; }
    [JsonIgnore]
    public string? DeletedBy { get; private set; }
    [JsonIgnore]
    public DateTime? DeletedOn { get; private set; }
    [JsonIgnore]
    public bool IsActive { get; private set; }

    [JsonIgnore]
    public ICollection<ActivityModel>? Activities { get; set; }

    [JsonIgnore]
    public EvaluationModel? Evaluation { get; set; }

    public GoalModel(
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
            string? createdBy
        )
    {
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
        CreatedBy = createdBy ?? "";
        CreatedOn = DateTime.UtcNow;
        IsActive = true;

        Activities = new List<ActivityModel>();
    }

    public void Update(
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
            string updatedBy
        )
    {
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
        UpdatedBy = updatedBy;
        UpdatedOn = DateTime.UtcNow;
    }
}
