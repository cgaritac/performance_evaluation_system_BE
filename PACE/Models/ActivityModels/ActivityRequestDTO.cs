namespace PACE.Models.ActivityModels;

public class ActivityRequestDTO
{
    public int Id { get; set; }
    public int GoalId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public ActivityRequestDTO()
    {
        Id = 0;
        GoalId = 0;
        Title = string.Empty;
        Description = string.Empty;
    }

    public ActivityRequestDTO(int id, int goalId, string title, string description)
    {
        Id = id;
        GoalId = goalId;
        Title = title;
        Description = description;
    }
}
