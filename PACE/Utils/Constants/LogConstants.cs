namespace PACE.Utils.Constants;

public static class LogConstants
{
    public const string SoftDeleteApplied = "Soft delete applied to entity {EntityType} with ID {Id}";
    public const string SoftDeleteInRelatedActivity = "Soft delete in activity related to GoalId {GoalId}, ActivityId {ActivityId}";
    public const string TotalActivitiesSoftDeleted = "Total activities soft deleted related to GoalId {GoalId}: {Count}";

    public const string UnhandledExceptionError = "Unhandled exception caught in middleware. " + "Path: {RequestPath}, Method: {RequestMethod}, StatusCode: 500";

    public const string CreateActivityError = "Error creating the activity";
    public const string UpdateActivityError = "Error updating the activity with ID {ActivityId}";
    public const string DeleteActivityError = "Error deleting the activity with ID {ActivityId}";

    public const string CreateEvaluationError = "Error creating the evaluation for employee {EmployeeId}";
    public const string CreateEvaluationsError = "Error creating the evaluations for department {DepartmentId}";
    public const string UpdateEvaluationError = "Error updating the evaluation with ID {EvaluationId}";
    public const string DeleteEvaluationError = "Error deleting the evaluation with ID {EvaluationId}";

    public const string CreateGoalError = "Error creating the goal for evaluation ID: {EvaluationId}";
    public const string UpdateGoalError = "Error updating the goal with ID: {GoalId}";
    public const string DeleteGoalError = "Error deleting the goal with ID: {GoalId}";
}
