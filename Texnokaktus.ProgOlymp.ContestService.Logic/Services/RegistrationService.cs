using System.Diagnostics.CodeAnalysis;
using Texnokaktus.ProgOlymp.ContestService.Domain;
using Texnokaktus.ProgOlymp.ContestService.Infrastructure.Clients.Abstractions;
using Texnokaktus.ProgOlymp.ContestService.Logic.Exceptions;
using Texnokaktus.ProgOlymp.ContestService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.ContestService.Logic.Services;

public class RegistrationService(IContestService contestService,
                                 IRegistrationServiceClient registrationServiceClient,
                                 TimeProvider timeProvider) : IRegistrationService
{
    public async Task<ContestRegistrationState?> GetRegistrationState(int contestId)
    {
        if (await contestService.GetContestAsync(contestId) is not { } contest) return null;

        return new(contest.Id,
                   contest.Name,
                   contest.RegistrationStart,
                   contest.RegistrationFinish,
                   GetState(contest, timeProvider.GetUtcNow()));
    }

    public async Task RegisterUserToPreliminaryStage(int contestId, string login, string? displayName)
    {
        if (await contestService.GetContestAsync(contestId) is not { } contest)
            throw new ContestNotFoundException(contestId);

        if (GetState(contest, timeProvider.GetUtcNow()) != RegistrationState.InProgress)
            throw new RegistrationIsNotAvailableException();

        await registrationServiceClient.RegisterParticipantAsync(contest.PreliminaryStage!.Id, login, displayName);
    }
    
    [SuppressMessage("ReSharper", "ConvertIfStatementToReturnStatement")]
    private static RegistrationState GetState(Contest contest, DateTimeOffset now)
    {
        if (contest.PreliminaryStage is null)
            return RegistrationState.Unavailable;

        if (now < contest.RegistrationStart) return RegistrationState.NotStarted;
        if (now >= contest.RegistrationFinish) return RegistrationState.Finished;
        return RegistrationState.InProgress;
    }
}
