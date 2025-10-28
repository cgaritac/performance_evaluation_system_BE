using PACE.Models.GoalModels;
using PACE.Utils.Enums;

namespace PACE.Models.EvaluationModels;

public class EvaluationWithGoalsResponseDTO
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeFullName { get; set; }
    public int Year { get; set; }
    public string? FeedbackComments { get; set; }
    public FeedbackEnum? Feedback { get; set; }

    public List<GoalResponseDTO> Goals { get; set; }


    public EvaluationWithGoalsResponseDTO()
    {
        Id = 0;
        EmployeeId = 0;
        EmployeeFullName = string.Empty;
        Year = DateTime.Now.Year;
        FeedbackComments = string.Empty;
        Feedback = FeedbackEnum.NotEvaluated;

        Goals = new List<GoalResponseDTO>();
    }

    public EvaluationWithGoalsResponseDTO(
            int id,
            int employeeId,
            string employeeFullName,
            int year,
            string? feedbackComments,
            FeedbackEnum? feedback,

            List<GoalResponseDTO> goals
        )
    {
        Id = id;
        EmployeeId = employeeId;
        EmployeeFullName = employeeFullName;
        Year = year;
        FeedbackComments = feedbackComments ?? string.Empty;
        Feedback = feedback ?? FeedbackEnum.NotEvaluated;

        Goals = goals;
    }
}
