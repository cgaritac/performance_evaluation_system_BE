using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PACE.Interfaces.IServices;
using PACE.Models.ActivityModels;
using PACE.Utils.Constants;
using PACE.Utils.Helpers;

namespace PACE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivitiesController(IActivityService activityService) : ControllerBase
{
    private readonly IActivityService _activityService = activityService;

    [HttpGet("idgoal/{goalid}")]
    [Authorize(Roles = RolConstants.AdminRol + "," + RolConstants.UserRol)]
    public async Task<IActionResult> GetByActivity(int goalid)
    {
        var result = await _activityService.GetActivitiesAsync(goalid);

        return ControllerResponseHelper.HandleControllerResult(result);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = RolConstants.AdminRol + "," + RolConstants.UserRol)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _activityService.GetActivityResponseByIdAsync(id);

        return ControllerResponseHelper.HandleControllerResult(result);
    }

    [HttpPost]
    [Authorize(Roles = RolConstants.AdminRol + "," + RolConstants.UserRol)]
    public async Task<IActionResult> Create([FromBody] ActivityRequestDTO activityDTO)
    {
        if (!ModelState.IsValid)
            return ValidatorHelper.BadRequestResult(ValidatorHelper.GetModeStateErrors(ModelState), null);

        var result = await _activityService.CreateActivityAsync(activityDTO);

        if (result.Success)
            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);

        return ControllerResponseHelper.HandleControllerResult(result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = RolConstants.AdminRol + "," + RolConstants.UserRol)]
    public async Task<IActionResult> Update(int id, [FromBody] ActivityRequestDTO activityDTO)
    {
        if (!ModelState.IsValid)
            return ValidatorHelper.BadRequestResult(ValidatorHelper.GetModeStateErrors(ModelState), null);

        if (id != activityDTO.Id)
            return ValidatorHelper.BadRequestResult(null, ErrorConstants.IdMismatchError);

        var result = await _activityService.UpdateActivityAsync(activityDTO);

        return ControllerResponseHelper.HandleControllerResult(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = RolConstants.AdminRol + "," + RolConstants.UserRol)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _activityService.DeleteActivityAsync(id);

        return ControllerResponseHelper.HandleControllerResult(result);
    }
}
