FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG GHPAT
ARG VERSION
WORKDIR /src
COPY ["Texnokaktus.ProgOlymp.ContestService/Texnokaktus.ProgOlymp.ContestService.csproj", "Texnokaktus.ProgOlymp.ContestService/"]
COPY ["Texnokaktus.ProgOlymp.ContestService.Infrastructure/Texnokaktus.ProgOlymp.ContestService.Infrastructure.csproj", "Texnokaktus.ProgOlymp.ContestService.Infrastructure/"]
COPY ["modules/Common.Contracts/Texnokaktus.ProgOlymp.Common.Contracts/Texnokaktus.ProgOlymp.Common.Contracts.csproj", "modules/Common.Contracts/Texnokaktus.ProgOlymp.Common.Contracts/"]
COPY ["Texnokaktus.ProgOlymp.ContestService.DataAccess/Texnokaktus.ProgOlymp.ContestService.DataAccess.csproj", "Texnokaktus.ProgOlymp.ContestService.DataAccess/"]
COPY ["Texnokaktus.ProgOlymp.ContestService.Logic/Texnokaktus.ProgOlymp.ContestService.Logic.csproj", "Texnokaktus.ProgOlymp.ContestService.Logic/"]
COPY ["Texnokaktus.ProgOlymp.ContestService.Domain/Texnokaktus.ProgOlymp.ContestService.Domain.csproj", "Texnokaktus.ProgOlymp.ContestService.Domain/"]
RUN dotnet nuget add source --username kav128 --password $GHPAT --store-password-in-clear-text --name "GitHub TexnoKaktus-Progolymp" https://nuget.pkg.github.com/Texnokaktus-ProgOlymp/index.json
RUN dotnet restore "Texnokaktus.ProgOlymp.ContestService/Texnokaktus.ProgOlymp.ContestService.csproj"
COPY . .
WORKDIR "/src/Texnokaktus.ProgOlymp.ContestService"
RUN dotnet build "Texnokaktus.ProgOlymp.ContestService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Texnokaktus.ProgOlymp.ContestService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false /p:Version=$VERSION

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Texnokaktus.ProgOlymp.ContestService.dll"]
