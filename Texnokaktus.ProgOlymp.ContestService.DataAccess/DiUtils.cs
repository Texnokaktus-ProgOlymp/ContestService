using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Texnokaktus.ProgOlymp.ContestService.DataAccess.Context;
using Texnokaktus.ProgOlymp.ContestService.DataAccess.Repositories;
using Texnokaktus.ProgOlymp.ContestService.DataAccess.Repositories.Abstractions;
using Texnokaktus.ProgOlymp.ContestService.DataAccess.Services;
using Texnokaktus.ProgOlymp.ContestService.DataAccess.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.ContestService.DataAccess;

public static class DiUtils
{
    public static IServiceCollection AddDataAccess(this IServiceCollection serviceCollection,
                                                   Action<DbContextOptionsBuilder> optionsAction) =>
        serviceCollection.AddDbContext<AppDbContext>(optionsAction)
                         .AddScoped<IUnitOfWork, UnitOfWork>()
                         .AddScoped<IContestRepository, ContestRepository>();

    public static IHealthChecksBuilder AddDatabaseHealthChecks(this IHealthChecksBuilder builder) =>
        builder.AddDbContextCheck<AppDbContext>("database");
}
