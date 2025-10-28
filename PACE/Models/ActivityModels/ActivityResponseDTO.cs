namespace PACE.Models.ActivityModels;

public class ActivityResponseDTO
{
    public int Id { get; set; }
    public int GoalId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CreatedBy { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public string? UpdatedBy { get; private set; }
    public DateTime? UpdatedOn { get; private set; }

    public ActivityResponseDTO()
    {
        Id = 0;
        GoalId = 0;
        Title = string.Empty;
        Description = string.Empty;
        CreatedBy = string.Empty;
        CreatedOn = DateTime.UtcNow;
        UpdatedBy = null;
        UpdatedOn = null;
    }

    public ActivityResponseDTO(int id, int goalId, string title, string description, string createdBy, DateTime createdOn, string? updatedBy, DateTime? updatedOn)
    {
        Id = id;
        GoalId = goalId;
        Title = title;
        Description = description;
        CreatedBy = createdBy;
        CreatedOn = createdOn;
        UpdatedBy = updatedBy;
        UpdatedOn = updatedOn;
    }
}
