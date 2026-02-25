FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER root
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080 \
    ASPNETCORE_ENVIRONMENT=Development

EXPOSE 8080
EXPOSE 8081

HEALTHCHECK --interval=30s --timeout=5s --start-period=10s --retries=3 \
    CMD curl -f http://localhost:8080/health || exit 1

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src   

COPY ["MainService/MainService.PL/MainService.PL.csproj", "MainService/MainService.PL/"]
COPY ["MainService/MainService.AL/MainService.AL.csproj", "MainService/MainService.AL/"]
COPY ["MainService/MainService.DAL/MainService.DAL.csproj", "MainService/MainService.DAL/"]
COPY ["MainService/MainService.IL/MainService.IL.csproj", "MainService/MainService.IL/"]

COPY ["Shared/Shared.csproj", "Shared/"]

RUN dotnet restore "MainService/MainService.PL/MainService.PL.csproj"

COPY . .

WORKDIR "/src/MainService/MainService.PL"
RUN dotnet build "MainService.PL.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "MainService.PL.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MainService.PL.dll"]
