using Texnokaktus.ProgOlymp.ContestService.DataAccess.Services.Abstractions;
using Texnokaktus.ProgOlymp.ContestService.Infrastructure.Clients.Abstractions;
using Texnokaktus.ProgOlymp.ContestService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.ContestService.Logic.Services;

internal class ContestService(IUnitOfWork unitOfWork, IContestDataServiceClient contestDataServiceClient) : IContestService
{
    public async Task<int> AddContestAsync(string name,
                                           DateTimeOffset registrationStart,
                                           DateTimeOffset registrationFinish,
                                           long? preliminaryStageId,
                                           long? finalStageId)
    {
        var contest = unitOfWork.ContestRepository.AddContest(new(name, registrationStart, registrationFinish));

        if (preliminaryStageId.HasValue)
        {
            var description = await contestDataServiceClient.GetContestAsync(preliminaryStageId.Value);
            contest.PreliminaryStage = new()
            {
                Id = preliminaryStageId.Value,
                Name = description.Name,
                ContestStart = description.StartTime.ToDateTimeOffset(),
                Duration = description.Duration.ToTimeSpan()
            };
        }

        if (finalStageId.HasValue)
        {
            var description = await contestDataServiceClient.GetContestAsync(finalStageId.Value);
            contest.FinalStage = new()
            {
                Id = finalStageId.Value,
                Name = description.Name,
                ContestStart = description.StartTime.ToDateTimeOffset(),
                Duration = description.Duration.ToTimeSpan()
            };
        }

        await unitOfWork.SaveChangesAsync();

        return contest.Id;
    }
}
