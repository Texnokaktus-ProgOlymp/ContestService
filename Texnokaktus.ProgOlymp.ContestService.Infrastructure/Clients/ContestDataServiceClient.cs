using Grpc.Core;
using Texnokaktus.ProgOlymp.Common.Contracts.Grpc.YandexContest;
using Texnokaktus.ProgOlymp.ContestService.Infrastructure.Clients.Abstractions;

namespace Texnokaktus.ProgOlymp.ContestService.Infrastructure.Clients;

internal class ContestDataServiceClient(ContestDataService.ContestDataServiceClient client) : IContestDataServiceClient
{
    public async Task<string?> GetContestUrlAsync(long contestId)
    {
        var request = new GetContestUrlRequest
        {
            ContestId = contestId
        };

        var response = await client.GetContestUrlAsync(request);
        return response.ContestUrl;
    }

    public async Task<ContestDescription> GetContestAsync(long contestId)
    {
        var request = new GetContestRequest
        {
            ContestId = contestId
        };

        var response = await client.GetContestAsync(request);
        return response.Result;
    }
}
