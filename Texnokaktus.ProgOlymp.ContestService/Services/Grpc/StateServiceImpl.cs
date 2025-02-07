using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Texnokaktus.ProgOlymp.Common.Contracts.Grpc.ContestService;
using Texnokaktus.ProgOlymp.ContestService.Domain;
using Texnokaktus.ProgOlymp.ContestService.Logic.Exceptions;
using Texnokaktus.ProgOlymp.ContestService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.ContestService.Services.Grpc;

public class StateServiceImpl(IRegistrationService registrationService) : RegistrationService.RegistrationServiceBase
{
    public override async Task<GetRegistrationStateResponse> GetRegistrationState(GetRegistrationStateRequest request, ServerCallContext context)
    {
        if (await registrationService.GetRegistrationState(request.ContestId) is not { } registrationState)
            throw new RpcException(new(StatusCode.NotFound, $"Contest with Id {request.ContestId} was not found"));

        return new()
        {
            Result = registrationState.State == RegistrationState.InProgress
        };
    }

    public override async Task<Empty> RegisterUserToPreliminaryStage(RegisterUserToPreliminaryStageRequest request, ServerCallContext context)
    {
        try
        {
            await registrationService.RegisterUserToPreliminaryStage(request.ContestId, request.Login, request.DisplayName);
            return new();
        }
        catch (ContestNotFoundException e)
        {
            throw new RpcException(new(StatusCode.NotFound, e.Message, e));
        }
        catch (RegistrationIsNotAvailableException e)
        {
            throw new RpcException(new(StatusCode.FailedPrecondition, e.Message, e));
        }
    }
}
