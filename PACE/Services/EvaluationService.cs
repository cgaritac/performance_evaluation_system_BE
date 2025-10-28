using Microsoft.EntityFrameworkCore;
using PACE.Interfaces.IRepositories;
using PACE.Interfaces.IServices;
using PACE.Models.CommonModels;
using PACE.Models.EvaluationModels;
using PACE.Utils.Constants;
using PACE.Utils.Enums;
using PACE.Utils.Helpers;
using PACE.Utils.Mappers;

namespace PACE.Services;

public class EvaluationService(IEvaluationRepository evaluationRepository, IEmployeeService employeeService, ILogger<EvaluationService> logger, IUserService userService) : IEvaluationService
{
    private readonly IEvaluationRepository _evaluationRepository = evaluationRepository;
    private readonly IEmployeeService _employeeService = employeeService;
    private readonly ILogger<EvaluationService> _logger = logger;
    private readonly IUserService _userService = userService;

    public async Task<ActionResultDTO<PageResponseDTO<EvaluationResponseDTO>>> GetEvaluations_PagedAsync(PageRequestDTO pageRequest)
    {
        var query = (IQueryable<EvaluationModel>)_evaluationRepository.GetEvaluations()
            .Include(e => e.Employee);

        query = ApplyFilters(query, pageRequest);

        var totalCount = await query.CountAsync();

        var evaluations = await query
            .AsNoTracking()
            .Skip((pageRequest.Page - 1) * pageRequest.PageSize)
            .Take(pageRequest.PageSize)
            .ToListAsync();

        var evaluationDTOs = evaluations.Select(e => e.ToDto()).ToList();

        var pageResponse = new PageResponseDTO<EvaluationResponseDTO>(evaluationDTOs, totalCount, pageRequest.Page, pageRequest.PageSize);

        return ActionResultDTO<PageResponseDTO<EvaluationResponseDTO>>.Ok(pageResponse);
    }

    public async Task<ActionResultDTO<EvaluationWithGoalsResponseDTO>> GetEvaluationWithGoalsByEmployeeIdAsync(int employeeId, int year)
    {
        var evaluations = await _evaluationRepository.GetEvaluationsWithGoalsByEmployeeId(employeeId, year).ToListAsync(); ;

        if (!evaluations.Any())
            return ActionResultDTO<EvaluationWithGoalsResponseDTO>.Fail(ErrorConstants.NotFoundEvaluationError, ErrorType.NotFound);

        var dto = evaluations.Select(evaluation => evaluation.ToEvaluationWithGoalsDto()).ToList().FirstOrDefault();

        if (dto is null)
            return ActionResultDTO<EvaluationWithGoalsResponseDTO>.Fail(ErrorConstants.NotFoundEvaluationError, ErrorType.NotFound);

        return ActionResultDTO<EvaluationWithGoalsResponseDTO>.Ok(dto);
    }

    public async Task<ActionResultDTO<bool>> CreateEvaluationsAsync(EvaluationRequestDTO evaluationDTO)
    {
        var departmentId = evaluationDTO.DepartmentId ?? 0;

        var employees = await _employeeService.GetEmployeesAsync(departmentId);

        if (employees.Data == null || employees.Data.Count == 0)
            return ActionResultDTO<bool>.Fail(ErrorConstants.EmployeeWithDepartmentIdExistanceError, ErrorType.NotFound);

        var evaluationsExists = await VerifyEvaluationsExistsByDepartmentIdAsync(departmentId, evaluationDTO.Year);

        if (evaluationsExists)
            return ActionResultDTO<bool>.Fail(ErrorConstants.EvaluationsExistsError + departmentId, ErrorType.NotFound);

        var existingEvaluations = _evaluationRepository.AllEvaluationsExistsByDepartmentIdAsync(departmentId, evaluationDTO.Year);
        var existingEmployeeIds = existingEvaluations.Select(e => e.EmployeeId).ToList();

        var user = _userService.GetUserName() ?? "Unknown";

        var employeesWithoutEvaluations = employees.Data
            .Where(e => !existingEmployeeIds.Contains(e.Id))
            .Select(e => e.ToEvaluation(evaluationDTO.Year, user))
            .ToList();

        try
        {
            var created = await _evaluationRepository.AddRangeAsync(employeesWithoutEvaluations);

            return created
                 ? ActionResultDTO<bool>.Ok(created)
                 : ActionResultDTO<bool>.Fail(ErrorConstants.CreationError, ErrorType.Validation);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LogConstants.CreateEvaluationsError, evaluationDTO.DepartmentId);
            return ActionResultDTO<bool>.Fail($"{ErrorConstants.CreationError}, {ex.Message}", ErrorType.Unknown);
        }
    }

    public async Task<ActionResultDTO<EvaluationResponseDTO>> UpdateEvaluationAsync(EvaluationRequestDTO evaluationDTO)
    {
        var employeeId = evaluationDTO.EmployeeId ?? 0;

        var employeeExists = await _employeeService.VerifyExistsEmployeeAsync(employeeId);

        if (!employeeExists)
            return ActionResultDTO<EvaluationResponseDTO>.Fail(ErrorConstants.EmployeeExistanceError, ErrorType.NotFound);

        var evaluationResult = await GetEvaluationByIdAsync(evaluationDTO.Id);

        if (evaluationResult.Data is null || !evaluationResult.Success)
            return ActionResultDTO<EvaluationResponseDTO>.Fail(ErrorConstants.NotFoundEvaluationError, ErrorType.NotFound);

        var evaluationExists = await VerifyEvaluationExistsByEmployeeIdAsync(employeeId, evaluationDTO.Year);

        if (!evaluationExists)
            return ActionResultDTO<EvaluationResponseDTO>.Fail(ErrorConstants.EvaluationExistanceError, ErrorType.Validation);

        var user = _userService.GetUserName() ?? "Unknown";

        evaluationResult.Data.Update(evaluationDTO.Year, evaluationDTO.FeedbackComments, evaluationDTO.Feedback, user);

        try
        {
            var updated = await _evaluationRepository.UpdateAsync(evaluationResult.Data);
            var updatedWithEmployeeResult = await GetEvaluationByEmployeeIdAsync(updated.EmployeeId, updated.Year);

            if (updatedWithEmployeeResult.Data is null || !updatedWithEmployeeResult.Success)
                return ActionResultDTO<EvaluationResponseDTO>.Fail(ErrorConstants.NotFoundEvaluationError, ErrorType.NotFound);

            return ActionResultDTO<EvaluationResponseDTO>.Ok(updatedWithEmployeeResult.Data.ToDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LogConstants.UpdateEvaluationError, evaluationDTO.Id);
            return ActionResultDTO<EvaluationResponseDTO>.Fail($"{ErrorConstants.UpdateError}, {ex.Message}", ErrorType.Unknown);
        }
    }

    public async Task<bool> VerifyEvaluationExistsByIdAsync(int id)
    {
        return await _evaluationRepository.EvaluationExistsByIdAsync(id);
    }

    private async Task<bool> VerifyEvaluationExistsByEmployeeIdAsync(int employeeId, int year)
    {
        return await _evaluationRepository.EvaluationExistsByEmployeeIdAsync(employeeId, year);
    }

    private async Task<bool> VerifyEvaluationsExistsByDepartmentIdAsync(int departmentId, int year)
    {
        var employees = await _employeeService.GetEmployeesAsync(departmentId);

        if (employees is null || !employees.Success || employees.Data is null)
            return false;

        var employeesList = employees.Data.Count;

        var evaluations = await _evaluationRepository.AllEvaluationsExistsByDepartmentIdAsync(departmentId, year).CountAsync();

        if (employeesList == evaluations)
            return true;

        return false;
    }

    private async Task<ActionResultDTO<EvaluationModel>> GetEvaluationByIdAsync(int id)
    {
        var evaluation = await _evaluationRepository.GetEvaluationById(id).FirstOrDefaultAsync();

        if (evaluation is null)
            return ActionResultDTO<EvaluationModel>.Fail(ErrorConstants.NotFoundEvaluationError, ErrorType.NotFound);

        return ActionResultDTO<EvaluationModel>.Ok(evaluation);
    }

    private async Task<ActionResultDTO<EvaluationModel>> GetEvaluationByEmployeeIdAsync(int employeeId, int year)
    {
        var evaluation = await _evaluationRepository.GetEvaluationByEmployeeId(employeeId, year).FirstOrDefaultAsync();

        if (evaluation is null)
            return ActionResultDTO<EvaluationModel>.Fail(ErrorConstants.NotFoundEvaluationError, ErrorType.NotFound);

        return ActionResultDTO<EvaluationModel>.Ok(evaluation);
    }

    private IQueryable<EvaluationModel> ApplyFilters(IQueryable<EvaluationModel> query, PageRequestDTO pageRequest)
    {
        query = Department_Filter(query, pageRequest);
        query = Search_Filter(query, pageRequest);
        query = Year_Filter(query, pageRequest);
        query = SortBy_Filter(query, pageRequest);

        return query;
    }

    private IQueryable<EvaluationModel> Department_Filter(IQueryable<EvaluationModel> query, PageRequestDTO pageRequest)
    {
        query = pageRequest.DepartmentId.HasValue
            ? query.Where(e => e.Employee != null && e.Employee.DepartmentId == pageRequest.DepartmentId)
            : query;

        return query;
    }

    private IQueryable<EvaluationModel> Search_Filter(IQueryable<EvaluationModel> query, PageRequestDTO pageRequest)
    {
        if (!string.IsNullOrEmpty(pageRequest.SearchTerm))
        {
            var searchTerm = pageRequest.SearchTerm.ToLower();

            int? feedbackValue = EnumsConversionHelper.GetFeedbackValue(searchTerm);

            query = query.Where(e => e.Employee != null &&
                (e.Employee.FirstName.ToLower().Contains(searchTerm) ||
                 e.Employee.LastName.ToLower().Contains(searchTerm) ||
                 (e.Employee.FirstName.ToLower() + " " + e.Employee.LastName.ToLower()).Contains(searchTerm) ||
                 e.Id.ToString().Contains(searchTerm) ||
                 e.Year.ToString().Contains(searchTerm) ||
                 e.EmployeeId.ToString().Contains(searchTerm) ||
                 (feedbackValue.HasValue && (int?)e.Feedback == feedbackValue.Value)));
        }

        return query;
    }

    private IQueryable<EvaluationModel> Year_Filter(IQueryable<EvaluationModel> query, PageRequestDTO pageRequest)
    {
        query = pageRequest.Year.HasValue
            ? query.Where(e => e.Year == pageRequest.Year)
            : query.Where(e => e.Year == DateTime.UtcNow.Year);

        return query;
    }

    private IQueryable<EvaluationModel> SortBy_Filter(IQueryable<EvaluationModel> query, PageRequestDTO pageRequest)
    {
        if (!string.IsNullOrEmpty(pageRequest.SortBy))
        {
            var isDescending = string.Equals(pageRequest.SortDirection, "desc", StringComparison.OrdinalIgnoreCase);

            switch (pageRequest.SortBy.ToLower())
            {
                case "year":
                    query = isDescending ? query.OrderByDescending(e => e.Year) : query.OrderBy(e => e.Year);
                    break;

                case "employeeid":
                    query = isDescending ? query.OrderByDescending(e => e.EmployeeId) : query.OrderBy(e => e.EmployeeId);
                    break;

                case "employeefullname":
                    query = isDescending
                        ? query.OrderByDescending(e => e.Employee!.FirstName)
                        : query.OrderBy(e => e.Employee!.FirstName);
                    break;

                case "feedback":
                    query = isDescending
                        ? query.OrderByDescending(e => e.Feedback)
                        : query.OrderBy(e => e.Feedback);
                    break;

                default:
                    query = isDescending ? query.OrderByDescending(e => e.Id) : query.OrderBy(e => e.Id);
                    break;
            };
        }
        else
        {
            query = query.OrderBy(e => e.Id);
        }

        return query;
    }
}
