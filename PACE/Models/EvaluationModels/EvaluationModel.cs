using PACE.Models.EmployeeModels;
using PACE.Models.GoalModels;
using PACE.Utils.Constants;
using PACE.Utils.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PACE.Models.EvaluationModels;

public class EvaluationModel
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = ErrorConstants.RequiredEmployeeIdError)]
    public int EmployeeId { get; set; }

    [Required(ErrorMessage = ErrorConstants.RequiredYearError)]
    [Range(NumericConstants.MinYear, NumericConstants.MaxYear, ErrorMessage = ErrorConstants.YearIntervalError)]
    public int Year { get; set; }

    [StringLength(NumericConstants.MaxDescription, ErrorMessage = ErrorConstants.DescriptionMaxLegthError)]
    public string? FeedbackComments { get; set; }

    [EnumDataType(typeof(FeedbackEnum))]
    public FeedbackEnum? Feedback { get; set; }

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
    public ICollection<GoalModel>? Goals { get; set; }
    [JsonIgnore]
    public EmployeeModel? Employee { get; set; }

    public EvaluationModel(int employeeId, int year, string? createdBy)
    {
        EmployeeId = employeeId;
        Year = year;
        Feedback = FeedbackEnum.NotEvaluated;
        CreatedBy = createdBy ?? "";
        CreatedOn = DateTime.UtcNow;
        IsActive = true;

        Goals = new List<GoalModel>();
    }

    public void Update(int year, string? feedbackComments, FeedbackEnum? feedback, string updatedBy)
    {
        Year = year;
        FeedbackComments = feedbackComments;
        Feedback = feedback;
        UpdatedBy = updatedBy;
        UpdatedOn = DateTime.UtcNow;
    }
}