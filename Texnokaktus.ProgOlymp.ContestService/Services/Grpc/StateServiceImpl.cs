using Grpc.Core;
using Texnokaktus.ProgOlymp.Common.Contracts.Grpc.ContestService;
using Texnokaktus.ProgOlymp.ContestService.Domain;
using Texnokaktus.ProgOlymp.ContestService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.ContestService.Services.Grpc;

public class StateServiceImpl(IRegistrationStateService registrationStateService) : StateService.StateServiceBase
{
    public override async Task<GetRegistrationStateResponse> GetRegistrationState(GetRegistrationStateRequest request, ServerCallContext context)
    {
        if (await registrationStateService.GetState(request.ContestId) is not { } registrationState)
            throw new RpcException(new(StatusCode.NotFound, $"Contest with Id {request.ContestId} was not found"));

        return new()
        {
            Result = registrationState.State == RegistrationState.InProgress
        };
    }
}
