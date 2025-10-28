using PACE.Models.GoalModels;
using PACE.Utils.Constants;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PACE.Models.ActivityModels;

public class ActivityModel
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = ErrorConstants.RequiredGoalIdError)]
    public int GoalId { get; set; }

    [Required(ErrorMessage = ErrorConstants.RequiredTitleError)]
    [StringLength(NumericConstants.MaxTitle, ErrorMessage = ErrorConstants.TitleLengthError)]
    public string Title { get; set; }

    [Required(ErrorMessage = ErrorConstants.RequiredDescriptionError)]
    [StringLength(NumericConstants.MaxDescription, ErrorMessage = ErrorConstants.DescriptionMaxLegthError)]
    public string Description { get; set; }

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
    public GoalModel? Goal { get; set; }

    public ActivityModel(int goalId, string title, string description, string? createdBy)
    {
        GoalId = goalId;
        Title = title;
        Description = description;
        CreatedBy = createdBy ?? "";
        CreatedOn = DateTime.UtcNow;
        IsActive = true;
    }

    public void Update(int goalId, string title, string description, string updatedBy)
    {
        GoalId = goalId;
        Title = title;
        Description = description;
        UpdatedBy = updatedBy;
        UpdatedOn = DateTime.UtcNow;
    }

    public void Delete(string deletedBy)
    {
        DeletedBy = deletedBy;
        DeletedOn = DateTime.UtcNow;
        IsActive = false;
    }
}
