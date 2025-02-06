using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Texnokaktus.ProgOlymp.Common.Contracts.Grpc.ContestService;
using Texnokaktus.ProgOlymp.ContestService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.ContestService.Services.Grpc;

public class ContestServiceImpl(IContestService contestService) : Common.Contracts.Grpc.ContestService.ContestService.ContestServiceBase
{
    public override async Task<GetContestResponse> GetContest(GetContestRequest request, ServerCallContext context)
    {
        if (await contestService.GetContestAsync(request.ContestId) is { } contest)
            return new()
            {
                Result = contest.MapContest()
            };

        throw new RpcException(new(StatusCode.NotFound, $"Contest with id {request.ContestId} was not found"));
    }

    public override async Task<AddContestResponse> AddContest(AddContestRequest request, ServerCallContext context)
    {
        var id = await contestService.AddContestAsync(request.Name,
                                                      request.RegistrationStart.ToDateTimeOffset(),
                                                      request.RegistrationFinish.ToDateTimeOffset(),
                                                      request.PreliminaryStageId,
                                                      request.FinalStageId);

        return new()
        {
            ContestId = id
        };
    }
}

file static class MappingExtensions
{
    public static Contest MapContest(this Domain.Contest contest) =>
        new()
        {
            Id = contest.Id,
            Name = contest.Name,
            RegistrationStart = contest.RegistrationStart.ToTimestamp(),
            RegistrationFinish = contest.RegistrationFinish.ToTimestamp(),
            PreliminaryStage = contest.PreliminaryStage?.MapContestStage(),
            FinalStage = contest.FinalStage?.MapContestStage()
        };

    private static ContestStage MapContestStage(this Domain.ContestStage contestStage) =>
        new()
        {
            Id = contestStage.Id,
            Name = contestStage.Name,
            ContestStart = contestStage.ContestStart.ToTimestamp(),
            ContestFinish = contestStage.ContestFinish?.ToTimestamp(),
            Duration = contestStage.Duration.ToDuration()
        };
}
