using Microsoft.Extensions.DependencyInjection;
using Texnokaktus.ProgOlymp.ContestService.Logic.Services;
using Texnokaktus.ProgOlymp.ContestService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.ContestService.Logic;

public static class DiExtensions
{
    public static IServiceCollection AddLogicServices(this IServiceCollection services) =>
        services.AddScoped<IRegistrationStateService, RegistrationStateService>()
                .AddScoped<IContestService, Logic.Services.ContestService>()
                .Decorate<IContestService, ContestServiceCachingDecorator>();
}
