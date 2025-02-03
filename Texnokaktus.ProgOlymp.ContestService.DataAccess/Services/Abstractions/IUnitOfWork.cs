using Texnokaktus.ProgOlymp.ContestService.DataAccess.Repositories.Abstractions;

namespace Texnokaktus.ProgOlymp.ContestService.DataAccess.Services.Abstractions;

public interface IUnitOfWork
{
    IContestRepository ContestRepository { get; }
    Task<int> SaveChangesAsync();
}
