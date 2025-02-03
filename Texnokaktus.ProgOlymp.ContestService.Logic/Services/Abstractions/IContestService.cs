namespace Texnokaktus.ProgOlymp.ContestService.Logic.Services.Abstractions;

public interface IContestService
{
    Task<int> AddContestAsync(string name,
                              DateTimeOffset registrationStart,
                              DateTimeOffset registrationEnd);
}
