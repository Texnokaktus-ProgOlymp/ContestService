using Texnokaktus.ProgOlymp.Common.Contracts.Grpc.YandexContest;

namespace Texnokaktus.ProgOlymp.ContestService.Infrastructure.Clients.Abstractions;

public interface IRegistrationServiceClient
{
    Task<Error?> RegisterParticipantAsync(long contestStageId, string login, string? displayName);
}
