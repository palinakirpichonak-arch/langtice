FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src


COPY ["MainService.PL/MainService.PL.csproj", "MainService.PL/"]
COPY ["MainService.AL/MainService.AL.csproj", "MainService.AL/"]
COPY ["MainService.IL/MainService.IL.csproj", "MainService.IL/"]
COPY ["MainService.DAL/MainService.DAL.csproj", "MainService.DAL/"]
COPY ["Migrations/Migrations.csproj", "Migrations/"]

RUN dotnet restore "MainService.PL/MainService.PL.csproj"

COPY . .

WORKDIR "/src/MainService.PL"
RUN dotnet build "MainService.PL.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "MainService.PL.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MainService.PL.dll"]
