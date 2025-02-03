using Texnokaktus.ProgOlymp.ContestService.DataAccess.Context;
using Texnokaktus.ProgOlymp.ContestService.DataAccess.Repositories.Abstractions;
using Texnokaktus.ProgOlymp.ContestService.DataAccess.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.ContestService.DataAccess.Services;

public class UnitOfWork(AppDbContext context, IContestRepository contestRepository) : IUnitOfWork
{
    public IContestRepository ContestRepository { get; } = contestRepository;

    public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
}
