using PACE.Models.EvaluationModels;
using PACE.Utils.Constants;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PACE.Models.EmployeeModels;

public class EmployeeModel
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = ErrorConstants.RequiredEmployeeNumberError)]
    public int EmployeeNumber { get; set; }

    [Required(ErrorMessage = ErrorConstants.RequiredFirstNameError)]
    [StringLength(NumericConstants.MaxFirstName, ErrorMessage = ErrorConstants.FirstNameLengthError)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = ErrorConstants.RequiredLastNameError)]
    [StringLength(NumericConstants.MaxLastName, ErrorMessage = ErrorConstants.LastNameLengthError)]
    public string LastName { get; set; }

    [Required(ErrorMessage = ErrorConstants.RequiredEmployeeEmailError)]
    [EmailAddress(ErrorMessage = ErrorConstants.EmailFormatError)]
    public string EmployeeEmail { get; set; }

    [Required(ErrorMessage = ErrorConstants.RequiredDepartmentIdError)]
    public int DepartmentId { get; set; }

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
    public ICollection<EvaluationModel>? Evaluations { get; set; }

    public EmployeeModel()
    {
        EmployeeNumber = 0;
        FirstName = string.Empty;
        LastName = string.Empty;
        EmployeeEmail = string.Empty;
        DepartmentId = 0;
        CreatedBy = string.Empty;
        CreatedOn = DateTime.UtcNow;
        IsActive = true;

        Evaluations = new List<EvaluationModel>();
    }
}