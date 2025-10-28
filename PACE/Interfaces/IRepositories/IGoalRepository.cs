using PACE.Models.GoalModels;

namespace PACE.Interfaces.IRepositories;

public interface IGoalRepository
{
    IQueryable<GoalModel> GetGoalById(int id);
    Task<GoalModel> AddAsync(GoalModel goal);
    Task<GoalModel> UpdateAsync(GoalModel goal);
    Task<bool> RemoveAsync(GoalModel goal);
    Task<bool> ExistsGoalAsync(int id);
}
