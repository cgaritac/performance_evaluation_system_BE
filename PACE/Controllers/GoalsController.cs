using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PACE.Interfaces.IServices;
using PACE.Models.GoalModels;
using PACE.Utils.Constants;
using PACE.Utils.Helpers;

namespace PACE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalsController(IGoalService goalService) : ControllerBase
{
    private readonly IGoalService _goalService = goalService;

    [HttpGet("{id}")]
    [Authorize(Roles = RolConstants.AdminRol + "," + RolConstants.UserRol)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _goalService.GetGoalResponseByIdAsync(id);

        return ControllerResponseHelper.HandleControllerResult(result);
    }

    [HttpPost]
    [Authorize(Roles = RolConstants.AdminRol + "," + RolConstants.UserRol)]
    public async Task<IActionResult> Create([FromBody] GoalRequestDTO goalDTO)
    {
        if (!ModelState.IsValid)
            return ValidatorHelper.BadRequestResult(ValidatorHelper.GetModeStateErrors(ModelState), null);

        var result = await _goalService.CreateGoalAsync(goalDTO);

        if (result.Success)
            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);

        return ControllerResponseHelper.HandleControllerResult(result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = RolConstants.AdminRol + "," + RolConstants.UserRol)]
    public async Task<IActionResult> Update(int id, [FromBody] GoalRequestDTO goalDTO)
    {
        if (!ModelState.IsValid)
            return ValidatorHelper.BadRequestResult(ValidatorHelper.GetModeStateErrors(ModelState), null);

        if (id != goalDTO.Id)
            return ValidatorHelper.BadRequestResult(null, ErrorConstants.IdMismatchError);

        var result = await _goalService.UpdateGoalAsync(goalDTO);

        return ControllerResponseHelper.HandleControllerResult(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = RolConstants.AdminRol + "," + RolConstants.UserRol)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _goalService.DeleteGoalAsync(id);

        return ControllerResponseHelper.HandleControllerResult(result);
    }
}
