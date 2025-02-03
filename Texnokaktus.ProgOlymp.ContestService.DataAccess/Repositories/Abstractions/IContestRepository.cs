using Texnokaktus.ProgOlymp.ContestService.DataAccess.Entities;

namespace Texnokaktus.ProgOlymp.ContestService.DataAccess.Repositories.Abstractions;

public interface IContestRepository
{
    Task<Contest?> GetById(int id);
    Task<bool> UpdateAsync(int id, Func<Contest, bool> updateAction);
}
