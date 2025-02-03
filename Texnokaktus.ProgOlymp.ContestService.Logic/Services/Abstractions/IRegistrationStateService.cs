using Texnokaktus.ProgOlymp.ContestService.Domain;

namespace Texnokaktus.ProgOlymp.ContestService.Logic.Services.Abstractions;

public interface IRegistrationStateService
{
    Task<ContestRegistrationState?> GetState(int contestId);
}
