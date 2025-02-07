using Texnokaktus.ProgOlymp.ContestService.Domain;

namespace Texnokaktus.ProgOlymp.ContestService.Logic.Services.Abstractions;

public interface IRegistrationService
{
    Task<ContestRegistrationState?> GetRegistrationState(int contestId);
    Task RegisterUserToPreliminaryStage(int contestId, string login, string? displayName);
}
