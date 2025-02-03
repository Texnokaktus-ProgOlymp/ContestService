using Texnokaktus.ProgOlymp.Common.Contracts.Grpc.YandexContest;

namespace Texnokaktus.ProgOlymp.ContestService.Infrastructure.Clients.Abstractions;

public interface IContestDataServiceClient
{
    Task<string?> GetContestUrlAsync(long contestId);
    Task<ContestDescription> GetContest(long contestId);
}
