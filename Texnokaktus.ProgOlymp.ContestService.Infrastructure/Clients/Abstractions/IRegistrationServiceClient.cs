namespace Texnokaktus.ProgOlymp.ContestService.Infrastructure.Clients.Abstractions;

public interface IRegistrationServiceClient
{
    Task RegisterParticipantAsync(long contestStageId, string login, string? displayName);
}
