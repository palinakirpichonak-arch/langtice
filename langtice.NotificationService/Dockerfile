FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["langtice.NotificationService/langtice.NotificationService.csproj", "langtice.NotificationService/"]
RUN dotnet restore "langtice.NotificationService/langtice.NotificationService.csproj"
COPY . .
WORKDIR "/src/langtice.NotificationService"
RUN dotnet build "./langtice.NotificationService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./langtice.NotificationService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "langtice.NotificationService.dll"]
