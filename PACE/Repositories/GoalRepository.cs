using Microsoft.EntityFrameworkCore;
using PACE.Data;
using PACE.Interfaces.IRepositories;
using PACE.Models.GoalModels;

namespace PACE.Repositories;

public class GoalRepository(PaceDbContext context) : IGoalRepository
{
    private readonly PaceDbContext _context = context;

    public IQueryable<GoalModel> GetGoalById(int id)
    {
        return _context.Goals
            .AsNoTracking()
            .Where(g => g.IsActive && g.Id == id);
    }

    public async Task<GoalModel> AddAsync(GoalModel goal)
    {
        _context.Goals.Add(goal);
        await _context.SaveChangesAsync();

        return goal;
    }

    public async Task<GoalModel> UpdateAsync(GoalModel goal)
    {
        _context.Goals.Update(goal);
        await _context.SaveChangesAsync();

        return goal;
    }

    public async Task<bool> RemoveAsync(GoalModel goal)
    {
        _context.Goals.Remove(goal);
        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> RemoveListAsync(List<GoalModel> goals)
    {
        _context.Goals.RemoveRange(goals);
        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> ExistsGoalAsync(int id)
    {
        var goalExists = await _context.Goals
            .AsNoTracking()
            .AnyAsync(e => e.IsActive && e.Id == id);

        return goalExists;
    }
}
