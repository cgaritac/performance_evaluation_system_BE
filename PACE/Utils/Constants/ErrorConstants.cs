namespace PACE.Utils.Constants;

public static class ErrorConstants
{
    public const string CreationError = "Error creating.";
    public const string UpdateError = "Error updating.";
    public const string DeletionError = "Error deleting.";

    public const string IdMismatchError = "The ID in the URL does not match the ID in the request body.";
    public const string UnexpectedError = "An unexpected error occurred. Please try again later.";
    public const string TestError = "This is a Test-Error";
    public const string EmailFormatError = "Email format is invalid.";

    public const string StartDateAfterEndDateError = "The Start date can not be after the End date.";
    public const string StartDateAfterDueDateError = "The Start date can not be after the Due date.";
    public const string EndDateWithoutStartDateError = "The End date can not be set without a Start date.";

    public const string EmployeeExistanceError = "Employee does not exist.";
    public const string EvaluationExistanceError = "Evaluation does not exist.";
    public const string GoalExistanceError = "Goal does not exist.";
    public const string EmployeeWithDepartmentIdExistanceError = "No employees found for this department.";

    public const string EvaluationExistsError = "Evaluation already exist.";
    public const string EvaluationsExistsError = "All the employees already have evaluations for the department: ";

    public const string InvalidEvaluationError = "Invalid evaluation.";
    public const string InvalidGoalError = "Invalid goal.";
    public const string InvalidActivityError = "Invalid activity.";

    public const string NotFoundEmployeeError = "Employee not found.";
    public const string NotFoundEvaluationError = "Evaluation not found.";
    public const string NotFoundGoalError = "Goal not found.";
    public const string NotFoundActivityError = "Activity not found.";

    public const string DaysAvailableIntervalError = "DaysAvailable must be between 0 and 100.";
    public const string YearIntervalError = "Year must be between 2000 and 2100.";

    public const string SearchTermLengthError = "The search term can not have more than 100 characters.";
    public const string DescriptionMaxLegthError = "Description cannot exceed 500 characters.";
    public const string FirstNameLengthError = "First name cant have more than 100 characters.";
    public const string LastNameLengthError = "Last name cant have more than 100 characters.";
    public const string IdentifierLengthError = "Identifier cant have more than 100 characters.";
    public const string TitleLengthError = "The title can not have more than 100 characters.";
    public const string CommentsMaxLengthError = "The comments can not have mora than 500 characters.";

    public const string RequiredEvaluationIdError = "EvaluationId is required";
    public const string RequiredGoalCategoryError = "GoalCategory is required";
    public const string RequiredGoalTypeError = "GoalType is required";
    public const string RequiredTitleError = "Title is required";
    public const string RequiredDueDateError = "DueDate is required";
    public const string RequiredStatusError = "Status is required";
    public const string RequiredEmployeeIdError = "EmployeeId is required.";
    public const string RequiredYearError = "Year is required.";
    public const string RequiredDescriptionError = "Description is required.";
    public const string RequiredGoalIdError = "GoalId is required.";
    public const string RequiredEmployeeNumberError = "EmployeeNumber is required.";
    public const string RequiredFirstNameError = "FirstName is required.";
    public const string RequiredLastNameError = "LastName is required.";
    public const string RequiredHireDateError = "HireDate is required.";
    public const string RequiredBirthDateError = "BirthDate is required.";
    public const string RequiredEmployeeEmailError = "EmployeeEmail is required.";
    public const string RequiredVacationsError = "VacationsApproverId is required.";
    public const string RequiredDepartmentIdError = "DepartmentId is required.";
    public const string RequiredDepartmentIdError = "DepartmentId is required.";

    public const string UserNotAllowedError = "User not Alowed to edit Manager-assigned goals";
}
