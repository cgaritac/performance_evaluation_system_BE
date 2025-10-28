using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PACE.Models.ActivityModels;
using PACE.Models.EvaluationModels;
using PACE.Models.GoalModels;
using PACE.Utils.Constants;
using PACE.Interfaces.IServices;

namespace PACE.Interceptors;

public class SoftDeleteInterceptor(ILogger<SoftDeleteInterceptor> logger, IUserService userService) : SaveChangesInterceptor
{
    private readonly ILogger<SoftDeleteInterceptor> _logger = logger;
    private readonly IUserService _userService = userService;

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is null) return result;

        var user = _userService.GetUserName() ?? "Unknown";

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if ((entry.Entity is EvaluationModel || entry.Entity is GoalModel || entry.Entity is ActivityModel) && entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;

                entry.CurrentValues["IsActive"] = false;
                entry.CurrentValues["DeletedBy"] = user;
                entry.CurrentValues["DeletedOn"] = DateTime.UtcNow;

                _logger.LogInformation(LogConstants.SoftDeleteApplied, entry.Entity.GetType().Name, entry.OriginalValues["Id"]);

                if (entry.Entity is GoalModel goal)
                {
                    var context = eventData.Context;

                    var activities = context.Set<ActivityModel>()
                        .Where(a => a.GoalId == goal.Id && a.IsActive)
                        .ToList();

                    foreach (var activity in activities)
                    {
                        var activityEntry = context.Entry(activity);
                        activityEntry.State = EntityState.Modified;
                        activity.Delete(user);

                        _logger.LogInformation(LogConstants.SoftDeleteInRelatedActivity,
                            goal.Id, activity.Id);
                    }

                    _logger.LogInformation(LogConstants.TotalActivitiesSoftDeleted,
                        goal.Id, activities.Count);
                }

            }
        }

        return result;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        return new ValueTask<InterceptionResult<int>>(SavingChanges(eventData, result));
    }
}