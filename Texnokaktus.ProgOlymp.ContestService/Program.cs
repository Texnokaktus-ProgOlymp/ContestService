using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Texnokaktus.ProgOlymp.ContestService.Converters;
using Texnokaktus.ProgOlymp.ContestService.DataAccess;
using Texnokaktus.ProgOlymp.ContestService.Infrastructure;
using Texnokaktus.ProgOlymp.ContestService.Logic;
using Texnokaktus.ProgOlymp.ContestService.Logic.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
       .AddDataAccess(optionsBuilder => optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDb")))
       .AddLogicServices();

builder.Services.AddSingleton(TimeProvider.System);

builder.Services.AddGrpcClients(builder.Configuration);

builder.Services.AddOpenApi(options => options.AddSchemaTransformer<SchemaTransformer>());

builder.Services.ConfigureHttpJsonOptions(options => options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.ConfigObject.Urls = [new() { Name = "v1", Url = "/openapi/v1.json" }]);
}

app.MapGroup("api/contests")
   .MapGet("{contestId:int}",
           (int contestId, IRegistrationStateService registrationStateService) =>
               registrationStateService.GetState(contestId));

await app.RunAsync();
