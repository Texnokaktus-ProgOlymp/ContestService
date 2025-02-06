using Texnokaktus.ProgOlymp.Common.Contracts.Grpc.YandexContest;

namespace Texnokaktus.ProgOlymp.ContestService.Infrastructure.Clients.Abstractions;

public interface IYandexContestRegistrationService
{
    Task<Error?> RegisterParticipantAsync()
}
