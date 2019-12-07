#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["tests/PokeShake.Services.PokeApi.Tests/PokeShake.Services.PokeApi.Tests.csproj", "tests/PokeShake.Services.PokeApi.Tests/"]
COPY ["src/PokeShake.Services.PokeApi/PokeShake.Services.PokeApi.csproj", "src/PokeShake.Services.PokeApi/"]
COPY ["src/PokeShake.Services.Common/PokeShake.Services.Common.csproj", "src/PokeShake.Services.Common/"]
RUN dotnet restore "tests/PokeShake.Services.PokeApi.Tests/PokeShake.Services.PokeApi.Tests.csproj"
COPY . .
WORKDIR "/src/tests/PokeShake.Services.PokeApi.Tests"
RUN dotnet test "PokeShake.Services.PokeApi.Tests.csproj" #-c Release -o /app/build

#FROM build AS publish
#RUN dotnet publish "PokeShake.Services.PokeApi.Tests.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "test", "PokeShake.Services.PokeApi.Tests.dll"]