using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Texnokaktus.ProgOlymp.Common.Contracts.Grpc.YandexContest;
using Texnokaktus.ProgOlymp.ContestService.Infrastructure.Clients;
using Texnokaktus.ProgOlymp.ContestService.Infrastructure.Clients.Abstractions;

namespace Texnokaktus.ProgOlymp.ContestService.Infrastructure;

public static class DiExtensions
{
    public static IServiceCollection AddGrpcClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpcClient<ContestDataService.ContestDataServiceClient>(options => options.Address = configuration.GetConnectionStringUri(nameof(ContestDataService)));

        return services.AddScoped<IContestDataServiceClient, ContestDataServiceClient>();
    }

    private static Uri? GetGrpcConnectionString<TGrpcService>(this IConfiguration configuration) =>
        configuration.GetConnectionStringUri(typeof(TGrpcService).Name);

    private static Uri? GetConnectionStringUri(this IConfiguration configuration, string name) =>
        configuration.GetConnectionString(name) is { } connectionString
            ? new Uri(connectionString)
            : null;
}
