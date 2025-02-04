using Texnokaktus.ProgOlymp.ContestService.DataAccess.Entities;
using Texnokaktus.ProgOlymp.ContestService.DataAccess.Models;

namespace Texnokaktus.ProgOlymp.ContestService.DataAccess.Repositories.Abstractions;

public interface IContestRepository
{
    Task<Contest?> GetById(int id);
    Contest AddContest(ContestInsertModel insertModel);
    Task<bool> UpdateAsync(int id, Func<Contest, bool> updateAction);
}
