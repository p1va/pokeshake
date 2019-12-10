
# Base stage
# Starting from the Nano Server image create the intermediate image called base
# With working directory /app
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

# Build stage
# Starting from the SDK image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/PokeShake.DTO/PokeShake.Dto.csproj", "src/PokeShake.DTO/"]
COPY ["src/PokeShake.Services.Common/PokeShake.Services.Common.csproj", "src/PokeShake.Services.Common/"]
COPY ["src/PokeShake.Services.FunTranslations/PokeShake.Services.FunTranslations.csproj", "src/PokeShake.Services.FunTranslations/"]
COPY ["src/PokeShake.Services.PokeApi/PokeShake.Services.PokeApi.csproj", "src/PokeShake.Services.PokeApi/"]
COPY ["src/PokeShake.Services.PokemonShakespeareanDescription/PokeShake.Services.PokemonShakespeareanDescription.csproj", "src/PokeShake.Services.PokemonShakespeareanDescription/"]
COPY ["src/PokeShake.WebApi/PokeShake.WebApi.csproj", "src/PokeShake.WebApi/"]

RUN dotnet restore "src/PokeShake.WebApi/PokeShake.WebApi.csproj"
COPY . .
WORKDIR "/src/src/PokeShake.WebApi"
RUN dotnet build "PokeShake.WebApi.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "PokeShake.WebApi.csproj" -c Release -o /app/publish

# Final stage
# Copy from publish into the base
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PokeShake.WebApi.dll"]