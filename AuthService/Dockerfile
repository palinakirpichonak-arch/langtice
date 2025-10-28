FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER root
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080 \
    ASPNETCORE_ENVIRONMENT=Development

EXPOSE 5000
EXPOSE 5001

HEALTHCHECK --interval=30s --timeout=5s --start-period=10s --retries=3 \
    CMD curl -f http://localhost:8080/health || exit 1

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["AuthService.PL/AuthService.PL.csproj", "AuthService.PL/"]
COPY ["AuthService.AL/AuthService.AL.csproj", "AuthService.AL/"]
COPY ["AuthService.IL/AuthService.IL.csproj", "AuthService.IL/"]
COPY ["AuthService.DAL/AuthService.DAL.csproj", "AuthService.DAL/"]

RUN dotnet restore "AuthService.PL/AuthService.PL.csproj"

COPY . .

WORKDIR "/src/AuthService.PL"
RUN dotnet build "AuthService.PL.csproj" -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish
RUN dotnet publish "AuthService.PL.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthService.PL.dll"]
