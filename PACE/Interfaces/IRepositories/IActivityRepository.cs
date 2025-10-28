using PACE.Models.ActivityModels;

namespace PACE.Interfaces.IRepositories;

public interface IActivityRepository
{
    IQueryable<ActivityModel> GetActivitiesByGoalId(int goalId);
    IQueryable<ActivityModel> GetActivityById(int id);
    Task<ActivityModel> AddAsync(ActivityModel activity);
    Task<ActivityModel> UpdateAsync(ActivityModel activity);
    Task<bool> RemoveAsync(ActivityModel activity);
    Task<bool> RemoveListAsync(List<ActivityModel> activities);
}
