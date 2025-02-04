
using System.Diagnostics.CodeAnalysis;
using Texnokaktus.ProgOlymp.ContestService.DataAccess.Services.Abstractions;
using Texnokaktus.ProgOlymp.ContestService.Domain;
using Texnokaktus.ProgOlymp.ContestService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.ContestService.Logic.Services;

internal class RegistrationStateService(IUnitOfWork unitOfWork, TimeProvider timeProvider) : IRegistrationStateService
{
    public async Task<ContestRegistrationState?> GetState(int contestId)
    {
        if (await unitOfWork.ContestRepository.GetById(contestId) is not { } contest) return null;

        return new(contest.Id,
                   contest.Name,
                   contest.RegistrationStart,
                   contest.RegistrationFinish,
                   GetState(contest.RegistrationStart,
                            contest.RegistrationFinish));
    }

    [SuppressMessage("ReSharper", "ConvertIfStatementToReturnStatement")]
    private RegistrationState GetState(DateTimeOffset registrationStart, DateTimeOffset registrationFinish)
    {
        var now = timeProvider.GetUtcNow();

        if (now < registrationStart) return RegistrationState.NotStarted;
        if (now >= registrationFinish) return RegistrationState.Finished;
        return RegistrationState.InProgress;
    }
}
