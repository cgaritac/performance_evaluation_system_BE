using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PACE.Interfaces.IServices;
using PACE.Models.CommonModels;
using PACE.Models.EvaluationModels;
using PACE.Utils.Constants;
using PACE.Utils.Helpers;

namespace PACE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EvaluationsController(IEvaluationService evaluationService) : ControllerBase
{
    private readonly IEvaluationService _evaluationService = evaluationService;

    [HttpGet]
    //[Authorize(Roles = RolConstants.AdminRol)]
    public async Task<IActionResult> Get([FromQuery] PageRequestDTO pageRequest)
    {
        if (!ModelState.IsValid)
            return ValidatorHelper.BadRequestResult(ValidatorHelper.GetModeStateErrors(ModelState), null);

        var result = await _evaluationService.GetEvaluations_PagedAsync(pageRequest);

        return ControllerResponseHelper.HandleControllerResult(result);
    }

    [HttpGet("withgoals/{employeeid}")]
    [Authorize(Roles = RolConstants.AdminRol + "," + RolConstants.UserRol)]
    public async Task<IActionResult> GetByEmployeeIdWithGoals(int employeeid, [FromQuery] EvaluationRequestDTO evaluationDTO)
    {
        if (!ModelState.IsValid)
            return ValidatorHelper.BadRequestResult(ValidatorHelper.GetModeStateErrors(ModelState), null);

        if (employeeid != evaluationDTO.EmployeeId)
            return ValidatorHelper.BadRequestResult(null, ErrorConstants.IdMismatchError);

        var result = await _evaluationService.GetEvaluationWithGoalsByEmployeeIdAsync(employeeid, evaluationDTO.Year);

        return ControllerResponseHelper.HandleControllerResult(result);
    }

    [HttpPost("all")]
    [Authorize(Roles = RolConstants.AdminRol)]
    public async Task<IActionResult> CreateAll([FromBody] EvaluationRequestDTO evaluationDTO)
    {
        if (!ModelState.IsValid)
            return ValidatorHelper.BadRequestResult(ValidatorHelper.GetModeStateErrors(ModelState), null);

        var result = await _evaluationService.CreateEvaluationsAsync(evaluationDTO);

        return ControllerResponseHelper.HandleControllerResult(result);
    }

    [HttpPut("idemployee/{employeeid}")]
    [Authorize(Roles = RolConstants.AdminRol)]
    public async Task<IActionResult> Update(int employeeid, [FromBody] EvaluationRequestDTO evaluationDTO)
    {
        if (!ModelState.IsValid)
            return ValidatorHelper.BadRequestResult(ValidatorHelper.GetModeStateErrors(ModelState), null);

        if (employeeid != evaluationDTO.EmployeeId)
            return ValidatorHelper.BadRequestResult(null, ErrorConstants.IdMismatchError);

        var result = await _evaluationService.UpdateEvaluationAsync(evaluationDTO);

        return ControllerResponseHelper.HandleControllerResult(result);
    }
}
