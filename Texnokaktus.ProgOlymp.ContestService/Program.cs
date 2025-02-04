using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Texnokaktus.ProgOlymp.ContestService.DataAccess;
using Texnokaktus.ProgOlymp.ContestService.DataAccess.Context;
using Texnokaktus.ProgOlymp.ContestService.Infrastructure;
using Texnokaktus.ProgOlymp.ContestService.Infrastructure.Clients.Abstractions;
using Texnokaktus.ProgOlymp.ContestService.Logic;
using Texnokaktus.ProgOlymp.ContestService.Logic.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
       .AddDataAccess(optionsBuilder => optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDb")))
       .AddLogicServices();

builder.Services.AddSingleton(TimeProvider.System);

builder.Services.AddGrpcClients(builder.Configuration);

builder.Services.AddEndpointsApiExplorer().AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ContestService", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World");

await app.RunAsync();
