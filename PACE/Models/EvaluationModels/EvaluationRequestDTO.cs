using PACE.Utils.Enums;

namespace PACE.Models.EvaluationModels;

public class EvaluationRequestDTO
{
    public int Id { get; set; }
    public int? EmployeeId { get; set; }
    public int Year { get; set; }
    public int? DepartmentId { get; set; }
    public string? FeedbackComments { get; set; }
    public FeedbackEnum? Feedback { get; set; }


    public EvaluationRequestDTO()
    {
        Id = 0;
        EmployeeId = 0;
        Year = DateTime.Now.Year;
        DepartmentId = 0;
        FeedbackComments = string.Empty;
        Feedback = FeedbackEnum.NotEvaluated;
    }

    public EvaluationRequestDTO(int id, int? employeeId, int year, int? departmentId, string? feedbackComments, FeedbackEnum? feedback)
    {
        Id = id;
        EmployeeId = employeeId;
        Year = year;
        DepartmentId = departmentId;
        FeedbackComments = feedbackComments;
        Feedback = feedback;
    }
}
