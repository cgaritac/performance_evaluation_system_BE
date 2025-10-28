using Microsoft.EntityFrameworkCore;
using PACE.Data;
using PACE.Interfaces.IRepositories;
using PACE.Models.ActivityModels;

namespace PACE.Repositories;

public class ActivityRepository(PaceDbContext context) : IActivityRepository
{
    private readonly PaceDbContext _context = context;

    public IQueryable<ActivityModel> GetActivitiesByGoalId(int goalId)
    {
        return _context.Activities
            .AsNoTracking()
            .Where(a => a.IsActive && a.GoalId == goalId);
    }

    public IQueryable<ActivityModel> GetActivityById(int id)
    {
        return _context.Activities
            .AsNoTracking()
            .Where(a => a.IsActive && a.Id == id);
    }

    public IQueryable<ActivityModel> GetActivityByGoalId(int goalId)
    {
        return _context.Activities
            .AsNoTracking()
            .Where(a => a.IsActive && a.GoalId == goalId);
    }

    public async Task<ActivityModel> AddAsync(ActivityModel activity)
    {
        _context.Activities.Add(activity);
        await _context.SaveChangesAsync();

        return activity;
    }

    public async Task<ActivityModel> UpdateAsync(ActivityModel activity)
    {
        _context.Activities.Update(activity);
        await _context.SaveChangesAsync();

        return activity;
    }

    public async Task<bool> RemoveAsync(ActivityModel activity)
    {
        _context.Activities.Remove(activity);
        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> RemoveListAsync(List<ActivityModel> activities)
    {
        _context.Activities.RemoveRange(activities);
        var result = await _context.SaveChangesAsync();

        return result > 0;
    }
}
