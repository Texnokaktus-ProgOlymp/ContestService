using Texnokaktus.ProgOlymp.ContestService.DataAccess.Services.Abstractions;
using Texnokaktus.ProgOlymp.ContestService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.ContestService.Logic.Services;

public class ContestService(IUnitOfWork unitOfWork) : IContestService
{
    public Task<int> AddContestAsync(string name, DateTimeOffset registrationStart, DateTimeOffset registrationEnd)
    {
        throw new NotImplementedException();
    }
}
