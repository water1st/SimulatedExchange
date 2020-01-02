FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/SimulatedExchange.Api/SimulatedExchange.Api.csproj", "src/SimulatedExchange.Api/"]
COPY ["src/SimulatedExchange.Commands/SimulatedExchange.Commands.csproj", "src/SimulatedExchange.Commands/"]
COPY ["src/SimulatedExchange.Domain/SimulatedExchange.Domain.csproj", "src/SimulatedExchange.Domain/"]
COPY ["src/SimulatedExchange.DataAccess.Abstraction/SimulatedExchange.DataAccess.Abstraction.csproj", "src/SimulatedExchange.DataAccess.Abstraction/"]
COPY ["src/SimulatedExchange.ClientAdapter.Abstraction/SimulatedExchange.ClientAdapter.Abstraction.csproj", "src/SimulatedExchange.ClientAdapter.Abstraction/"]
COPY ["src/SimulatedExchange.Abstraction/SimulatedExchange.Abstraction.csproj", "src/SimulatedExchange.Abstraction/"]
COPY ["src/SimulatedExchange.Utilities/SimulatedExchange.Utilities.csproj", "src/SimulatedExchange.Utilities/"]
COPY ["src/SimulatedExchange.Queries/SimulatedExchange.Queries.csproj", "src/SimulatedExchange.Queries/"]
COPY ["src/SimulatedExchange.DataAccess.Memory/SimulatedExchange.DataAccess.Memory.csproj", "src/SimulatedExchange.DataAccess.Memory/"]
COPY ["src/SimulatedExchange.DataAccess.MySQL/SimulatedExchange.DataAccess.MySQL.csproj", "src/SimulatedExchange.DataAccess.MySQL/"]
COPY ["src/SimulatedExchange.Applications/SimulatedExchange.Applications.csproj", "src/SimulatedExchange.Applications/"]
RUN dotnet restore "src/SimulatedExchange.Api/SimulatedExchange.Api.csproj"
COPY . .
WORKDIR "/src/src/SimulatedExchange.Api"
RUN dotnet build "SimulatedExchange.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimulatedExchange.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimulatedExchange.Api.dll"]